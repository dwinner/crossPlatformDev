using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotOnXamarin.Forms.Models;
using Microsoft.Bot.Connector.DirectLine;

namespace BotOnXamarin.Forms.Services
{
   public class BotConnectorService
   {
      public const string BOT_HANDLE = "Testera";
      private static bool started;
      private readonly string directLineSecret = "MUVA_6xC3NY.cwA.ipU.V8wKyZcVuqRlgAj2Z1I3u-Jzbry2gY4SHA2VJhzy27c";

      public BotConnectorService() => Client = new DirectLineClient(directLineSecret);

      public static Conversation BotConversation { get; set; }
      public static DirectLineClient Client { get; set; }
      private static string WaterMark { get; set; }
      public event Action<List<BotMessage>> BotMessageReceived;

      public async Task SetUpAsync()
      {
         if (!started)
         {
            BotConversation = await Client.Conversations
               .StartConversationAsync().ConfigureAwait(false);
            started = true;
         }
      }

      /// <summary>
      ///    Get message from the BOT
      /// </summary>
      /// <returns></returns>
      public async Task ReceiveMessageAsync()
      {
         //Send the message to the bot
         var response = await Client.Conversations.GetActivitiesAsync(
            BotConversation.ConversationId, WaterMark).ConfigureAwait(false);

         WaterMark = response.Watermark;

         var activities = new List<Activity>();
         foreach (var activity in response.Activities)
         {
            //Get only the activities corresponding to the Bot.
            if (activity.From.Id == BOT_HANDLE)
            {
               activities.Add(activity);
            }
         }

         //Fire the event for message received.
         BotMessageReceived?.Invoke(CreateBotMessages(activities));
      }

      /// <summary>
      ///    CHange message into Bot message.
      /// </summary>
      /// <param name="activities"></param>
      /// <returns></returns>
      private List<BotMessage> CreateBotMessages(IEnumerable<Activity> activities)
      {
         var botMessages = new List<BotMessage>();

         foreach (var activity in activities)
         {
            botMessages.Add(new BotMessage {ActivityId = activity.Id, Content = activity.Text, ISent = false});
         }

         return botMessages;
      }

      /// <summary>
      ///    Sends a message to the Bot.
      /// </summary>
      /// <param name="messge"></param>
      /// <param name="userName"></param>
      /// <param name="userId"></param>
      /// <returns></returns>
      public async Task SendMessageAsync(string messge, string userName = "")
      {
         var userMesage = new Activity
         {
            From = new ChannelAccount(userName),
            Text = messge,
            Type = ActivityTypes.Message
         };

         await Client.Conversations.PostActivityAsync(BotConversation.ConversationId, userMesage)
            .ConfigureAwait(true);
         //We initiate the process of receiving messages
         //From the Bot.
         await ReceiveMessageAsync();
      }
   }
}