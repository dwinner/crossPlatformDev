using SQLite;

namespace ContactBookApp.Models
{
   public class Contact : ViewModelBase
   {
      private const int DbMaxLength = 0xFF;
      private string _firstName;
      private string _lastName;

      [PrimaryKey]
      [AutoIncrement]
      public int Id { get; set; }

      [MaxLength(DbMaxLength)]
      public string FirstName
      {
         get => _firstName;
         set
         {
            SetProperty(ref _firstName, value);
            OnPropertyChanged(nameof(FullName));
         }
      }

      [MaxLength(DbMaxLength)]
      public string LastName
      {
         get => _lastName;
         set
         {
            SetProperty(ref _lastName, value);
            OnPropertyChanged(nameof(FullName));
         }
      }

      public string FullName => $"{FirstName} {LastName}";

      [MaxLength(DbMaxLength)]
      public string Phone { get; set; }

      [MaxLength(DbMaxLength)]
      public string Email { get; set; }

      public bool IsBlocked { get; set; }
   }
}