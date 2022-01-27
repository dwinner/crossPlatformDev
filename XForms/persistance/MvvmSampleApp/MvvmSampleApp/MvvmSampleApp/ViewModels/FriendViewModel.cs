using System;
using MvvmSampleApp.Models;

namespace MvvmSampleApp.ViewModels
{
   public class FriendViewModel : ViewModelBase
   {
      private FriendListViewModel _friendListVm;

      public FriendViewModel() => Friend = new Friend();

      public Friend Friend { get; }

      public FriendListViewModel ListViewModel
      {
         get => _friendListVm;
         set
         {
            if (!Equals(_friendListVm, value))
            {
               _friendListVm = value;
               OnPropertyChanged();
            }
         }
      }

      public string Name
      {
         get => Friend.Name;
         set
         {
            if (!string.Equals(Friend.Name, value, StringComparison.CurrentCultureIgnoreCase))
            {
               Friend.Name = value;
               OnPropertyChanged();
            }
         }
      }

      public string Email
      {
         get => Friend.Email;
         set
         {
            if (!string.Equals(Friend.Email, value, StringComparison.CurrentCultureIgnoreCase))
            {
               Friend.Email = value;
               OnPropertyChanged();
            }
         }
      }

      public string Phone
      {
         get => Friend.Phone;
         set
         {
            if (!string.Equals(Friend.Phone, value, StringComparison.CurrentCultureIgnoreCase))
            {
               Friend.Phone = value;
               OnPropertyChanged();
            }
         }
      }

      public bool IsValid =>
         !string.IsNullOrEmpty(Name.Trim())
         || !string.IsNullOrEmpty(Phone.Trim())
         || !string.IsNullOrEmpty(Email.Trim());
   }
}