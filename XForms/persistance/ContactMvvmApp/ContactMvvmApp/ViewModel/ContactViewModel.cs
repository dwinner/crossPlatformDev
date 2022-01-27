using ContactMvvmApp.Interfaces;
using ContactMvvmApp.Models;

namespace ContactMvvmApp.ViewModel
{
   public class ContactViewModel : ViewModelBase
   {
      private string _firstName;
      private string _lastName;

      public ContactViewModel()
      {
      }

      public ContactViewModel(Contact contact)
      {
         Id = contact.Id;
         _firstName = contact.FirstName;
         _lastName = contact.LastName;
         Phone = contact.Phone;
         Email = contact.Email;
         IsBlocked = contact.IsBlocked;
      }

      public int Id { get; set; }

      public string Phone { get; set; }

      public string Email { get; set; }

      public bool IsBlocked { get; set; }

      public string FirstName
      {
         get => _firstName;
         set
         {
            SetValue(ref _firstName, value);
            OnPropertyChanged(nameof(FullName));
         }
      }

      public string LastName
      {
         get => _lastName;
         set
         {
            SetValue(ref _lastName, value);
            OnPropertyChanged(nameof(FullName));
         }
      }

      public string FullName => $"{FirstName} {LastName}";
   }
}