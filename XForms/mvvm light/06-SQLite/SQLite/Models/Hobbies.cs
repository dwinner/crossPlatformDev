using SQLite.Net.Attributes;
using SQLiteExample.Interfaces;

namespace SQLiteExample.Models
{
   public class Hobbies : IInterface
   {
      public int ParentId { get; set; }

      public string HobbyName { get; set; }

      public string Description { get; set; }

      public string FreqencyAttended { get; set; }

      public double Cost { get; set; }

      [PrimaryKey]
      [AutoIncrement]
      public int Id => 0;
   }
}