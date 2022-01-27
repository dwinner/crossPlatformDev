using SQLite;

namespace ContactMvvmApp.Interfaces
{
   public interface ISqLiteDb
   {
      SQLiteAsyncConnection GetConnection();
   }
}