using GalaSoft.MvvmLight.Ioc;
using geolocation_plugin.ViewModel;

namespace geolocation_plugin
{
   public class ViewModelLocator
   {
      public const string MainKey = "Main";

      public ViewModelLocator() => SimpleIoc.Default.Register<MainViewModel>();

      public static MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
   }
}