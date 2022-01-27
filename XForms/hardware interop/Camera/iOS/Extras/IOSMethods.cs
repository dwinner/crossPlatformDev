// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOSMethods.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Camera.Portable.Extras;
using ObjCRuntime;
using UIKit;

namespace Camera.iOS.Extras
{
   /// <summary>
   ///    The methods interface
   /// </summary>
   public class IOSMethods : IMethods
   {
      #region Public Methods

      /// <summary>
      ///    Exit this instance.
      /// </summary>
      public void Exit()
      {
         UIApplication.SharedApplication.PerformSelector(new Selector("terminateWithSuccess"), null, 0f);
      }

      /// <summary>
      ///    Displaies the entry alert.
      /// </summary>
      /// <returns>The entry alert.</returns>
      /// <param name="tcs">Tcs.</param>
      public void DisplayEntryAlert(TaskCompletionSource<string> tcs, string message)
      {
         var alert = new UIAlertView();
         alert.Title = "Title";
         alert.AddButton("OK");
         alert.AddButton("Cancel");
         alert.Message = message;
         alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
         alert.Clicked += (s, ev) =>
         {
            if (ev.ButtonIndex == 0)
            {
               tcs.SetResult(alert.GetTextField(0).Text);
            }
            else
            {
               tcs.SetResult(null);
            }
         };
         alert.Show();
      }

      #endregion
   }
}