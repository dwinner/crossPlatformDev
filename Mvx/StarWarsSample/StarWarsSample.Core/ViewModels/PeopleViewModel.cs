using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using StarWarsSample.Core.Models;
using StarWarsSample.Core.Services.Interfaces;
using StarWarsSample.Core.ViewModelResults;

namespace StarWarsSample.Core.ViewModels
{
   public class PeopleViewModel : BaseViewModel
   {
      private readonly IMvxNavigationService _navigation;
      private readonly IPeopleService _peopleService;
      private string _nextPage;
      private MvxObservableCollection<Person> _people;

      public PeopleViewModel(IPeopleService peopleService, IMvxNavigationService navigation)
      {
         _peopleService = peopleService;
         _navigation = navigation;
         People = new MvxObservableCollection<Person>();
         PersonSelectedCommand = new MvxAsyncCommand<Person>(PersonSelected);
         FetchPeopleCommand = new MvxCommand(() =>
            {
               if (!string.IsNullOrEmpty(_nextPage))
               {
                  FetchPeopleTask = MvxNotifyTask.Create(LoadPeople);
                  RaisePropertyChanged(() => FetchPeopleTask);
               }
            });
         RefreshPeopleCommand = new MvxCommand(RefreshPeople);
      }

      // MVVM Properties
      public MvxNotifyTask LoadPeopleTask { get; private set; }

      public MvxNotifyTask FetchPeopleTask { get; private set; }

      public MvxObservableCollection<Person> People
      {
         get => _people;
         set
         {
            _people = value;
            RaisePropertyChanged(() => People);
         }
      }

      // MVVM Commands
      public IMvxCommand<Person> PersonSelectedCommand { get; }

      public IMvxCommand FetchPeopleCommand { get; }

      public IMvxCommand RefreshPeopleCommand { get; }

      // MvvmCross Lifecycle
      public override Task Initialize()
      {
         LoadPeopleTask = MvxNotifyTask.Create(LoadPeople);

         return base.Initialize();
      }

      // Private methods
      private async Task LoadPeople()
      {
         var result = await _peopleService.GetPeopleAsync(_nextPage).ConfigureAwait(true);
         if (string.IsNullOrEmpty(_nextPage))
         {
            People.Clear();
            People.AddRange(result.Results.Where(p => !p.Name.Contains("Vader") && !p.Name.Contains("Anakin")));
         }
         else
         {
            People.AddRange(result.Results.Where(p => !p.Name.Contains("Vader") && !p.Name.Contains("Anakin")));
         }

         _nextPage = result.Next;
      }

      private async Task PersonSelected(Person selectedPerson)
      {
         var result = await _navigation.Navigate<PersonViewModel, Person, DestructionResult<Person>>(selectedPerson)
            .ConfigureAwait(true);

         if (result != null && result.Destroyed)
         {
            var person = People.FirstOrDefault(p => p.Name == result.Entity.Name);
            if (person != null)
            {
               People.Remove(person);
            }
         }
      }

      private void RefreshPeople()
      {
         _nextPage = null;

         LoadPeopleTask = MvxNotifyTask.Create(LoadPeople);
         RaisePropertyChanged(() => LoadPeopleTask);
      }
   }
}