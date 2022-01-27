using GalaSoft.MvvmLight.Ioc;
using SQLiteExample.ViewModel;

namespace SQLiteExample
{
   /// <summary>
   ///    This class contains static references to all the view models in the
   ///    application and provides an entry point for the bindings.
   /// </summary>
   public class ViewModelLocator
   {
      public const string MainKey = nameof(MainVm);
      public const string AddPetsKey = nameof(AddPetsVm);
      public const string AddHobbiesKey = nameof(AddHobbiesVm);
      public const string ShowDataKey = nameof(ShowDataVm);

      /// <summary>
      ///    Initializes a new instance of the ViewModelLocator class.
      /// </summary>
      public ViewModelLocator()
      {
         SimpleIoc.Default.Register<MainViewModel>();
         SimpleIoc.Default.Register<AddPetViewModel>();
         SimpleIoc.Default.Register<AddHobbiesViewModel>();
         SimpleIoc.Default.Register<ShowDataViewModel>();
      }

      public MainViewModel MainVm => SimpleIoc.Default.GetInstance<MainViewModel>();

      public AddPetViewModel AddPetsVm => SimpleIoc.Default.GetInstance<AddPetViewModel>();

      public AddHobbiesViewModel AddHobbiesVm => SimpleIoc.Default.GetInstance<AddHobbiesViewModel>();

      public ShowDataViewModel ShowDataVm => SimpleIoc.Default.GetInstance<ShowDataViewModel>();
   }
}