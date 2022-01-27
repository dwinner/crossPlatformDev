using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace MvvxSandboxApp.UI.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   [MvxContentPagePresentation(
      WrapInNavigationPage = true,
      NoHistory = false)]
   public partial class UsaStateItemPage
   {
      public UsaStateItemPage()
      {
         InitializeComponent();
      }
   }
}