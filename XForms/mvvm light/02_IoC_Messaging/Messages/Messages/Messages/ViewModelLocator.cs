using GalaSoft.MvvmLight.Ioc;
using Messages.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Messages
{
   /// <summary>
   ///    This class contains static references to all the view models in the
   ///    application and provides an entry point for the bindings.
   /// </summary>
   public class ViewModelLocator
   {
      public const string MainViewKey = "Main";
      public const string SecondViewKey = "Second";

      /// <summary>
      ///    Initializes a new instance of the ViewModelLocator class.
      /// </summary>
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
         SimpleIoc.Default.Register<SecondViewModel>(true);
         SimpleIoc.Default.Register<MainViewModel>();
      }

      public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

      public SecondViewModel SecondVM => ServiceLocator.Current.GetInstance<SecondViewModel>();
   }
}