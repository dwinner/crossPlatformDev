using ContactMvvmApp.Impl;
using ContactMvvmApp.Interfaces;
using ContactMvvmApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactMvvmApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ContactDetailPage
   {
      public ContactDetailPage(ContactViewModel viewModel)
      {
         InitializeComponent();

         var contactStore = new ContactStoreImpl(DependencyService.Get<ISqLiteDb>());
         var pageService = new PageService();
         BindingContext = new ContactDetailViewModel(viewModel ?? new ContactViewModel(), contactStore, pageService);
      }
   }
}