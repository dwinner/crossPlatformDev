using SQLite.Net.Attributes;

namespace FileStorageApp.DataAccess.Storable
{
   /// <summary>
   ///    File storable
   /// </summary>
   public class FileStorable : IStorable
   {
      /// <summary>
      ///    File contents
      /// </summary>
      public string Content { get; set; }

      [PrimaryKey]
      public string Key { get; set; }
   }
}