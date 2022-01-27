using BotOnXamarin.Forms.Services;
using BotOnXamarin.Forms.ViewModels;
using Xamarin.Forms;

namespace BotOnXamarin.Forms.Views
{
   public partial class MainPage : ContentPage
   {
      private BotConnectorService service;

      public MainPage()
      {
         InitializeComponent();
         BindingContext = new MainViewModel();
      }
   }
}