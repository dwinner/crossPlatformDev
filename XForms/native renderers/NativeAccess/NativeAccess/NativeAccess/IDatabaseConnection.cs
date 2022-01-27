using SQLite;

namespace NativeAccess
{
   public interface IDatabaseConnection
   {
      SQLiteConnection DbConnection();
   }
}