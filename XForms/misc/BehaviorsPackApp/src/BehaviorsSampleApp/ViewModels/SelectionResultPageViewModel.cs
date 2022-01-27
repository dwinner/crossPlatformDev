namespace BehaviorsSampleApp.ViewModels
{
   public class SelectionResultPageViewModel : ViewModelBase
   {
      private Fruit _fruit;

      public Fruit Fruit
      {
         get => _fruit;
         set => SetProperty(ref _fruit, value);
      }
   }
}