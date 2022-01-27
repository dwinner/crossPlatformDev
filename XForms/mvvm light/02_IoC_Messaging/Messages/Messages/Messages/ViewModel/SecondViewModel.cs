using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Messages.Models;

namespace Messages.ViewModel
{
   public class SecondViewModel : ViewModelBase
   {
      private string _textData1;
      private string _textData2;
      private string _textData3;
      private string _textData4;

      public SecondViewModel()
      {
         Messenger.Default.Register<NotificationMessage<MessageData>>(this, message =>
         {
            // Gets the message object.
            var data = message.Content;

            // Checks the associated action.
            switch (message.Notification)
            {
               case "SelectData":
                  TextData1 = data.Data1;
                  TextData2 = data.Data2;
                  TextData3 = data.Data3;
                  TextData4 = data.Data4;
                  break;
            }
         });
      }

      public string TextData1
      {
         get => _textData1;
         private set { Set(() => TextData1, ref _textData1, value); }
      }

      public string TextData2
      {
         get => _textData2;
         private set { Set(() => TextData2, ref _textData2, value); }
      }

      public string TextData3
      {
         get => _textData3;
         private set { Set(() => TextData3, ref _textData3, value); }
      }

      public string TextData4
      {
         get => _textData4;
         private set { Set(() => TextData4, ref _textData4, value); }
      }
   }
}