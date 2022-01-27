// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DroidMethods.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Camera.Portable.Extras;
using Xamarin.Forms;
using Application = Android.App.Application;

namespace Camera.Droid.Extras
{
   /// <summary>
   ///    The android methods interface
   /// </summary>
   public class DroidMethods : IMethods
   {
      #region Public Methods

      /// <summary>
      ///    Exit this instance.
      /// </summary>
      public void Exit()
      {
         Process.KillProcess(Process.MyPid());
      }

      /// <summary>
      ///    Displaies the entry alert.
      /// </summary>
      /// <returns>The entry alert.</returns>
      /// <param name="tcs">Tcs.</param>
      public void DisplayEntryAlert(TaskCompletionSource<string> tcs, string message)
      {
         var context = Application.Context;
         //var context = Forms.Context;
         
         var factory = LayoutInflater.From(context);
         var view = factory.Inflate(Resource.Layout.EntryAlertView, null);

         var editText = view.FindViewById<EditText>(Resource.Id.textEntry);

         new AlertDialog.Builder(context)
            .SetTitle("Chat")
            .SetMessage(message)
            .SetPositiveButton("Ok", (sender, e) => { tcs.SetResult(editText.Text); })
            .SetNegativeButton("Cancel", (sender, e) => { tcs.SetResult(null); })
            .SetView(view)
            .Show();
      }

      #endregion
   }
}