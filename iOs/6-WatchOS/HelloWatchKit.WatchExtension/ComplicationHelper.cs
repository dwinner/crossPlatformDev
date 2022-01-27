using System;
using ClockKit;

namespace HelloWatchKit.WatchExtension
{
   public static class ComplicationHelper
   {
      public static string Answer { get; set; } = string.Empty;

      public static void UpdateComplications()
      {
         var server = CLKComplicationServer.SharedInstance;
         Array.ForEach(server.ActiveComplications, complication => server.ReloadTimeline(complication));
      }
   }
}