using System;

namespace MvvmSample.Model
{
   public class Person : ViewModelBase
   {
      private string _address;
      private DateTime _dateOfBirth;
      private string _fullName;

      public string FullName
      {
         get => _fullName;
         set
         {
            _fullName = value;
            OnPropertyChanged();
         }
      }

      public DateTime DateOfBirth
      {
         get => _dateOfBirth;
         set
         {
            _dateOfBirth = value;
            OnPropertyChanged();
         }
      }

      public string Address
      {
         get => _address;
         set
         {
            _address = value;
            OnPropertyChanged();
         }
      }
   }
}