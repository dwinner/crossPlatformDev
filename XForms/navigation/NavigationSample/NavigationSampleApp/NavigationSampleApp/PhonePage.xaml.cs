using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavigationSampleApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class PhonePage
   {
      private readonly bool _edited;

      public PhonePage(Phone selectedPhone)
      {
         InitializeComponent();

         Phone = selectedPhone ?? new Phone();
         _edited = selectedPhone != null;
         BindingContext = Phone;
      }

      public Phone Phone { get; set; }

      private async void OnSave(object sender, EventArgs e)
      {
         await Navigation.PopAsync().ConfigureAwait(false);

         if (_edited || !(Application.Current.MainPage is NavigationPage navigationPage))
         {
            return;
         }

         var navigationStack = navigationPage.Navigation.NavigationStack;
         if (navigationStack[navigationPage.Navigation.NavigationStack.Count - 1] is MainPage homePage)
         {
            homePage.AddPhone(Phone);
         }
      }
   }
}