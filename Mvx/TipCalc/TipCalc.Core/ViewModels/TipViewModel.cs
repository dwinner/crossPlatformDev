using System.Threading.Tasks;
using MvvmCross.ViewModels;
using TipCalc.Core.Services;

namespace TipCalc.Core.ViewModels
{
   public class TipViewModel : MvxViewModel
   {
      private readonly ICalculationService _calculationService;

      private int _generosity;

      private double _subTotal;

      private double _tip;

      public TipViewModel(ICalculationService calculationService) => _calculationService = calculationService;

      public double SubTotal
      {
         get => _subTotal;
         set
         {
            _subTotal = value;
            RaisePropertyChanged(() => SubTotal);

            Recalcuate();
         }
      }

      public int Generosity
      {
         get => _generosity;
         set
         {
            _generosity = value;
            RaisePropertyChanged(() => Generosity);

            Recalcuate();
         }
      }

      public double Tip
      {
         get => _tip;
         set
         {
            _tip = value;
            RaisePropertyChanged(() => Tip);
         }
      }

      public override async Task Initialize()
      {
         await base.Initialize();

         SubTotal = 100;
         Generosity = 10;
         Recalcuate();
      }

      private void Recalcuate()
      {
         Tip = _calculationService.TipAmount(SubTotal, Generosity);
      }
   }
}