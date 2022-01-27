using System.ComponentModel;
using ContactMvvmApp.Impl;
using ContactMvvmApp.Interfaces;
using ContactMvvmApp.ViewModel;
using Xamarin.Forms;

namespace ContactMvvmApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class ContactPage
   {
      public ContactPage()
      {
         var contactStore = new ContactStoreImpl(DependencyService.Get<ISqLiteDb>());
         var pageService = new PageService();
         ViewModel = new ContactsPageViewModel(contactStore, pageService);

         InitializeComponent();
      }

      public ContactsPageViewModel ViewModel
      {
         get => BindingContext as ContactsPageViewModel;
         set => BindingContext = value;
      }

      protected override void OnAppearing()
      {
         ViewModel.LoadDataCommand.Execute(null);
         base.OnAppearing();
      }

      private void OnContactSelected(object sender, SelectedItemChangedEventArgs e) =>
         ViewModel.SelectContactCommand.Execute(e.SelectedItem);
   }
}