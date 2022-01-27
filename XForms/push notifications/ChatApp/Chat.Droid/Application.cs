using System;
using Android.App;
using Android.Content;
using Android.Runtime;

namespace Chat.Droid
{
   /// <summary>
   ///    Chat application.
   /// </summary>
   [Application]
   public class ChatApplication : Application
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="ChatApplication" /> class.
      /// </summary>
      public ChatApplication()
      {
      }

      /// <summary>
      ///    Initializes a new instance of the <see cref="ChatApplication" /> class.
      /// </summary>
      /// <param name="javaReference">Java reference.</param>
      /// <param name="transfer">Transfer.</param>
      public ChatApplication(IntPtr javaReference, JniHandleOwnership transfer)
         : base(javaReference, transfer)
      {
      }

      /// <summary>
      ///    Gets or sets the presenter.
      /// </summary>
      /// <value>The presenter.</value>
      public object Presenter { get; set; }

      /// <summary>
      ///    Gets or sets the current activity.
      /// </summary>
      /// <value>The current activity.</value>
      public Activity CurrentActivity { get; set; }

      /// <summary>
      ///    Gets the application.
      /// </summary>
      /// <returns>The application.</returns>
      /// <param name="context">Context.</param>
      public static ChatApplication GetApplication(Context context) => (ChatApplication) context.ApplicationContext;
   }
}