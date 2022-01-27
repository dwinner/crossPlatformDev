using SQLite.Net;

namespace SQLiteExample.Interfaces
{
   public interface ISqLiteConnectionFactory
   {
      SQLiteConnection GetConnection();
   }
}