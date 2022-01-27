using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using FileStorageApp.Controls;
using FileStorageApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GestureView), typeof(GestureViewRenderer))]

namespace FileStorageApp.Droid.Renderers
{
   public class GestureViewRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<GestureView, LinearLayout>
   {
      private readonly GestureDetector _detector;
      private readonly LinearLayout _layout;
      private readonly GestureListener _listener;

      public GestureViewRenderer(Context context)
         : base(context)
      {
         _listener = new GestureListener();
         _detector = new GestureDetector(Context, _listener);
         _layout = new LinearLayout(Context);
      }

      protected override void OnElementChanged(ElementChangedEventArgs<GestureView> e)
      {
         base.OnElementChanged(e);

         if (e.NewElement == null)
         {
            GenericMotion -= HandleGenericMotion;
            Touch -= HandleTouch;
         }

         if (e.OldElement == null)
         {
            GenericMotion += HandleGenericMotion;
            Touch += HandleTouch;
         }

         if (Element != null)
         {
            _listener.InitCoreSwipeView(Element);
         }

         SetNativeControl(_layout);
      }

      private void HandleTouch(object sender, TouchEventArgs e) => _detector.OnTouchEvent(e.Event);

      private void HandleGenericMotion(object sender, GenericMotionEventArgs e) => _detector.OnTouchEvent(e.Event);

      /// <summary>
      ///    Gesture listener
      /// </summary>
      private sealed class GestureListener : GestureDetector.SimpleOnGestureListener
      {
         private const int SwipeThreshold = 50;
         private const int SwipeVelocityThreshold = 50;
         private GestureView _swipeView;

         internal void InitCoreSwipeView(GestureView swipeView) => _swipeView = swipeView;

         public override bool OnSingleTapUp(MotionEvent e)
         {
            _swipeView.OnTouch();
            return base.OnSingleTapUp(e);
         }

         public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
         {
            try
            {
               var diffY = e2.GetY() - e1.GetY();
               var diffX = e2.GetX() - e1.GetX();

               if (Math.Abs(diffX) > Math.Abs(diffY) && Math.Abs(diffX) > SwipeThreshold
                                                     && Math.Abs(velocityX) > SwipeVelocityThreshold
                                                     && _swipeView != null)
               {
                  if (diffX > 0)
                  {
                     _swipeView.OnSwipeRight();
                  }
                  else
                  {
                     _swipeView.OnSwipeLeft();
                  }
               }
            }
            catch (Exception)
            {
            }

            return base.OnFling(e1, e2, velocityX, velocityY);
         }
      }
   }
}