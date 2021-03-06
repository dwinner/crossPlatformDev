using Alert3.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Alert3
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
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
         SimpleIoc.Default.Register<MainViewModel>();
      }

      public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
   }
}