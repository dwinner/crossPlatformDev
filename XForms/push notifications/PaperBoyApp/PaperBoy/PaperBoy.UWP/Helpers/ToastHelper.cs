using System;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;
using Newtonsoft.Json.Linq;

namespace PaperBoy.UWP.Helpers
{
   public static class ToastHelper
   {
      public static void SendWelcomeToast()
      {
         var payload = new XmlDocument();
         var content = "<toast>" +
                       "<visual>" +
                       "<binding template=\"ToastGeneric\">" +
                       "<image placement=\"appLogoOverride\" hint-crop=\"circle\" src=\"ms-appx:///Assets/pb_toastlogo.png\"/>" +
                       "<text>Welcome to Paperboy!</text>" +
                       "<text>The coolest Xamarin Forms news reader in the Universe!</text>" +
                       "<text placement=\"attribution\">Ata Mahmoud</text>" +
                       "</binding>" +
                       "</visual>" +
                       "</toast>";

         payload.LoadXml(content);

         var toast = new ToastNotification(payload);
         var notifier = ToastNotificationManager.CreateToastNotifier();

         notifier.Show(toast);
      }

      public static void SendNotificationToast(string title, string description)
      {
         var payload = new XmlDocument();

         var content = "<toast>" +
                       "<visual>" +
                       "<binding template=\"ToastGeneric\">" +
                       "<image placement=\"appLogoOverride\" hint-crop=\"circle\" src=\"ms-appx:///Assets/pb_toastlogo.png\"/>" +
                       $"<text>{title}</text>" +
                       $"<text>{description}</text>" +
                       "<text placement=\"attribution\">WintellectNOW</text>" +
                       "</binding>" +
                       "</visual>" +
                       "</toast>";

         payload.LoadXml(content);

         var toast = new ToastNotification(payload);

         var notifier = ToastNotificationManager.CreateToastNotifier();
         notifier.Show(toast);
      }

      public static void RegisterPushListenerTask()
      {
         var taskName = "PushListenerTask";

         foreach (var cur in BackgroundTaskRegistration.AllTasks)
         {
            if (cur.Value.Name == taskName)
            {
               return;
            }
         }

         var builder = new BackgroundTaskBuilder();
         builder.Name = taskName;
         builder.TaskEntryPoint = "Paperboy.UWP.Services.Background.PushListener";
         builder.SetTrigger(new PushNotificationTrigger());

         var task = builder.Register();
      }

      public static void ProcessNotification(PushNotificationReceivedEventArgs args)
      {
         try
         {
            var message = new JObject();

            var title = message["message"].ToString();

            SendNotificationToast("New Favorite added", title);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }
      }
   }
}