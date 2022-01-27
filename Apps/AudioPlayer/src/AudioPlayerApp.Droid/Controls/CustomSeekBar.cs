using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AudioPlayerApp.Droid.Controls
{
   public class CustomSeekBar : SeekBar
   {
      protected CustomSeekBar(IntPtr javaReference, JniHandleOwnership transfer)
         : base(javaReference, transfer)
      {
      }

      public CustomSeekBar(Context context) : base(context)
      {
      }

      public CustomSeekBar(Context context, IAttributeSet attrs)
         : base(context, attrs)
      {
      }

      public CustomSeekBar(Context context, IAttributeSet attrs, int defStyleAttr)
         : base(context, attrs, defStyleAttr)
      {
      }

      public CustomSeekBar(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
         : base(context, attrs, defStyleAttr, defStyleRes)
      {
      }

      /// <summary>
      ///    Occurs when value changed
      /// </summary>
      public event EventHandler ValueChanged;

      public override bool OnTouchEvent(MotionEvent e)
      {
         if (!Enabled)
         {
            return false;
         }

         switch (e.Action)
         {
            // Only fire value change events when the touch is relesed
            case MotionEventActions.Up:
            {
               ValueChanged?.Invoke(this, EventArgs.Empty);
            }
               break;
         }

         // We also want to fire all base motion events
         base.OnTouchEvent(e);

         return true;
      }
   }
}