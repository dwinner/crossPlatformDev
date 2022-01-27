using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Xamarin.Essentials;

namespace MvvmCrossDemo.Droid.Views
{
   [MvxActivityPresentation]
   [Activity(Label = "First view")]
   public class FirstView : MvxActivity
   {
      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);
         SetContentView(Resource.Layout.FirstView);
         Platform.Init(this, bundle);
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
         [GeneratedEnum] Permission[] grantResults)
      {
         Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }
   }
}