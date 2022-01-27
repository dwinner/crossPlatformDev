using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileClient;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SocialClientApp
{
   public class ApplicationViewModel : ViewModelBase
   {
      private const string EndPointUrl = "";
      private ICommand _backCommand;
      private ICommand _createFriendCommand;
      private ICommand _deleteFriendCommand;
      private readonly FriendsService _friendsService = new FriendsService(EndPointUrl);
      private bool _initialized;
      private bool _isBusy;
      private ICommand _saveFriendCommand;
      private Friend _selectedFriend;

      public ApplicationViewModel()
      {
         Friends = new ObservableCollection<Friend>();
         IsBusy = false;
      }

      public ObservableCollection<Friend> Friends { get; }

      public ICommand CreateFriendCommand =>
         _createFriendCommand ??
         (_createFriendCommand =
            new Command(() => Navigation.PushAsync(new FriendPage(new Friend(), this))));

      public ICommand DeleteFriendCommand
      {
         get
         {
            return _deleteFriendCommand ?? (_deleteFriendCommand = new Command(async friendObj =>
            {
               var friend = friendObj as Friend;
               if (friend != null)
               {
                  IsBusy = true;
                  var deletedFriend = await _friendsService.DeleteAsync(friend.Id).ConfigureAwait(true);
                  if (deletedFriend != null)
                  {
                     Friends.Remove(deletedFriend);
                  }

                  IsBusy = false;
               }

               BackCommand?.Execute(null);
            }));
         }
      }

      public ICommand SaveFriendCommand =>
         _saveFriendCommand ?? (_saveFriendCommand = new Command(async friendObj =>
         {
            var friend = friendObj as Friend;
            if (friend != null)
            {
               IsBusy = true;

               if (friend.Id > 0)
               {
                  var updatedFriend = await _friendsService.UpdateAsync(friend).ConfigureAwait(true);
                  if (updatedFriend != null)
                  {
                     var position = Friends.IndexOf(updatedFriend);
                     if (position != -1)
                     {
                        Friends.RemoveAt(position);
                        Friends.Insert(position, updatedFriend);
                     }
                  }
               }
               else
               {
                  var addedFriend = await _friendsService.AddAsync(friend).ConfigureAwait(true);
                  if (addedFriend != null)
                  {
                     Friends.Add(addedFriend);
                  }
               }

               IsBusy = false;
            }

            BackCommand?.Execute(null);
         }));

      public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(() => Navigation.PopAsync()));

      public INavigation Navigation { get; set; }

      public bool IsBusy
      {
         get => _isBusy;
         set
         {
            _isBusy = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsLoaded));
         }
      }

      public bool IsLoaded => !_isBusy;

      public Friend SelectedFriend
      {
         get => _selectedFriend;
         set
         {
            if (_selectedFriend != value)
            {
               var tempFriend = new Friend(value.Id, value.Name, value.Email, value.Phone);
               _selectedFriend = null;
               OnPropertyChanged();
               Navigation.PushAsync(new FriendPage(tempFriend, this));
            }
         }
      }

      public async Task GetFriendsAsync()
      {
         if (_initialized)
         {
            return;
         }

         IsBusy = true;
         var friends = await _friendsService.GetAsync().ConfigureAwait(true);

         //Friends.Clear();
         while (Friends.Count > 0)
         {
            Friends.RemoveAt(Friends.Count - 1);
         }

         friends.ForEach(friend => Friends.Add(friend));
         IsBusy = false;
         _initialized = true;
      }
   }
}