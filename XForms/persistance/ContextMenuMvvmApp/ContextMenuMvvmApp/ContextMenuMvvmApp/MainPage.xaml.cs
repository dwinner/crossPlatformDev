using System.ComponentModel;
using ContextMenuMvvmApp.ViewModels;

namespace ContextMenuMvvmApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
         BindingContext = new PhoneListViewModel();
      }
   }
}