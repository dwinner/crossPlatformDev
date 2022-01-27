using System;
using Xamarin.Forms;

namespace FileStorageApp.Controls
{
   /// <summary>
   ///    Swipe view
   /// </summary>
   public class GestureView : View
   {
      /// <summary>
      ///    Occurs when swipe left.
      /// </summary>
      public event EventHandler SwipeLeft;

      /// <summary>
      ///    Occurs when swipe right.
      /// </summary>
      public event EventHandler SwipeRight;

      /// <summary>
      ///    Occurs when touch.
      /// </summary>
      public event EventHandler Touch;

      public virtual void OnSwipeLeft() => SwipeLeft?.Invoke(this, EventArgs.Empty);

      public virtual void OnSwipeRight() => SwipeRight?.Invoke(this, EventArgs.Empty);

      public virtual void OnTouch() => Touch?.Invoke(this, EventArgs.Empty);
   }
}