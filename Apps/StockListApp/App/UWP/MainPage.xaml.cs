using Windows.UI.Xaml.Navigation;
using StockList.Core.Ioc;
using StockList.Core.Modules;
using StockList.Shared;
using StockListApp.UWP.Modules;
using StockListApp.Xam.Modules;
using XamApp = StockListApp.Xam.App;

namespace StockListApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
         InitIoC();
         NavigationCacheMode = NavigationCacheMode.Required;
         LoadApplication(new XamApp());
      }

      private void InitIoC()
      {
         IoC.CreateContainer();
         IoC.RegisterModule(new UwpModule());
         IoC.RegisterModule(new SharedModule(true));
         IoC.RegisterModule(new XamFormsModule());
         IoC.RegisterModule(new PortableModule());
         IoC.StartContainer();
      }
   }
}