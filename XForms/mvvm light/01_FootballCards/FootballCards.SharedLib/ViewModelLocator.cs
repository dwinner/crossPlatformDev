using CommonServiceLocator;
using FootballCards.SharedLib.ViewModel;
using GalaSoft.MvvmLight.Ioc;

namespace FootballCards.SharedLib
{
   public sealed class ViewModelLocator
   {
      // set up keys for the pages
      public const string MainPageKey = "FirstPage";
      public const string MapPageKey = "MapPage";

      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
         SimpleIoc.Default.Register<MainViewModel>();
         SimpleIoc.Default.Register<MapViewModel>();
      }

      /// <summary>
      ///    Main view model
      /// </summary>
      public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

      /// <summary>
      ///    Map view model
      /// </summary>
      public MapViewModel Map => ServiceLocator.Current.GetInstance<MapViewModel>();
   }
}