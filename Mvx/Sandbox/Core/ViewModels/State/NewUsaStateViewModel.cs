using System.Diagnostics;
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
   public class NewUsaStateViewModel : BaseViewModel<UsaStateViewModel, ConfirmationResult<UsaState>>
   {
      private readonly IUserDialogs _dialog;
      private readonly IMvxNavigationService _navigation;
      private readonly IUsaStateStorage _storeStateSvc;
      private UsaStateViewModel _newState;

      public NewUsaStateViewModel(IUserDialogs dialog, IMvxNavigationService navigation, IUsaStateStorage storeStateSvc)
      {
         _dialog = dialog;
         _navigation = navigation;
         _storeStateSvc = storeStateSvc;
         CloseCommand = new MvxAsyncCommand(OnCloseAsync);
         AddNewStateCommand = new MvxAsyncCommand(OnAddNewStateAsync);
      }

      public IMvxCommand CloseCommand { get; }

      public IMvxCommand AddNewStateCommand { get; }

      public UsaStateViewModel NewState
      {
         get => _newState;
         set => SetProperty(ref _newState, value);
      }

      private async Task OnAddNewStateAsync()
      {
         var newUsaState = NewState.UsaState;
         var confirmation = await _dialog.ConfirmAsync(new ConfirmConfig
         {
            Title = "Add new USA State",
            Message = $"Do you want to add a new state '{newUsaState.StateName}'",
            OkText = "YES",
            CancelText = "NO"
         }).ConfigureAwait(false);

         if (!confirmation)
         {
            return;
         }

         var success = await _storeStateSvc.AddNewStateAsync(newUsaState).ConfigureAwait(false);
         if (!success)
         {
            await _dialog.AlertAsync(new AlertConfig
            {
               Title = "Error",
               Message = "Error on adding",
               OkText = "OK"
            }).ConfigureAwait(false);

            return;
         }

         var cfrmResult = new ConfirmationResult<UsaState>
         {
            Entity = newUsaState,
            IsConfirmed = true
         };

         await _navigation.Close(this, cfrmResult).ConfigureAwait(false);
      }

      private async Task OnCloseAsync()
      {
         await _navigation.Close(this).ConfigureAwait(false);
         Debug.WriteLine(nameof(OnCloseAsync));
      }

      public override void Prepare(UsaStateViewModel parameter)
      {
         NewState = parameter;
      }
   }
}