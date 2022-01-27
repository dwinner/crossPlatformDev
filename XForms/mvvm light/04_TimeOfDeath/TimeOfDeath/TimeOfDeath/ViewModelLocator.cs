using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using TimeOfDeath.Interfaces;
using TimeOfDeath.Services;

namespace TimeOfDeath
{
   /// <summary>
   ///    This class contains static references to all the view models in the
   ///    application and provides an entry point for the bindings.
   /// </summary>
   public class ViewModelLocator
   {
      public const string StateFoundKey = "StateFound";
      public const string TimeTempKey = "TimeTemp";

      /// <summary>
      ///    Initializes a new instance of the ViewModelLocator class.
      /// </summary>
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

         SimpleIoc.Default.Register<StateFoundViewModel>();
         SimpleIoc.Default.Register<TimeTempViewModel>();

         SimpleIoc.Default.Register<ICalculateData, CalcDataService>();
      }

      public StateFoundViewModel StateFound => ServiceLocator.Current.GetInstance<StateFoundViewModel>();

      public TimeTempViewModel TimeTemp => ServiceLocator.Current.GetInstance<TimeTempViewModel>();

      public static void Cleanup()
      {
         // TODO Clear the ViewModels
      }
   }
}