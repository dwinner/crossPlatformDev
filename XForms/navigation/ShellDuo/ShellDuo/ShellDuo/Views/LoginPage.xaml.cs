using ShellDuo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellDuo.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class LoginPage : ContentPage
   {
      public LoginPage()
      {
         InitializeComponent();
         BindingContext = new LoginViewModel();
      }
   }
}