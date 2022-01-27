using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UIKit;
// ReSharper disable FunctionNeverReturns

namespace BackgroundUpdate
{
   public partial class ViewController : UIViewController
   {
      private bool isTimerActive;

      protected ViewController(IntPtr handle) : base(handle)
      {
      }

      [UsedImplicitly]
      partial void ButtonRunTimer_TouchUpInside(UIButton sender)
      {
         if (!isTimerActive)
         {
            isTimerActive = true;
            Task.Run(UpdateTimerAsync);
         }
      }

#pragma warning disable RECS0135 
      private async Task UpdateTimerAsync()
#pragma warning restore RECS0135 
      {
         while (true)
         {
            InvokeOnMainThread(() => { LabelTime.Text = DateTime.Now.ToLongTimeString(); });
            await Task.Delay(1000).ConfigureAwait(false);
         }
      }
   }
}