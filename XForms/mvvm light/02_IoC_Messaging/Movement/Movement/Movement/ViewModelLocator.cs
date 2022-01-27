using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Movement.ViewModel;

namespace Movement
{
   /// <summary>
   ///    This class contains static references to all the view models in the
   ///    application and provides an entry point for the bindings.
   /// </summary>
   public class ViewModelLocator
   {
      /// <summary>
      ///    Initializes a new instance of the ViewModelLocator class.
      /// </summary>
      public const string MainViewKey = "Main";

      public const string BackViewKey = "Back";

      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
         SimpleIoc.Default.Register<MainViewModel>();
         SimpleIoc.Default.Register<BackViewModel>();
      }

      public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

      public BackViewModel Back => ServiceLocator.Current.GetInstance<BackViewModel>();
   }
}