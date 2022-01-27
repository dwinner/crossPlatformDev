using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.App;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using PaperBoy.Common;
using PaperBoy.Data;
using PaperBoy.Droid.Helpers;
#pragma warning disable 618

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]

namespace PaperBoy.Droid
{
   [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
   [IntentFilter(new[] {Constants.INTENT_FROM_GCM_MESSAGE}, Categories = new[] {"@PACKAGE_NAME@"})]
   [IntentFilter(new[] {Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK}, Categories = new[] {"@PACKAGE_NAME@"})]
   [IntentFilter(new[] {Constants.INTENT_FROM_GCM_LIBRARY_RETRY}, Categories = new[] {"@PACKAGE_NAME@"})]
   public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
   {
      public static string[] SenderIds = {CoreConstants.GcmRemoteId};
   }

   [Service]
   public class GcmService : GcmServiceBase
   {
      public GcmService()
         : base(PushHandlerBroadcastReceiver.SenderIds)
      {
      }

      public static string RegistrationId { get; private set; }

      protected override void OnUnRegistered(Context context, string registrationId)
      {
      }

      protected override void OnError(Context context, string errorId)
      {
      }

      protected override void OnMessage(Context context, Intent intent)
      {
         var msg = new StringBuilder();

         if (intent?.Extras != null)
         {
            foreach (var key in intent.Extras.KeySet())
            {
               msg.AppendLine($"{key}={intent.Extras.Get(key)}");
            }
         }

         var title = intent.Extras.Get("message").ToString();

         //Store the message
         var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
         var edit = prefs.Edit();
         edit.PutString("last_msg", msg.ToString());
         edit.Commit();

         CreateNotification(title, "A new Favorite has been added");
      }

      private async void CreateNotification(string title, string description)
      {
         var notificationManager = GetSystemService(NotificationService) as NotificationManager;

         var uiIntent = new Intent(this, typeof(MainActivity));

         var builder = new NotificationCompat.Builder(this);

         var options = new BitmapFactory.Options
         {
            InJustDecodeBounds = true
         };

         var largeIcon = await BitmapFactory.DecodeResourceAsync(Resources, Resource.Drawable.icon, options)
            .ConfigureAwait(true);

         ToastHelper.ProcessNotification(this, notificationManager, uiIntent, builder, title, description, largeIcon);
      }

      protected override void OnRegistered(Context context, string registrationId)
      {
         RegistrationId = registrationId;

         var push = FavoriteManager.DefaultManager.CurrentClient.GetPush();

         MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
      }

      public async void Register(Push push, IEnumerable<string> tags)
      {
         try
         {
            const string templateBodyGcm = "{\"data\":{\"message\":\"$(messageParam)\"}}";

            var templates = new JObject();
            templates["genericMessage"] = new JObject
            {
               {"body", templateBodyGcm}
            };

            await push.RegisterAsync(RegistrationId, templates).ConfigureAwait(true);
         }
         catch (Exception)
         {
            // ignored
         }
      }
   }
}