using System;
using ContactBookApp.Interfaces;
using ContactBookApp.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactBookApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ContactDetailPage
   {
      private readonly SQLiteAsyncConnection _connection;

      public ContactDetailPage([Annotations.NotNull] Contact contact)
      {
         if (contact == null)
         {
            throw new ArgumentNullException(nameof(contact));
         }

         InitializeComponent();

         _connection = DependencyService.Get<ISqLiteDb>().GetConnection();

         BindingContext = new Contact
         {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Phone = contact.Phone,
            Email = contact.Email,
            IsBlocked = contact.IsBlocked
         };
      }

      public event EventHandler<Contact> ContactAdded;
      public event EventHandler<Contact> ContactUpdated;

      private async void OnSaved(object sender, EventArgs e)
      {
         var contact = BindingContext as Contact;

         if (string.IsNullOrWhiteSpace(contact.FullName))
         {
            await DisplayAlert("Error", "Please enter the name", "Ok")
               .ConfigureAwait(true);
            return;
         }

         if (contact.Id == 0)
         {
            await _connection.InsertAsync(contact)
               .ConfigureAwait(true);
            OnContactAdded(contact);
         }
         else
         {
            await _connection.UpdateAsync(contact)
               .ConfigureAwait(true);
            OnContactUpdated(contact);
         }

         await Navigation.PopAsync()
            .ConfigureAwait(true);
      }

      protected virtual void OnContactAdded(Contact e) => ContactAdded?.Invoke(this, e);

      protected virtual void OnContactUpdated(Contact e) => ContactUpdated?.Invoke(this, e);
   }
}