using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.Animations;
using Android.Widget;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана заставки
   /// </summary>
   [Activity(Label = "Splash", MainLauncher = true, NoHistory = true)]
   public class SplashScreenActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.SplashScreen);
         RunAnimation();
      }

      protected override void OnResume()
      {
         base.OnResume();
         RunAnimation();
      }

      protected override void OnPause()
      {
         base.OnPause();

         var topTitle = FindViewById<TextView>(Resource.Id.SplashTopTitleTextView);
         topTitle.ClearAnimation();
         var bottomTitle = FindViewById<TextView>(Resource.Id.SplashBottomTitleTextView);
         bottomTitle.ClearAnimation();
         var tableLayout = FindViewById<TableLayout>(Resource.Id.TableLayoutId);
         for (var i = 0; i < tableLayout.ChildCount; i++)
         {
            var rowView = tableLayout.GetChildAt(i);
            rowView.ClearAnimation();
         }
      }

      private void RunAnimation()
      {
         var topTitle = FindViewById<TextView>(Resource.Id.SplashTopTitleTextView);
         var firstFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.FadeIn);
         topTitle.StartAnimation(firstFadeIn);

         var bottomTitle = FindViewById<TextView>(Resource.Id.SplashBottomTitleTextView);
         var secondFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.FadeInAfteDelay);
         secondFadeIn.AnimationEnd += (sender, e) =>
         {
            StartActivity(new Intent(this, typeof(MenuScreenActivity)));
            Finish();
         };
         bottomTitle.StartAnimation(secondFadeIn);

         var spinIn = AnimationUtils.LoadAnimation(this, Resource.Animation.GradualApproach);
         var controller = new LayoutAnimationController(spinIn) {Order = DelayOrder.Normal};
         var tableLayout = FindViewById<TableLayout>(Resource.Id.TableLayoutId);
         for (var i = 0; i < tableLayout.ChildCount; i++)
            if (tableLayout.GetChildAt(i) is TableRow rowView)
               rowView.LayoutAnimation = controller;
      }
   }
}