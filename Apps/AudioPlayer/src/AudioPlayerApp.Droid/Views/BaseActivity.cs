using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;

namespace AudioPlayerApp.Droid.Views
{
   public abstract class BaseActivity<TViewModel> : MvxAppCompatActivity<TViewModel>
      where TViewModel : class, IMvxViewModel
   {
      protected abstract int ActivityLayoutId { get; }

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         SetContentView(ActivityLayoutId);
      }
   }
}