using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmSampleApp.Views;
using Xamarin.Forms;

namespace MvvmSampleApp.ViewModels
{
   public class FriendListViewModel : ViewModelBase
   {
      private ICommand _back;
      private ICommand _createFriend;
      private ICommand _deleteFriend;
      private ICommand _saveFriend;
      private FriendViewModel _selectedFriend;

      public FriendListViewModel() => Friends = new ObservableCollection<FriendViewModel>();

      public ObservableCollection<FriendViewModel> Friends { get; set; }

      public ICommand CreateFriendCommand =>
         _createFriend ?? (_createFriend = new Command(() =>
            Navigation.PushAsync(new FriendPage(new FriendViewModel {ListViewModel = this}))));

      public ICommand DeleteFriendCommand =>
         _deleteFriend ?? (_deleteFriend = new Command(friendObj =>
         {
            if (friendObj is FriendViewModel friend)
            {
               Friends.Remove(friend);
            }

            BackCommand.Execute(null);
         }));

      public ICommand SaveFriendCommand =>
         _saveFriend ?? (_saveFriend = new Command(friendObj =>
         {
            if (friendObj is FriendViewModel friend && friend.IsValid)
            {
               Friends.Add(friend);
            }

            BackCommand.Execute(null);
         }));

      public ICommand BackCommand => _back ?? (_back = new Command(() => Navigation.PopAsync()));

      public INavigation Navigation { get; set; }

      public FriendViewModel SelectedFriend
      {
         get => _selectedFriend;
         set
         {
            if (!Equals(_selectedFriend, value))
            {
               var tempFriend = value;
               _selectedFriend = null;
               OnPropertyChanged();
               Navigation.PushAsync(new FriendPage(tempFriend));
            }
         }
      }
   }
}