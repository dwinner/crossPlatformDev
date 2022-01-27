using SQLite.Net.Attributes;
using SQLiteExample.Interfaces;

namespace SQLiteExample.Models
{
   public class Pets : IInterface
   {
      public int ParentId { get; set; }

      public string PetsName { get; set; }

      public int PetsAge { get; set; }

      public bool IsDog { get; set; }

      public string Breed { get; set; }

      [PrimaryKey]
      [AutoIncrement]
      public int Id => 0;
   }
}