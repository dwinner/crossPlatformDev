//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PaperBoy.Common;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PaperBoy.Common;
#endif

namespace PaperBoy.Data
{
   public class FavoriteManager
   {
#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<Favorite> favoritesTable;
#else
      private readonly IMobileServiceTable<Favorite> _favoritesTable;
#endif
      private FavoriteManager()
      {
         CurrentClient = new MobileServiceClient(CoreConstants.ApplicationUrl);
#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore("localfavorites.db");
            store.DefineTable<Favorite>();

            this.client.SyncContext.InitializeAsync(store);

            this.favoritesTable = client.GetSyncTable<Favorite>();
#else
         _favoritesTable = CurrentClient.GetTable<Favorite>();
#endif
      }

      public static FavoriteManager DefaultManager { get; set; } = new FavoriteManager();

      public MobileServiceClient CurrentClient { get; }

      public bool IsOfflineEnabled => _favoritesTable is IMobileServiceSyncTable<Favorite>;

      public async Task<ObservableCollection<Favorite>> GetFavoritesAsync(bool syncItems = false)
      {
         try
         {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            var items = await _favoritesTable.ToEnumerableAsync();

            return new ObservableCollection<Favorite>(items);
         }
         catch (MobileServiceInvalidOperationException msioe)
         {
            Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
         }
         catch (Exception e)
         {
            Debug.WriteLine(@"Sync error: {0}", e.Message);
         }

         return null;
      }

      public async Task SaveFavoriteAsync(Favorite item)
      {
         try
         {
            if (item.Id == null)
            {
               await _favoritesTable.InsertAsync(item);
            }
            else
            {
               await _favoritesTable.UpdateAsync(item);
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine(@"SaveFavoriteAsync error: {0}", ex.Message);
         }
      }
#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.favoritesTable.PullAsync("allFavorites", this.favoritesTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult!=null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
                
            }

            if (syncErrors !=null)
            {
                foreach (var error in syncErrors)
                {
                    if(error.OperationKind==MobileServiceTableOperationKind.Update && error.Result !=null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);

                }
            }
        }
#endif
   }
}