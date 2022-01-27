﻿using System.Collections.Generic;
using Prism.Commands;
using Prism.Navigation;
using TransitionApp.Models;
using TransitionApp.Views;

namespace TransitionApp.ViewModels
{
   public class DynamicSampleFrom2ViewModel : ViewModelBase
   {
      private List<DogModel> _dogs;

      private int _selectedDogId;

      public DynamicSampleFrom2ViewModel(INavigationService navigationService) : base(navigationService)
      {
         NavigateDogCommand = new DelegateCommand<DogModel>(selectedDog =>
         {
            SelectedDogId = selectedDog.Id;

            var navParam = new NavigationParameters {{nameof(selectedDog), selectedDog}};
            navigationService.NavigateAsync($"{nameof(DynamicSampleTo2)}", navParam);
         });
      }

      public List<DogModel> Dogs
      {
         get => _dogs;
         set => SetProperty(ref _dogs, value);
      }

      public int SelectedDogId
      {
         get => _selectedDogId;
         set => SetProperty(ref _selectedDogId, value);
      }

      public DelegateCommand<DogModel> NavigateDogCommand { get; set; }

      public override void OnNavigatedTo(INavigationParameters parameters)
      {
         base.OnNavigatedTo(parameters);

         if (parameters.GetNavigationMode() != NavigationMode.Back)
         {
            var description =
               "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Dogs = new List<DogModel>
            {
               new DogModel {Id = 1, Title = "Christmas Dog", Image = "christmas_dog.jpg", Description = description},
               new DogModel {Id = 2, Title = "Cute Dog", Image = "cute_dog.jpg", Description = description},
               new DogModel {Id = 3, Title = "Lazy Dog", Image = "lazy_dog.jpg", Description = description},
               new DogModel {Id = 4, Title = "What the Dog??!?", Image = "what_the_dog.jpg", Description = description}
            };
         }
      }
   }
}