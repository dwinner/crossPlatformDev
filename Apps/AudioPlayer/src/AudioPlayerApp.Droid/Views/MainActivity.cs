using Android.App;
using Android.OS;
using Android.Views;
using AudioPlayerApp.Core.Sound;
using AudioPlayerApp.Core.ViewModels;
using AudioPlayerApp.Droid.Sound;
using MvvmCross;

// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace AudioPlayerApp.Droid.Views
{
   [Activity(
      Theme = "@style/AppTheme",
      WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden,
      Label = "Audio player")]
   public class MainActivity : BaseActivity<MainPageViewModel>
   {
      protected override int ActivityLayoutId => Resource.Layout.main;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);
         Mvx.IoCProvider.RegisterType<ISoundHandler, AndroidSoundHandlerImpl>();
      }
   }
}