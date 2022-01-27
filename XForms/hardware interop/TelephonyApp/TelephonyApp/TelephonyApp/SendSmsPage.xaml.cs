using System;
using Plugin.Messaging;
using Xamarin.Forms.Xaml;

namespace TelephonyApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SendSmsPage
   {
      public SendSmsPage() => InitializeComponent();

      private void OnSendSms(object sender, EventArgs e)
      {
         var smsMessenger = CrossMessaging.Current.SmsMessenger;
         if (smsMessenger.CanSendSms && !string.IsNullOrWhiteSpace(phoneNumberEntry.Text)
                                     && !string.IsNullOrWhiteSpace(smsDataEditor.Text))
         {
            smsMessenger.SendSms(phoneNumberEntry.Text, smsDataEditor.Text);
         }
      }
   }
}