using System;
using Plugin.Messaging;
using Xamarin.Forms.Xaml;

namespace TelephonyApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class MakeCallPage
   {
      public MakeCallPage()
      {
         InitializeComponent();
      }

      private void OnCall(object sender, EventArgs e)
      {
         var phoneDialer = CrossMessaging.Current.PhoneDialer;
         if (phoneDialer.CanMakePhoneCall)
         {
            phoneDialer.MakePhoneCall(phoneNumberEntry.Text);
         }
      }
   }
}