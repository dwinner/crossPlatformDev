using FileStorageApp.IoC;
using Xamarin.Forms;

namespace FileStorageApp
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();

         if (Current.Resources == null)
         {
            Current.Resources = new ResourceDictionary();
         }

         MainPage = IoCConfig.Resolve<NavigationPage>();
      }
   }
}