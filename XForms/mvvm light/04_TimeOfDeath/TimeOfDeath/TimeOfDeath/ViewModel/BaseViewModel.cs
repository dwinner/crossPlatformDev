using GalaSoft.MvvmLight;
using TimeOfDeath.Models;

namespace TimeOfDeath
{
   public class BaseViewModel : ViewModelBase
   {
      private static CalcData calcData;

      public CalcData CalcData
      {
         get => calcData;
         set { Set(() => CalcData, ref calcData, value); }
      }
   }
}