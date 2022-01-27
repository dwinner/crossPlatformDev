using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorageApp.DataAccess.Storable;
using FileStorageApp.Logging;
using FileStorageApp.Threading;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace FileStorageApp.DataAccess.Storage
{
   /// <summary>
   ///    SQLite storage impl
   /// </summary>
   public class SqLiteStorage : ISqLiteStorage
   {
      private readonly AsyncLock _asyncLock = new AsyncLock();
      private readonly string _dbPath;
      private readonly object _lockObject = new object();
      private readonly ILogger _logger;
      private readonly ISQLitePlatform _sqLitePlatform;
      private readonly string _tag;
      private SQLiteAsyncConnection _asyncConn;
      private SQLiteConnectionWithLock _conn;

      public SqLiteStorage(ISqLiteSetup sqLiteSetup, ILogger logger)
      {
         _dbPath = sqLiteSetup?.DatabasePath;
         _sqLitePlatform = sqLiteSetup?.Platform;
         _logger = logger;
         _tag = $"{GetType()} ";
      }

      /// <inheritdoc />
      public void CreateSqLiteAsyncConnection() => _asyncConn = new SQLiteAsyncConnection(() =>
         _conn ?? (_conn = new SQLiteConnectionWithLock(_sqLitePlatform, new SQLiteConnectionString(_dbPath, true))));

      /// <inheritdoc />
      public async Task CreateTableAsync<TStorableTable>() where TStorableTable : class, IStorable, new()
      {
         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            await _asyncConn.CreateTableAsync<TStorableTable>().ConfigureAwait(false);
         }
      }

      /// <inheritdoc />
      public async Task DropTableAsync<TStorableTable>() where TStorableTable : class, IStorable, new()
      {
         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            await _asyncConn.DropTableAsync<TStorableTable>().ConfigureAwait(false);
         }
      }

      /// <inheritdoc />
      public async Task InsertObjectAsync<TStorableTable>(TStorableTable item)
         where TStorableTable : class, IStorable, new()
      {
         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            try
            {
               var insertOrReplaceQuery = item.CreateInsertOrReplaceQuery();
               await _asyncConn.QueryAsync<TStorableTable>(insertOrReplaceQuery).ConfigureAwait(false);
            }
            catch (Exception error)
            {
               var location = $"InsertObject<T>() Failed to insert or replace object with key {item.Key}.";
               _logger.WriteLineTime(
                  $"{_tag}{Environment.NewLine}{location}{Environment.NewLine}ErrorMessage: {Environment.NewLine}{error.Message}{Environment.NewLine}Stacktrace: {Environment.NewLine}{error.StackTrace}");
            }
         }
      }

      /// <inheritdoc />
      public async Task<IList<TStorableTable>> GetTableAsync<TStorableTable>()
         where TStorableTable : class, IStorable, new()
      {
         var items = new List<TStorableTable>();

         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            try
            {
               items.AddRange(await _asyncConn
                  .QueryAsync<TStorableTable>($"SELECT * FROM {typeof(TStorableTable).Name};").ConfigureAwait(false));
            }
            catch (Exception error)
            {
               var location = $"GetTable<T>() Failed to 'SELECT *' from table {typeof(TStorableTable).Name}.";
               _logger.WriteLineTime(
                  $"{_tag}{Environment.NewLine}{location}{Environment.NewLine}ErrorMessage: {Environment.NewLine}{error.Message}{Environment.NewLine}Stacktrace: {Environment.NewLine}{error.StackTrace}");
            }
         }

         return items;
      }

      /// <inheritdoc />
      public async Task<TStorableTable> GetObjectAsync<TStorableTable>(string key)
         where TStorableTable : class, IStorable, new()
      {
         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            try
            {
               var items = await _asyncConn.QueryAsync<TStorableTable>(
                  $"SELECT * FROM {typeof(TStorableTable).Name} WHERE Key = '{key}';").ConfigureAwait(false);
               if (items != null && items.Count > 0)
               {
                  return items.FirstOrDefault();
               }
            }
            catch (Exception error)
            {
               var location = $"GetObject<T>() Failed to get object from key {key}.";
               _logger.WriteLineTime(
                  $"{_tag}{Environment.NewLine}{location}{Environment.NewLine}ErrorMessage: {Environment.NewLine}{error.Message}{Environment.NewLine}Stacktrace: {Environment.NewLine}{error.StackTrace}");
            }
         }

         return default(TStorableTable);
      }

      /// <inheritdoc />
      public async Task ClearTableAsync<TStorableTable>() where TStorableTable : class, IStorable, new()
      {
         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            await _asyncConn.QueryAsync<TStorableTable>($"DELETE FROM {typeof(TStorableTable).Name};")
               .ConfigureAwait(false);
         }
      }

      /// <inheritdoc />
      public async Task DeleteObjectAsync<TStorableTable>(TStorableTable item)
      {
         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            await _asyncConn.DeleteAsync(item).ConfigureAwait(false);
         }
      }

      /// <inheritdoc />
      public async Task DeleteObjectByKeyAsync<TStorableTable>(string key)
         where TStorableTable : class, IStorable, new()
      {
         using (await _asyncLock.LockAsync().ConfigureAwait(false))
         {
            try
            {
               await _asyncConn.QueryAsync<TStorableTable>(
                  $"DELETE FROM {typeof(TStorableTable).Name} WHERE Key=\'{key}\';").ConfigureAwait(false);
            }
            catch (Exception error)
            {
               var location = $"DeleteObjectByKey<T>() Failed to delete object from key {key}.";
               _logger.WriteLineTime(
                  $"{_tag}{Environment.NewLine}{location}{Environment.NewLine}ErrorMessage: {Environment.NewLine}{error.Message}{Environment.NewLine}Stacktrace: {Environment.NewLine}{error.StackTrace}");
            }
         }
      }

      /// <inheritdoc />
      public void CloseConnection()
      {
         lock (_lockObject)
         {
            if (_conn != null)
            {
               _conn.Close();
               _conn.Dispose();
               _conn = null;

               // Must be called as the disposal of the connection is not released until the GC runs.
               GC.Collect();
               GC.WaitForPendingFinalizers();
            }
         }
      }
   }
}