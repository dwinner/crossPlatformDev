using BotOnXamarin.Forms.Models;
using BotOnXamarin.Forms.Views.DataTemplates;
using Xamarin.Forms;

namespace BotOnXamarin.Forms.Views
{
   public class ChatDataTemplateSelector : DataTemplateSelector
   {
      private readonly DataTemplate MessageFromBotTemplate;

      private readonly DataTemplate MessageFromUserTemplate;

      public ChatDataTemplateSelector()
      {
         MessageFromUserTemplate = new DataTemplate(typeof(SentByUser));
         MessageFromBotTemplate = new DataTemplate(typeof(SentByBot));
      }

      protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
      {
         var msg = item as BotMessage;
         DataTemplate template = null;

         if (msg == null)
         {
            return null;
         }

         if (msg.ISent)
         {
            template = MessageFromUserTemplate;
         }
         else
         {
            template = MessageFromBotTemplate;
         }

         return template;
      }
   }
}