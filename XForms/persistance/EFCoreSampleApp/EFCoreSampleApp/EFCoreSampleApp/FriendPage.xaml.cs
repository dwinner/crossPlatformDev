using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EFCoreSampleApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FriendPage
   {
      private readonly string _dbPath;

      public FriendPage()
      {
         InitializeComponent();
         _dbPath = DependencyService.Get<IDbPath>().GetDbPath(App.DbFileName);
      }

      private async void OnSaveFriend(object sender, EventArgs e)
      {
         var friend = (Friend) BindingContext;
         if (!string.IsNullOrEmpty(friend.Name))
         {
            using (var dbContext = new ApplicationContext(_dbPath))
            {
               if (friend.Id == 0)
               {
                  dbContext.Friends.Add(friend);
               }
               else
               {
                  dbContext.Friends.Update(friend);
               }

               await dbContext.SaveChangesAsync().ConfigureAwait(true);
            }
         }

         await Navigation.PopAsync().ConfigureAwait(true);
      }

      private async void OnDeleteFriend(object sender, EventArgs e)
      {
         var friend = (Friend) BindingContext;
         using (var dbContext = new ApplicationContext(_dbPath))
         {
            dbContext.Friends.Remove(friend);
            await dbContext.SaveChangesAsync().ConfigureAwait(true);
         }

         await Navigation.PopAsync().ConfigureAwait(true);
      }
   }
}