using System;
using System.Collections.Generic;
using FootballCards.SharedLib.Helpers;
using FootballCards.SharedLib.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace FootballCards.SharedLib.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      private RelayCommand _buttonClicked;
      private RelayCommand _buttonShowMap;
      private int _capacity;
      private List<Cards> _footballCards;
      private double _latitude;
      private double _longitude;
      private readonly INavigationService _navigationService;
      private int _numberShuffles;
      private string _stadiumName;
      private string _teamName;
      private string _textNumber;

      public MainViewModel(INavigationService navigation)
      {
         _navigationService = navigation;
         _footballCards = Helpers.Helpers.GenerateCards;
      }

      public string TextNumber
      {
         get => _textNumber;
         set
         {
            Set(() => TextNumber, ref _textNumber, value);
            if (!string.IsNullOrEmpty(_textNumber))
            {
               NumberShuffles = Convert.ToInt32(_textNumber);
            }
         }
      }

      public double Longitude
      {
         get => _longitude;
         set { Set(() => Longitude, ref _longitude, value, true); }
      }

      public double Latitude
      {
         get => _latitude;
         set { Set(() => Latitude, ref _latitude, value, true); }
      }

      public int Capacity
      {
         get => _capacity;
         set { Set(() => Capacity, ref _capacity, value, true); }
      }

      public string StadiumName
      {
         get => _stadiumName;
         set { Set(() => StadiumName, ref _stadiumName, value, true); }
      }

      public string TeamName
      {
         get => _teamName;
         set { Set(() => TeamName, ref _teamName, value, true); }
      }

      /// <summary>
      ///    Number of shuffles
      /// </summary>
      public int NumberShuffles
      {
         get => _numberShuffles;
         set
         {
            Set(() => NumberShuffles, ref _numberShuffles, value, true);
            if (_numberShuffles > 0)
            {
               // enable the click
               _buttonClicked?.RaiseCanExecuteChanged();
            }
         }
      }

      public RelayCommand ButtonClicked
      {
         get
         {
            return _buttonClicked ??
                   (_buttonClicked = new RelayCommand(() =>
                   {
                      // Shuffle the cards number of shuffle times
                      _footballCards = _footballCards.Shuffle(NumberShuffles);

                      // get the first card
                      var topCard = _footballCards[0];

                      // propogate the properties
                      TeamName = topCard.TeamName;
                      StadiumName = topCard.StadiumName;
                      Capacity = topCard.Capacity;
                      Longitude = topCard.Longitude;
                      Latitude = topCard.Latitude;
                   }));
         }
      }

      public RelayCommand ButtonShowMap
      {
         get
         {
            return _buttonShowMap ??
                   (_buttonShowMap = new RelayCommand(() =>
                   {
                      _navigationService.NavigateTo(ViewModelLocator.MapPageKey,
                         new List<double> {Latitude, Longitude});
                   }));
         }
      }
   }
}