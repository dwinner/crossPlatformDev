using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace MvvxSandboxApp.UI.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   [MvxContentPagePresentation(
      WrapInNavigationPage = true,
      NoHistory = false,
      Title = "New state")]
   public partial class NewUsaStatePage
   {
      public NewUsaStatePage()
      {
         InitializeComponent();
      }
   }
}