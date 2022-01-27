namespace FileStorageApp.DataAccess.Storable
{
   /// <summary>
   ///    The storable interface
   /// </summary>
   public interface IStorable
   {
      /// <summary>
      ///    Primary key
      /// </summary>
      string Key { get; set; }
   }
}