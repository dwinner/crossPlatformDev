using MvvxSandboxApp.Core.Models;
using MvvxSandboxApp.Core.ViewModels.Core;

namespace MvvxSandboxApp.Core.ViewModels.State
{
   public class UsaStateViewModel : BaseViewModel
   {
      private bool _isSelected;
      private UsaState _usaState;

      public UsaStateViewModel()
         : this(new UsaState
         {
            StateName = "New state name",
            Abbr = "New abbreaviation",
            HoodYear = 1500,
            Capital = "New capital",
            CapitalSinceYear = 1500
         })
      {
      }

      public UsaStateViewModel(UsaState usaState) => _usaState = usaState;

      public UsaState UsaState
      {
         get => _usaState;
         set
         {
            _usaState = value;
            RaisePropertyChanged(() => UsaState);
            RaisePropertyChanged(() => SummaryName);
         }
      }

      public string SummaryName => $"{UsaState.StateName} [{UsaState.Abbr}]";

      public bool IsSelected
      {
         get => _isSelected;
         set => SetProperty(ref _isSelected, value);
      }
   }
}