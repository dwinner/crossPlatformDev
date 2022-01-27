using System;

namespace BotOnXamarin.Forms.Models
{
   public class BotMessage
   {
      public BotMessage(string activityId, string msg, DateTime time)
      {
         ActivityId = activityId;
         Time = time;
         Content = msg;
      }

      public BotMessage()
      {
      }

      public string ActivityId { get; set; }

      public DateTime Time { get; set; }

      public string Content { get; set; }

      public bool ISent { get; set; }
   }
}