using System;

namespace BehaviorsSampleApp.ViewModels
{
   public class SelectedFruitEventArgs : EventArgs
   {
      public Fruit SelectedFruit { get; set; }
   }
}