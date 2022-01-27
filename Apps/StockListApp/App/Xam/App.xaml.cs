using StockList.Core.Ioc;
using Xamarin.Forms;

namespace StockListApp.Xam
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

         MainPage = IoC.Resolve<NavigationPage>();
      }
   }
}