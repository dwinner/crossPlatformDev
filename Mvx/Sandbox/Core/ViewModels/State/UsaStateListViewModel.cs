using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvxSandboxApp.Core.Models;
using MvvxSandboxApp.Core.Services.Interfaces;
using MvvxSandboxApp.Core.ViewModelResults;
using MvvxSandboxApp.Core.ViewModels.Action;
using MvvxSandboxApp.Core.ViewModels.Core;
using MvvxSandboxApp.Core.ViewModels.State;

/*
 TODO: Some suggestions for improvements
   1. Endless loading icon occurs while a user acts pull to refresh;
   2. While loading more items there is no user notification via some loading progress UI;
   3. If there are too many items it would be nice to switch the view to some [1,2,3...n]
      presentation to load only chunck of data;
   4. For more effective "load on demand" results, use IAsyncEnumerable based approach:
		- https://medium.com/@csharpwriter/a-clean-way-to-handle-show-paginated-data-using-iasyncenumerable-db495f7132d7
		- https://www.codeproject.com/Articles/5165551/Easy-ListView-With-Load-more-in-xamarin-forms
	  Take into account that this approach also works without issues on UWP based devices!
 */

namespace SandboxApp.Core.ViewModels.State;

public class UsaStateListViewModel : BaseViewModel
{
   private const int DefaultPageCount = 30;
   private readonly IMvxNavigationService _navigation;
   private readonly int _pageCount = DefaultPageCount;
   private readonly IUsaStateStorage _stateStorage;
   private List<UsaState> _filteredStates;
   private int _pageOffset;
   private UsaStateViewModel _selectedState;
   private MvxObservableCollection<UsaStateViewModel> _states;

   public UsaStateListViewModel(IMvxNavigationService navigation, IUsaStateStorage stateStorage)
   {
      _navigation = navigation;
      _stateStorage = stateStorage;

      UsaStateClickCommand = new MvxAsyncCommand<UsaStateViewModel>(UsaStateClickAsync);
      UsaStateSelectedCommand = new MvxAsyncCommand(UsaStateSelectedAsync);
      ViewSelectedStateCommand = new MvxAsyncCommand(ViewSelectedStateAsync);
      AddNewStateCommand = new MvxAsyncCommand(AddNewStateAsync);
      DeleteStateCommand = new MvxAsyncCommand<UsaStateViewModel>(DeleteStateAsync);
      DeleteSelectedCommand = new MvxAsyncCommand(DeleteSelectedStateAsync);
      ForceSelectCommand = new MvxCommand<UsaStateViewModel>(ForceSelect);
      FetchMoreCommand = new MvxCommand(() =>
      {
         if (_pageOffset == 0)
         {
            return;
         }

         FetchStatesTask = MvxNotifyTask.Create(LoadStatesAsync);
         RaisePropertyChanged(() => FetchStatesTask);
      });
      RefreshStatesCommand = new MvxCommand(RefreshStates);
      ApplyFilterCommand = new MvxAsyncCommand(ApplyFilterAsync);
      SearchStateCommand = new MvxAsyncCommand<string>(SearchStateAsync);
   }

   public MvxNotifyTask LoadStatesTask { get; set; }

   public MvxNotifyTask FetchStatesTask { get; set; }

   public MvxObservableCollection<UsaStateViewModel> States
   {
      get => _states;
      set
      {
         _states = value;
         RaisePropertyChanged(() => States);
      }
   }

   public UsaStateViewModel SelectedState
   {
      get
      {
         if (_selectedState is { IsSelected: false })
         {
            _selectedState.IsSelected = true;
         }

         return _selectedState;
      }
      set
      {
         if (_selectedState != null)
         {
            _selectedState.IsSelected = false;
         }

         SetProperty(ref _selectedState, value);
      }
   }

   public IMvxCommand<UsaStateViewModel> UsaStateClickCommand { get; }

   public IMvxCommand UsaStateSelectedCommand { get; }

   public IMvxCommand ViewSelectedStateCommand { get; }

   public IMvxCommand AddNewStateCommand { get; }

   public IMvxCommand DeleteSelectedCommand { get; }

   public IMvxCommand DeleteStateCommand { get; }

   public IMvxCommand<UsaStateViewModel> ForceSelectCommand { get; }

   public IMvxCommand FetchMoreCommand { get; }

   public IMvxCommand RefreshStatesCommand { get; }

   public IMvxCommand ApplyFilterCommand { get; }

   public IMvxCommand<string> SearchStateCommand { get; }

   public override void Prepare()
   {
      base.Prepare();
      States = new MvxObservableCollection<UsaStateViewModel>();
   }

   public override Task Initialize()
   {
      LoadStatesTask = MvxNotifyTask.Create(LoadStatesAsync);
      return Task.FromResult(0);
   }

