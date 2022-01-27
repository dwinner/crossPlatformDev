using System;
using Plugin.Messaging;
using Xamarin.Forms.Xaml;

namespace TelephonyApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SendEmailPage
   {
      public SendEmailPage()
      {
         InitializeComponent();
      }

      private void OnSendEmail(object sender, EventArgs e)
      {
         var emailMessenger = CrossMessaging.Current.EmailMessenger;
         if (emailMessenger.CanSendEmail
             && !string.IsNullOrWhiteSpace(emailEntry.Text)
             && !string.IsNullOrWhiteSpace(subjectEntry.Text)
             && !string.IsNullOrWhiteSpace(emailTextEditor.Text))
         {
            emailMessenger.SendEmail(emailEntry.Text, subjectEntry.Text, emailTextEditor.Text);
         }
      }
   }
}