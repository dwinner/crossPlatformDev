namespace TipCalc.Core.Services
{
   public class CalculationService : ICalculationService
   {
      public double TipAmount(double subTotal, int generosity) => subTotal * generosity / 100.0;
   }
}