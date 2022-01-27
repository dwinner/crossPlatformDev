using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvxSandboxApp.Core.Models;
using MvvxSandboxApp.Core.Services.Interfaces;
using MvvxSandboxApp.Core.ViewModelResults;
using MvvxSandboxApp.Core.ViewModels.Core;

namespace MvvxSandboxApp.Core.ViewModels.Action
{
   public class FilterStateViewModel : BaseViewModelResult<ConfirmationResult<ApplyFilterResult>>
   {
      private readonly IMvxNavigationService _navigation;
      private readonly IUsaStateStorage _stateStorage;
      private int _lowerCapitalSinceYear;
      private int _lowerHoodYear;
      private int _maxCapitalSinceYear;
      private int _maxHoodYear;
      private int _minCapitalSinceYear;
      private int _minHoodYear;
      private int _upperCapitalSinceYear;
      private int _upperHoodYear;

      public FilterStateViewModel(IMvxNavigationService aNavigation, IUsaStateStorage aStateStorage)
      {
         _navigation = aNavigation;
         _stateStorage = aStateStorage;
         ApplyFilterCommand = new MvxAsyncCommand(OnApplyFilterAsync);
         ResetFilterCommand = new MvxAsyncCommand(OnResetFilterAsync);
      }

      public IMvxCommand ApplyFilterCommand { get; }

      public IMvxCommand ResetFilterCommand { get; }

      public int MinHoodYear
      {
         get => _minHoodYear;
         set => SetProperty(ref _minHoodYear, value);
      }

      public int MaxHoodYear
      {
         get => _maxHoodYear;
         set => SetProperty(ref _maxHoodYear, value);
      }

      public int MinCapitalSinceYear
      {
         get => _minCapitalSinceYear;
         set => SetProperty(ref _minCapitalSinceYear, value);
      }

      public int MaxCapitalSinceYear
      {
         get => _maxCapitalSinceYear;
         set => SetProperty(ref _maxCapitalSinceYear, value);
      }

      public int LowerHoodYear
      {
         get => _lowerHoodYear;
         set => SetProperty(ref _lowerHoodYear, value);
      }

      public int UpperHoodYear
      {
         get => _upperHoodYear;
         set => SetProperty(ref _upperHoodYear, value);
      }

      public int LowerCapitalSinceYear
      {
         get => _lowerCapitalSinceYear;
         set => SetProperty(ref _lowerCapitalSinceYear, value);
      }

      public int UpperCapitalSinceYear
      {
         get => _upperCapitalSinceYear;
         set => SetProperty(ref _upperCapitalSinceYear, value);
      }

      public override async Task Initialize()
      {
         var states = new List<UsaState>();
         await foreach (var usaState in _stateStorage.GetAllStatesAsync())
         {
            states.Add(usaState);
         }

         // TODO: Always use MvxNotifyTask to track loading of data during init

         MinHoodYear = states.Min(state => state.HoodYear);
         MaxHoodYear = states.Max(state => state.HoodYear);
         MinCapitalSinceYear = states.Min(state => state.CapitalSinceYear);
         MaxCapitalSinceYear = states.Max(state => state.CapitalSinceYear);

         LowerHoodYear = MinHoodYear;
         UpperHoodYear = MaxHoodYear;
         LowerCapitalSinceYear = MinCapitalSinceYear;
         UpperCapitalSinceYear = MaxCapitalSinceYear;
      }

      private async Task OnResetFilterAsync()
      {
         var result = new ConfirmationResult<ApplyFilterResult>
         {
            Entity = ApplyFilterResult.Empty,
            IsConfirmed = false
         };

         await _navigation.Close(this, result).ConfigureAwait(false);
         await Task.CompletedTask.ConfigureAwait(false);
      }

      private async Task OnApplyFilterAsync()
      {
         (int start, int end) hoodYearRange = (LowerHoodYear, UpperHoodYear);
         (int start, int end) caplitalYearRange = (LowerCapitalSinceYear, UpperCapitalSinceYear);
         var applyFilter = new ApplyFilterResult(hoodYearRange, caplitalYearRange);
         var result = new ConfirmationResult<ApplyFilterResult>
         {
            Entity = applyFilter,
            IsConfirmed = true
         };

         await _navigation.Close(this, result).ConfigureAwait(false);
         await Task.CompletedTask.ConfigureAwait(false);
      }
   }
}