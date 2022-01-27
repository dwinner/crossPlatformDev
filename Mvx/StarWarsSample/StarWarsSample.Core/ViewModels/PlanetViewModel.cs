using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using StarWarsSample.Core.Models;
using StarWarsSample.Core.MvxInteraction;
using StarWarsSample.Core.Resources;
using StarWarsSample.Core.Services.Interfaces;
using StarWarsSample.Core.ViewModelResults;

namespace StarWarsSample.Core.ViewModels
{
   public class PlanetViewModel : BaseViewModel<IPlanet, DestructionResult<IPlanet>>
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IUserDialogs _userDialogs;
      private IPlanet _planet;

      public PlanetViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
      {
         _navigationService = navigationService;
         _userDialogs = userDialogs;
         DestroyPlanetCommand = new MvxAsyncCommand(DestroyPlanet);
      }

      // MVVM Properties

      [MvxInject]
      public IMyService MyService { get; set; }

      public IPlanet Planet
      {
         get => _planet;
         set
         {
            _planet = value;
            RaisePropertyChanged(() => Planet);
         }
      }

      public MvxInteraction<DestructionAction> Interaction { get; set; } = new MvxInteraction<DestructionAction>();

      // MVVM Commands
      public IMvxCommand DestroyPlanetCommand { get; }

      // MvvmCross Lifecycle
      public override void Prepare(IPlanet parameter)
      {
         Planet = parameter;
      }

      public override Task Initialize() => Task.FromResult(0);

      // Private methods
      private async Task DestroyPlanet()
      {
         var destroy = await _userDialogs.ConfirmAsync(new ConfirmConfig
         {
            Title = Strings.DestroyPlanet,
            Message = Strings.SirAreYouSureYouWantToDestroyThisPlanet,
            OkText = Strings.Yes,
            CancelText = Strings.No
         }).ConfigureAwait(true);

         if (!destroy)
         {
            return;
         }

         var request = new DestructionAction
         {
            OnDestroyed = () => _navigationService.Close(
               this,
               new DestructionResult<IPlanet>
               {
                  Entity = Planet,
                  Destroyed = true
               })
         };

         Interaction.Raise(request);
      }
   }
}