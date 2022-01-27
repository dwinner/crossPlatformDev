using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvxSandboxApp.Core.Models;
using MvvxSandboxApp.Core.Services.Interfaces;
using MvvxSandboxApp.Core.ViewModelResults;
using MvvxSandboxApp.Core.ViewModels.Core;

namespace MvvxSandboxApp.Core.ViewModels.State
{
   public class UsaStateItemViewModel : BaseViewModel<UsaStateViewModel, ViewResult<UsaState>>
   {
      private readonly IUserDialogs _dialog;
      private readonly IMvxNavigationService _navigation;
      private readonly IUsaStateStorage _stateStorage;
      private UsaStateViewModel _currentState;

      public UsaStateItemViewModel(IMvxNavigationService navigation, IUsaStateStorage stateStorage, IUserDialogs dialog)
      {
         _navigation = navigation;
         _stateStorage = stateStorage;
         _dialog = dialog;
         UpdateCommand = new MvxAsyncCommand(OnUpdateAsync);
         DeleteCommand = new MvxAsyncCommand(OnDeleteAsync);
         CloseCommand = new MvxAsyncCommand(OnCloseAsync);
      }

      public UsaStateViewModel CurrentState
      {
         get => _currentState;
         set => SetProperty(ref _currentState, value);
      }

      public IMvxCommand UpdateCommand { get; }

      public IMvxCommand DeleteCommand { get; }

      public IMvxCommand CloseCommand { get; }

      public override void Prepare(UsaStateViewModel parameter)
      {
         CurrentState = parameter;
      }

      private async Task OnCloseAsync()
      {
         var viewResult = new ViewResult<UsaState>
         {
            Entity = null,
            Action = ViewAction.None
         };

         _ = await _navigation.Close(this, viewResult)
            .ConfigureAwait(false);
      }

      private async Task OnDeleteAsync()
      {
         var stateToDelete = _currentState.UsaState;
         if (stateToDelete == null)
         {
            return;
         }

         var success = await _stateStorage.RemoveStateAsync(stateToDelete).ConfigureAwait(false);
         if (!success)
         {
            var alertConfig = new AlertConfig
            {
               Title = "ERROR",
               Message = $"Error while removing {stateToDelete.StateName}",
               OkText = "OK"
            };
            _dialog.Alert(alertConfig);

            return;
         }

         var viewResult = new ViewResult<UsaState>
         {
            Entity = stateToDelete,
            Action = ViewAction.Deleted
         };

         await _navigation.Close(this, viewResult).ConfigureAwait(false);
      }

      private async Task OnUpdateAsync()
      {
         var stateToUpdate = _currentState.UsaState;
         if (stateToUpdate == null)
         {
            return;
         }

         var updateResult = await _stateStorage.UpdateStateAsync(stateToUpdate).ConfigureAwait(false);
         if (!updateResult)
         {
            var alertConfig = new AlertConfig
            {
               Title = "ERROR",
               Message = $"Error while updating {stateToUpdate.StateName}",
               OkText = "OK"
            };
            _dialog.Alert(alertConfig);

            return;
         }

         var viewResult = new ViewResult<UsaState>
         {
            Entity = stateToUpdate,
            Action = ViewAction.Updated
         };

         await _navigation.Close(this, viewResult).ConfigureAwait(false);
      }
   }
}