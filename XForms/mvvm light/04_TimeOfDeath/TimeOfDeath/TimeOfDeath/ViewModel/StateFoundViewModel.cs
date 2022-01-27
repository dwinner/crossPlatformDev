using System;
using System.Collections.Generic;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using TimeOfDeath.Extensions;
using TimeOfDeath.Helpers;
using TimeOfDeath.Interfaces;

namespace TimeOfDeath
{
   public class StateFoundViewModel : ViewModelBase
   {
      private string bodyCondition;

      private RelayCommand btnCalculateResults;
      private readonly ICalculateData calcData;

      private string foundInAir;

      private string foundInWater;

      private bool foundWater;
      private INavigationService navService;

      private string pulledFromWater;

      public StateFoundViewModel(INavigationService nav, ICalculateData data)
      {
         navService = nav;
         calcData = data;
      }

      public double TempSurround
      {
         get => calcData.GetData<double>("tempSurround");
         set
         {
            calcData.SetData("tempSurround", value);
            RaisePropertyChanged("tempSurround");
         }
      }

      public double TempBody
      {
         get => calcData.GetData<double>("tempBody");
         set
         {
            calcData.SetData("tempBody", value);
            RaisePropertyChanged("tempBody");
         }
      }

      public double BodyWeight
      {
         get => calcData.GetData<double>("bodyWeight");
         set => calcData.SetData("bodyWeight", value);
      }

      public string FoundInWater
      {
         get => foundInWater;
         set
         {
            if (!string.IsNullOrEmpty(value))
            {
               var r = GetFoundWater.IndexOf(value);
               if (r == 2)
               {
                  FoundWater = true;
                  SetPA = 0;
               }
               else
               {
                  FoundWater = false;
               }

               SetA = -2;
               SetW = r;
            }

            Set(() => FoundInWater, ref foundInWater, value);
         }
      }

      private int SetW
      {
         get => calcData.GetData<int>("setW");
         set => calcData.SetData("setW", value);
      }

      public string FoundInAir
      {
         get => foundInAir;
         set
         {
            if (!string.IsNullOrEmpty(value))
            {
               SetA = GetFoundAir.IndexOf(value);
               SetPA = SetW = -1;
            }

            Set(() => FoundInAir, ref foundInAir, value);
         }
      }

      private int SetA
      {
         get => calcData.GetData<int>("setA");
         set => calcData.SetData("setA", value);
      }

      public string BodyCondition
      {
         get => bodyCondition;
         set
         {
            if (!string.IsNullOrEmpty(value))
            {
               var r = GetBodyLayers.IndexOf(value);
               if (r >= 6)
               {
                  SetW = 1;
                  SetA = -2;
                  InWater = true;
                  Air = false;
               }
               else
               {
                  SetW = 0;
                  InWater = false;
                  Air = true;
               }

               SetP = r;
            }

            Set(() => BodyCondition, ref bodyCondition, value);
         }
      }

      private int SetP
      {
         get => calcData.GetData<int>("setP");
         set => calcData.SetData("setP", value);
      }

      public string PulledFromWater
      {
         get => pulledFromWater;
         set
         {
            SetA = -2;
            if (!string.IsNullOrEmpty(value))
            {
               SetPA = GetPulledWater.IndexOf(value);
            }

            Set(() => PulledFromWater, ref pulledFromWater, value);
         }
      }

      private int SetPA
      {
         get => calcData.GetData<int>("setPA");
         set => calcData.SetData("setPA", value);
      }

      public bool FoundWater
      {
         get => foundWater;
         set { Set(() => FoundWater, ref foundWater, value, true); }
      }

      public bool InWater { get; set; }
      public bool Air { get; set; }

      public DateTime TODDate => calcData.GetData<DateTime>("date");

      public string TimeOfDeath => calcData.GetData<DateTime>("timeOfDeath").ToShortTimeString();

      public string DateOfDeath => calcData.GetData<DateTime>("dateOfDeath").ToShortDateString();

      public RelayCommand BtnCalculateResults
      {
         get
         {
            return btnCalculateResults ??
                   (
                      btnCalculateResults = new RelayCommand(
                         async () =>
                         {
                            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                            if (TempBody < TempSurround)
                            {
                               await dialog.ShowError("Time Of Death Error",
                                  "Body temperature must be greater than surrounds",
                                  "OK", null);
                               return;
                            }

                            if (BodyWeight <= 0)
                            {
                               await dialog.ShowError("Time Of Death Error", "The body has to have some weight", "OK",
                                  null);
                               return;
                            }

                            if (TempBody == -999)
                            {
                               await dialog.ShowError("Time Of Death Error", "Temp of surround = null", "OK", null);
                               return;
                            }

                            if (TempSurround == -999)
                            {
                               await dialog.ShowError("Time Of Death Error", "Temp of surround = null", "OK", null);
                               return;
                            }

                            new Calculation(calcData.GetCalcData()).CalcTOD(TODDate);
                            await dialog.ShowMessage("Time of Death result",
                               string.Format("Death occured on or before {0}\nAt or around {1}", DateOfDeath,
                                  TimeOfDeath),
                               "OK", null);
                         }));
         }
      }

      public List<string> GetBodyLayers =>
         new List<string>
         {
            "Dry and naked", "Dry with 1-2 thin layers", "Dry with 2-3 thin layers", "Dry with 3-4 thin layers",
            "Dry with 1-2 thicker layers", "Dry with more thin or thicker layers",
            "Wet and naked", "Wet with 1-2 thin wet layers", "Wet with 2 thicker wet layers",
            "Wet with 2 or more thicker wet layers"
         };

      public List<string> GetFoundAir => new List<string> {"still", "moving", "unknown"};

      public List<string> GetFoundWater => new List<string> {"moving", "still", "pulled from water", "unknown"};

      public List<string> GetPulledWater => new List<string> {"still", "moving"};
   }
}