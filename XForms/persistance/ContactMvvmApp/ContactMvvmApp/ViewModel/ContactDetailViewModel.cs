using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ContactMvvmApp.Interfaces;
using ContactMvvmApp.Models;
using ContactMvvmApp.Utils;
using Xamarin.Forms;

namespace ContactMvvmApp.ViewModel
{
   public class ContactDetailViewModel
   {
      private readonly IContactStore _contactStore;
      private readonly IPageService _pageService;

      public ContactDetailViewModel(ContactViewModel viewModel, IContactStore contactStore, IPageService pageService)
      {
         if (viewModel == null)
         {
            throw new ArgumentNullException(nameof(viewModel));
         }

         _pageService = pageService;
         _contactStore = contactStore;

         SaveCommand = new Command(async () => await SaveAsync().ConfigureAwait(true));
         Contact = new Contact
         {
            Id = viewModel.Id,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Phone = viewModel.Phone,
            Email = viewModel.Email,
            IsBlocked = viewModel.IsBlocked
         };
      }

      public Contact Contact { get; }

      public ICommand SaveCommand { get; }

      private async Task SaveAsync()
      {
         if (string.IsNullOrWhiteSpace(Contact.FirstName) &&
             string.IsNullOrWhiteSpace(Contact.LastName))
         {
            await _pageService.DisplayAlertAsync("Error", "Please enter the name.", "OK")
               .ConfigureAwait(true);
            return;
         }

         if (Contact.Id == 0)
         {
            await _contactStore.AddContactAsync(Contact).ConfigureAwait(true);
            MessagingCenter.Send(this, MessageBusNames.ContactAdded, Contact);
         }
         else
         {
            await _contactStore.UpdateContactAsync(Contact).ConfigureAwait(true);
            MessagingCenter.Send(this, MessageBusNames.ContactUpdated, Contact);
         }

         await _pageService.PopAsync().ConfigureAwait(true);
      }
   }
}