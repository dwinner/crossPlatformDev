using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BotOnXamarin.Forms.Models;
using BotOnXamarin.Forms.MVVMShared;
using BotOnXamarin.Forms.Services;
using Xamarin.Forms;

namespace BotOnXamarin.Forms.ViewModels
{
   public class MainViewModel : BindableBase
   {
      private readonly BotConnectorService _botService;
      private ObservableCollection<BotMessage> _botMessages;
      private string _currentMessage;

      public MainViewModel()
      {
         _botService = new BotConnectorService();
         SendCommand = new RelayCommand(async () => await SendMessage(), () => string.IsNullOrEmpty(CurrentMessage));
         BotMessages = new ObservableCollection<BotMessage>();
         _botService.BotMessageReceived += OnBotMessageReceived;
      }

      public RelayCommand SendCommand { get; }

      public ObservableCollection<BotMessage> BotMessages
      {
         get => _botMessages;
         set => SetProperty(ref _botMessages, value);
      }

      public string CurrentMessage
      {
         get => _currentMessage;
         set => SetProperty(ref _currentMessage, value);
      }

      /// <summary>
      ///    Called when the bot sends a message.
      /// </summary>
      /// <param name="msgs"></param>
      private void OnBotMessageReceived(List<BotMessage> msgs)
      {
         foreach (var msg in msgs)
         {
            Device.BeginInvokeOnMainThread(() => { BotMessages.Add(msg); });
         }
      }

      /// <summary>
      ///    Send Messages to the Bot using the BotConnectorService Class
      /// </summary>
      /// <returns></returns>
      private async Task SendMessage()
      {
         try
         {
            await _botService.SetUpAsync();
            var msg = new BotMessage {Content = CurrentMessage, ISent = true, Time = DateTime.Now};
            BotMessages.Add(msg);
            await _botService.SendMessageAsync(CurrentMessage);
            CurrentMessage = string.Empty;
         }
         catch (Exception e)
         {
            await Application.Current.MainPage.DisplayAlert("Alert",
               "Problem connecting. Please check your internet connection.", "OK");
         }
      }
   }
}