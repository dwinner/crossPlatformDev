using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ContactMvvmApp.Interfaces;
using ContactMvvmApp.Models;
using ContactMvvmApp.Utils;
using Xamarin.Forms;

namespace ContactMvvmApp.ViewModel
{
   public class ContactsPageViewModel : ViewModelBase
   {
      private readonly IContactStore _contactStore;
      private bool _isDataLoaded;
      private readonly IPageService _pageService;
      private ContactViewModel _selectedContact;

      public ContactsPageViewModel(IContactStore contactStore, IPageService pageService)
      {
         _contactStore = contactStore;
         _pageService = pageService;

         LoadDataCommand = new Command(async () => await LoadDataAsync().ConfigureAwait(true));
         AddContactCommand = new Command(async () => await AddContactAsync().ConfigureAwait(true));
         SelectContactCommand = new Command<ContactViewModel>(SelectContact);
         DeleteContactCommand = new Command<ContactViewModel>(async c => await DeleteContactAsync(c).ConfigureAwait(true));

         // Subscribe to events
         MessagingCenter.Subscribe<ContactDetailViewModel, Contact>(
            this, MessageBusNames.ContactAdded, OnContactAdded);
         MessagingCenter.Subscribe<ContactDetailViewModel, Contact>(
            this, MessageBusNames.ContactUpdated, OnContactUpdated);
      }

      private void OnContactAdded(ContactDetailViewModel vm, Contact addedContact) => Contacts.Add(new ContactViewModel(addedContact));

      private void OnContactUpdated(ContactDetailViewModel vm, Contact updatedContact)
      {
         var contactInList = Contacts.Single(c => c.Id == updatedContact.Id);

         contactInList.Id = updatedContact.Id;
         contactInList.FirstName = updatedContact.FirstName;
         contactInList.LastName = updatedContact.LastName;
         contactInList.Phone = updatedContact.Phone;
         contactInList.Email = updatedContact.Email;
         contactInList.IsBlocked = updatedContact.IsBlocked;
      }

      public ObservableCollection<ContactViewModel> Contacts { get; } = new ObservableCollection<ContactViewModel>();

      public ContactViewModel SelectedContact
      {
         get => _selectedContact;
         set => SetValue(ref _selectedContact, value);
      }

      public ICommand LoadDataCommand { get; }
      public ICommand AddContactCommand { get; }
      public ICommand SelectContactCommand { get; }
      public ICommand DeleteContactCommand { get; }

      private async Task LoadDataAsync()
      {
         if (_isDataLoaded)
         {
            return;
         }

         _isDataLoaded = true;
         var contacts = await _contactStore.GetContactsAsync().ConfigureAwait(true);

         foreach (var c in contacts)
         {
            Contacts.Add(new ContactViewModel(c));
         }
      }

      private async Task AddContactAsync()
      {
         await _pageService.PushAsync(new ContactDetailPage(new ContactViewModel())).ConfigureAwait(true);

         Debug.WriteLine(nameof(AddContactAsync));
      }

      private void SelectContact(ContactViewModel contact)
      {
         if (contact == null)
         {
            return;
         }

         SelectedContact = null;
         _pageService.PushAsync(new ContactDetailPage(contact));
      }

      private async Task DeleteContactAsync(ContactViewModel contactViewModel)
      {
         var alertResult = await _pageService.DisplayAlertAsync("Warning",
            $"Are you sure you want to delete {contactViewModel.FullName}?", "Yes", "No")
            .ConfigureAwait(true);
         if (alertResult)
         {
            Contacts.Remove(contactViewModel);
            var contact = await _contactStore.GetContactAsync(contactViewModel.Id)
               .ConfigureAwait(true);
            await _contactStore.DeleteContactAsync(contact)
               .ConfigureAwait(true);
         }
      }
   }
}