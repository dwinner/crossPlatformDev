using System;

namespace Xamarin.Forms.BehaviorsPack
{
   public class AlertButton : IAlertButton
   {
      public Action Action { get; set; }
      public string Message { get; set; }

      public void OnClicked(object sender, EventArgs eventArgs)
      {
         Action?.Invoke();
      }
   }

   public class AlertButton<T> : IAlertButton
   {
      public T Parameter { get; set; }
      public Action<T> Action { get; set; }
      public string Message { get; set; }

      public void OnClicked(object sender, EventArgs eventArgs)
      {
         Action?.Invoke(Parameter);
      }
   }
}