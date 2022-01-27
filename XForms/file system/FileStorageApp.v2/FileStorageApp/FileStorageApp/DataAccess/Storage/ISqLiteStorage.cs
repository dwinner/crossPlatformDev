using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorageApp.DataAccess.Storable;

// TOREFACTOR: Is it possible to have the whole type generified?

namespace FileStorageApp.DataAccess.Storage
{
   /// <summary>
   ///    SQLite storage contract
   /// </summary>
   public interface ISqLiteStorage
   {
      /// <summary>
      ///    Creates the SQLite async connection
      /// </summary>
      void CreateSqLiteAsyncConnection();

      /// <summary>
      ///    Creates the table
      /// </summary>
      /// <typeparam name="TStorableTable">Table type</typeparam>
      /// <returns>Task</returns>
      Task CreateTableAsync<TStorableTable>() where TStorableTable : class, IStorable, new();

      /// <summary>
      ///    Drops the table
      /// </summary>
      /// <typeparam name="TStorableTable">Table type</typeparam>
      /// <returns>Task</returns>
      Task DropTableAsync<TStorableTable>() where TStorableTable : class, IStorable, new();

      /// <summary>
      ///    Inserts the object
      /// </summary>
      /// <typeparam name="TStorableTable">Table type</typeparam>
      /// <param name="item">Table item</param>
      /// <returns>Task</returns>
      Task InsertObjectAsync<TStorableTable>(TStorableTable item) where TStorableTable : class, IStorable, new();

      /// <summary>
      ///    Gets the table
      /// </summary>
      /// <typeparam name="TStorableTable">Table type</typeparam>
      /// <returns>Task</returns>
      Task<IList<TStorableTable>> GetTableAsync<TStorableTable>() where TStorableTable : class, IStorable, new();

      /// <summary>
      ///    Gets the object
      /// </summary>
      /// <typeparam name="TStorableTable">Table type</typeparam>
      /// <returns>The object</returns>
      Task<TStorableTable> GetObjectAsync<TStorableTable>(string key) where TStorableTable : class, IStorable, new();

      /// <summary>
      ///    Removes all table objects
      /// </summary>
      /// <returns>The all table objects</returns>
      /// <typeparam name="TStorableTable">Table type</typeparam>
      Task ClearTableAsync<TStorableTable>() where TStorableTable : class, IStorable, new();

      /// <summary>
      ///    Deletes the object
      /// </summary>
      /// <returns>The object</returns>
      /// <typeparam name="TStorableTable">Table type</typeparam>
      Task DeleteObjectAsync<TStorableTable>(TStorableTable item);

      /// <summary>
      ///    Deletes the object by key.
      /// </summary>
      /// <param name="key">Key</param>
      /// <typeparam name="TStorableTable">Table item</typeparam>
      /// <returns>The object by key</returns>
      Task DeleteObjectByKeyAsync<TStorableTable>(string key) where TStorableTable : class, IStorable, new();

      /// <summary>
      ///    Closes the connection.
      /// </summary>
      void CloseConnection();
   }
}