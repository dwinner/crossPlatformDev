using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using StarWarsSample.Core.Extensions;
using StarWarsSample.Core.Models;
using StarWarsSample.Core.Services.Interfaces;
using StarWarsSample.Core.ViewModelResults;

namespace StarWarsSample.Core.ViewModels
{
   public class PlanetsViewModel : BaseViewModel
   {
      private readonly IMvxNavigationService _navigation;
      private readonly IPlanetsService _planetsService;
      private string _nextPage;
      private MvxObservableCollection<IPlanet> _planets;

      public PlanetsViewModel(IPlanetsService planetsService, IMvxNavigationService navigation)
      {
         _planetsService = planetsService;
         _navigation = navigation;

         Planets = new MvxObservableCollection<IPlanet>();
         PlanetSelectedCommand = new MvxAsyncCommand<IPlanet>(PlanetSelectedAsync);
         FetchPlanetCommand = new MvxCommand(() =>
         {
            if (!string.IsNullOrEmpty(_nextPage))
            {
               FetchPlanetsTask = MvxNotifyTask.Create(LoadPlanetsAsync);
               RaisePropertyChanged(() => FetchPlanetsTask);
            }
         });
         RefreshPlanetsCommand = new MvxCommand(RefreshPlanets);
      }

      // MVVM Properties
      public MvxNotifyTask LoadPlanetsTask { get; private set; }

      public MvxNotifyTask FetchPlanetsTask { get; private set; }

      public MvxObservableCollection<IPlanet> Planets
      {
         get => _planets;
         set
         {
            _planets = value;
            RaisePropertyChanged(() => Planets);
         }
      }

      // MVVM Commands
      public IMvxCommand<IPlanet> PlanetSelectedCommand { get; }

      public IMvxCommand FetchPlanetCommand { get; }

      public IMvxCommand RefreshPlanetsCommand { get; }

      // MvvmCross Lifecycle
      public override Task Initialize()
      {
         LoadPlanetsTask = MvxNotifyTask.Create(LoadPlanetsAsync);

         return Task.FromResult(0);
      }

      // Private methods
      private async Task LoadPlanetsAsync()
      {
         var result = await _planetsService.GetPlanetsAsync(_nextPage).ConfigureAwait(true);
         var planetsToAdd = new List<IPlanet>();
         for (var i = 0; i < result.Results.Count(); i++)
         {
            if (i % 2 == 0)
            {
               planetsToAdd.Add(result.Results.ElementAt(i).ToPlanet());
            }
            else
            {
               planetsToAdd.Add(result.Results.ElementAt(i).ToPlanet2());
            }
         }

         if (string.IsNullOrEmpty(_nextPage))
         {
            Planets.Clear();
         }

         Planets.AddRange(planetsToAdd);

         _nextPage = result.Next;
      }

      private async Task PlanetSelectedAsync(IPlanet selectedPlanet)
      {
         var result = await _navigation.Navigate<PlanetViewModel, IPlanet, DestructionResult<IPlanet>>(selectedPlanet)
            .ConfigureAwait(true);

         if (result != null && result.Destroyed)
         {
            var planet = Planets.FirstOrDefault(p => p.Name == result.Entity.Name);
            if (planet != null)
            {
               Planets.Remove(planet);
            }
         }
      }

      private void RefreshPlanets()
      {
         _nextPage = null;

         LoadPlanetsTask = MvxNotifyTask.Create(LoadPlanetsAsync);
         RaisePropertyChanged(() => LoadPlanetsTask);
      }
   }
}