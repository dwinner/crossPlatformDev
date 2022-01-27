using SQLite.Net.Interop;

namespace FileStorageApp.DataAccess.Storage
{
   /// <summary>
   ///    The SQLite setup object
   /// </summary>
   public interface ISqLiteSetup
   {
      /// <summary>
      ///    Database path
      /// </summary>
      string DatabasePath { get; set; }

      /// <summary>
      ///    The platform abstraction
      /// </summary>
      ISQLitePlatform Platform { get; set; }
   }
}