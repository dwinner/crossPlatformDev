using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ContactBookApp.Interfaces;
using ContactBookApp.Models;
using SQLite;
using Xamarin.Forms;

namespace ContactBookApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class ContactListPage
   {
      private readonly SQLiteAsyncConnection _connection;
      private ObservableCollection<Contact> _contacts;
      private bool _isDataLoaded;

      public ContactListPage()
      {
         InitializeComponent();
         _connection = DependencyService.Get<ISqLiteDb>().GetConnection();
      }

      protected override async void OnAppearing()
      {
         if (_isDataLoaded)
         {
            return;
         }

         _isDataLoaded = true;
         await LoadDataAsync().ConfigureAwait(true);

         base.OnAppearing();
      }

      private async Task LoadDataAsync()
      {
         await _connection.CreateTableAsync<Contact>().ConfigureAwait(true);
         var contacts = await _connection.Table<Contact>().ToListAsync().ConfigureAwait(true);
         _contacts = new ObservableCollection<Contact>(contacts);
         contactsListView.ItemsSource = _contacts;
      }

      private void OnContactAdded(object sender, EventArgs e)
      {
         var contactDetailPage = new ContactDetailPage(new Contact());
         contactDetailPage.ContactAdded += (source, contact) => _contacts.Add(contact);
         Navigation.PushAsync(contactDetailPage);
      }

      private void OnContactSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (contactsListView.SelectedItem == null)
         {
            return;
         }

         var selectedContact = e.SelectedItem as Contact;
         contactsListView.SelectedItem = null;
         var contactDetailPage = new ContactDetailPage(selectedContact);
         contactDetailPage.ContactUpdated += (src, contact) =>
         {
            selectedContact.Id = contact.Id;
            selectedContact.FirstName = contact.FirstName;
            selectedContact.LastName = contact.LastName;
            selectedContact.Phone = contact.Phone;
            selectedContact.Email = contact.Email;
            selectedContact.IsBlocked = contact.IsBlocked;
         };

         Navigation.PushAsync(contactDetailPage);
      }

      private async void OnContactDeleted(object sender, EventArgs e)
      {
         if ((sender as MenuItem)?.CommandParameter is Contact contact
             && await DisplayAlert("Warning", $"Are you sure you want to delete {contact.FullName}?", "Yes", "No")
                .ConfigureAwait(true))
         {
            _contacts.Remove(contact);
            await _connection.DeleteAsync(contact).ConfigureAwait(true);
         }
      }
   }
}