using SQLite;

namespace ContactBookApp.Interfaces
{
   public interface ISqLiteDb
   {
      SQLiteAsyncConnection GetConnection();
   }
}