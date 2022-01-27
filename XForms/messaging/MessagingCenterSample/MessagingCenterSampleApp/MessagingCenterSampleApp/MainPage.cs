using System;
using Xamarin.Forms;

namespace MessagingCenterSampleApp
{
   public class MainPage : ContentPage
   {
      private readonly Label _stackLabel;

      public MainPage()
      {
         Title = "Main Page";
         var forwardButton = new Button {Text = "Forward"};
         forwardButton.Clicked += GoToForward;
         _stackLabel = new Label
         {
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
         };
         Content = new StackLayout {Children = {forwardButton, _stackLabel}};

         // устанавливаем подписку на сообщения
         Subscribe();
      }

      // установка подписки на сообщения
      private void Subscribe() =>
         MessagingCenter.Subscribe<Page>(
            this, // кто подписывается на сообщения
            Messages.LabelChange, // название сообщения
            sender => { _stackLabel.Text = "Message have been sent"; }); // вызываемое действие

      // Переход вперед на MessagePage
      private void GoToForward(object sender, EventArgs e) => Navigation.PushAsync(new MessagePage());
   }
}