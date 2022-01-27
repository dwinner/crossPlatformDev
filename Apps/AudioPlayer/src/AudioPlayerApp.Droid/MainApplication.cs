using System;
using Android.App;
using Android.Runtime;
using AudioPlayerApp.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace AudioPlayerApp.Droid
{
#if DEBUG
   [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
   public class MainApplication : MvxAppCompatApplication<Setup, App>
   {
      public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
      {
      }
   }
}