using System;
using GalaSoft.MvvmLight.Views;
using TimeOfDeath.Interfaces;

namespace TimeOfDeath
{
   public class TimeTempViewModel : BaseViewModel
   {
      private readonly ICalculateData calcData;

      private bool inKilos;
      private INavigationService navService;

      public TimeTempViewModel(INavigationService nav, ICalculateData calc)
      {
         navService = nav;
         calcData = calc;
      }

      public DateTime SetDate
      {
         get => calcData.GetData<DateTime>("date");
         set
         {
            calcData.SetData("date", value);
            RaisePropertyChanged();
         }
      }

      public double TempBody
      {
         get => calcData.GetData<double>("tempWeight");
         set
         {
            calcData.SetData("tempWeight", value);
            RaisePropertyChanged("tempWeight");
         }
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

      public double BodyWeight
      {
         get => calcData.GetData<double>("bodyWeight");
         set => calcData.SetData("bodyWeight", value);
      }

      public bool InKilos
      {
         get => inKilos;
         set { Set(() => InKilos, ref inKilos, value); }
      }
   }
}