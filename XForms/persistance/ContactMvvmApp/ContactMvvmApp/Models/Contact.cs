using SQLite;

namespace ContactMvvmApp.Models
{
   public class Contact
   {
      private const int DefaultMaxLength = 0xFF;

      [PrimaryKey]
      [AutoIncrement]
      public int Id { get; set; }

      [MaxLength(DefaultMaxLength)]
      public string FirstName { get; set; }

      [MaxLength(DefaultMaxLength)]
      public string LastName { get; set; }

      [MaxLength(DefaultMaxLength)]
      public string Phone { get; set; }

      [MaxLength(DefaultMaxLength)]
      public string Email { get; set; }

      public bool IsBlocked { get; set; }
   }
}