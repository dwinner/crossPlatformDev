using System.Collections.Generic;
using Prism.AppModel;
using Prism.Navigation;
using TransitionApp.Models;

namespace TransitionApp.ViewModels
{
   public class DynamicSampleToViewModel : ViewModelBase, IAutoInitialize
   {
      private List<DogModel> _dogs;

      private DogModel _selectedDog;

      public DynamicSampleToViewModel(INavigationService navigationService) : base(navigationService)
      {
      }

      public List<DogModel> Dogs
      {
         get => _dogs;
         set => SetProperty(ref _dogs, value);
      }

      public DogModel SelectedDog
      {
         get => _selectedDog;
         set => SetProperty(ref _selectedDog, value);
      }
   }
}