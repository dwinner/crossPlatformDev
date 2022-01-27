using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace MvvxSandboxApp.UI.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   [MvxContentPagePresentation(
      WrapInNavigationPage = true,
      NoHistory = false,
      Title = "Filter states")]
   public partial class FilterStatePage
   {
      public FilterStatePage()
      {
         InitializeComponent();
      }
   }
}