using System;
using Xamarin.Forms;

namespace MessagingCenterSampleApp
{
   public class MessagePage : ContentPage
   {
      public MessagePage()
      {
         Title = "Message Page";
         var backBtn = new Button {Text = "Back"};
         backBtn.Clicked += GoToBack;
         Content = new StackLayout {Children = {backBtn}};
      }

      private void GoToBack(object sender, EventArgs e)
      {
         // отправляем сообщение
         MessagingCenter.Send<Page>(this, Messages.LabelChange);
         Navigation.PopAsync();
      }
   }
}