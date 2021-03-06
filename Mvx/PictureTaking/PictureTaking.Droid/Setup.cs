using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform.Platform;
using PictureTaking.Core;

namespace PictureTaking.Droid
{
   public class Setup : MvxAndroidSetup
   {
      public Setup(Context applicationContext) : base(applicationContext)
      {
      }

      protected override IMvxApplication CreateApp() => new App();

      protected override IMvxTrace CreateDebugTrace() => new DebugTrace();
   }
}