   private async Task ApplyFilterAsync()
   {
      var filterCriteria = await _navigation
         .Navigate<FilterStateViewModel, ConfirmationResult<ApplyFilterResult>>()
         .ConfigureAwait(false);

      if (filterCriteria is { IsConfirmed: true })
      {
         var applyFilterResult = filterCriteria.Entity;
         _filteredStates = new List<UsaState>();
         await foreach (var usaState in _stateStorage.GetAllStatesAsync())
         {
            var (hoodStart, hoodEnd) = applyFilterResult.HoodYear;
            var (capitalStart, capitalEnd) = applyFilterResult.CapitalSinceYear;
            if (usaState.HoodYear >= hoodStart
                && usaState.HoodYear <= hoodEnd
                && usaState.CapitalSinceYear >= capitalStart
                && usaState.CapitalSinceYear <= capitalEnd)
            {
               _filteredStates.Add(usaState);
            }
         }

         _pageOffset = 0;
         States.Clear();
         States.AddRange(
            _filteredStates.Skip(_pageOffset).Take(_pageCount)
               .Select(state => new UsaStateViewModel(state)));
      }
      else
      {
         _filteredStates = null;
      }
   }

   private async Task SearchStateAsync(string searchStatePattern)
   {
      States.Clear();

      if (string.IsNullOrEmpty(searchStatePattern))
      {
         var pagedResults = await GetPagedResultsAsync().ConfigureAwait(false);
         States.AddRange(pagedResults.Select(usState => new UsaStateViewModel(usState)));
      }
      else
      {
         var searchResults = new List<UsaStateViewModel>();
         await foreach (var usState in _stateStorage.GetAllStatesAsync())
         {
            if (usState.StateName.StartsWith(searchStatePattern, StringComparison.CurrentCultureIgnoreCase))
            {
               searchResults.Add(new UsaStateViewModel(usState));
            }
         }

         States.AddRange(searchResults);
      }
   }

   private async Task LoadStatesAsync()
   {
      var pagedResults = await GetPagedResultsAsync().ConfigureAwait(false);
      if (_pageOffset == 0)
      {
         States.Clear();
      }

      States.AddRange(pagedResults.Select(state => new UsaStateViewModel(state)));
      _pageOffset += _pageCount;
   }

   private async ValueTask<IEnumerable<UsaState>> GetPagedResultsAsync()
   {
      var pagedResults = _filteredStates != null
         ? _filteredStates.Skip(_pageOffset).Take(_pageCount)
         : await _stateStorage.GetPagedUsaStatesAsync(_pageOffset, _pageCount).ConfigureAwait(false);
      return pagedResults;
   }

   private async Task UsaStateClickAsync(UsaStateViewModel navStateVm)
   {
      // Load related entities
      var stateWithDetail = await _stateStorage.LoadRelatedDetailAsync(navStateVm.UsaState.UsaStateId)
         .ConfigureAwait(false);
      navStateVm.UsaState = stateWithDetail;
      var result = await _navigation
         .Navigate<UsaStateItemViewModel, UsaStateViewModel, ViewResult<UsaState>>(navStateVm)
         .ConfigureAwait(false);

      if (result == null)
      {
         return;
      }

      var affectedState = result.Entity;
      switch (result.Action)
      {
         case ViewAction.None:
            break;

         case ViewAction.Deleted:
            var stateToDeleteVm =
               States.FirstOrDefault(state => state.UsaState.StateName == affectedState.StateName);
            if (stateToDeleteVm != null)
            {
               States.Remove(stateToDeleteVm);
            }

            break;

         case ViewAction.Updated:
            var stateToUpdateVm =
               States.FirstOrDefault(state => state.UsaState.StateName == affectedState.StateName);
            if (stateToUpdateVm != null)
            {
               stateToUpdateVm.UsaState = affectedState;
            }

            break;

         default:
            throw new ArgumentOutOfRangeException(nameof(result.Action));
      }
   }

   private Task UsaStateSelectedAsync()
   {
      Debug.WriteLine(_selectedState != null ? _selectedState.ToString() : "not selected");
      return Task.CompletedTask;
   }

   private async Task ViewSelectedStateAsync()
   {
      if (_selectedState == null)
      {
         return;
      }

      await UsaStateClickAsync(_selectedState).ConfigureAwait(false);
   }

   private async Task AddNewStateAsync()
   {
      var result = await _navigation
         .Navigate<NewUsaStateViewModel, UsaStateViewModel, ConfirmationResult<UsaState>>(
            new UsaStateViewModel())
         .ConfigureAwait(false);

      var newStateVm = new UsaStateViewModel(result.Entity);
      if (result is { IsConfirmed: true, Entity: { } })
      {
         States.Add(newStateVm);
         newStateVm.UsaState = result.Entity;
      }
   }

   private async Task DeleteStateAsync(UsaStateViewModel stateVm)
   {
      var stateToDel = stateVm.UsaState;
      var removedResult = await _stateStorage.RemoveStateAsync(stateToDel).ConfigureAwait(true);
      if (!removedResult)
      {
         return;
      }

      var foundStateVm = States.FirstOrDefault(state => state.UsaState.StateName == stateToDel.StateName);
      if (foundStateVm == null)
      {
         return;
      }

      States.Remove(foundStateVm);
   }

   private async Task DeleteSelectedStateAsync()
   {
      if (_selectedState == null)
      {
         return;
      }

      await DeleteStateAsync(_selectedState).ConfigureAwait(false);
   }

   private void ForceSelect(UsaStateViewModel stateToSelect)
   {
      if (stateToSelect == null)
      {
         return;
      }

      SelectedState = stateToSelect;
   }

   private void RefreshStates()
   {
      _pageOffset = default;
      LoadStatesTask = MvxNotifyTask.Create(LoadStatesAsync);
      RaisePropertyChanged(() => LoadStatesTask);
   }
}