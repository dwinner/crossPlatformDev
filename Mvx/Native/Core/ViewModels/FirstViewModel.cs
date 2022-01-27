using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.Infrastructure.Messages;
using MvvmCrossDemo.Core.Services;
using Xamarin.Essentials;

namespace MvvmCrossDemo.Core.ViewModels
{
   public class FirstViewModel : MvxViewModel
   {
      private readonly IGreetingService _greetingService;

      private readonly IMvxNavigationService _navigationService;

      // private readonly MvxSubscriptionToken _token; NOTE: this token has unlimited lifetime, would be a weak reference
      private IMvxCommand _getGreetingCommand;
      private string _greeting;
      private string _launchTime;
      private IMvxAsyncCommand _navToPostListAsyncCommand;
      private IMvxAsyncCommand _sendEmailCommand;
      private string _userName;

      public FirstViewModel(IGreetingService greetingService, IMvxNavigationService navigationService,
         IMvxMessenger messenger)
      {
         _greetingService = greetingService;
         _navigationService = navigationService;
         /*_token = */
         messenger.Subscribe<LaunchTimeMessage>(message =>
            LaunchTime = $"The App has launched: {message.TimeSpan.Seconds} seconds");
      }

      public string LaunchTime
      {
         get => _launchTime;
         set => SetProperty(ref _launchTime, value);
      }

      public string UserName
      {
         get => _userName;
         set => SetProperty(ref _userName, value);
      }

      public string Greeting
      {
         get => _greeting;
         set => SetProperty(ref _greeting, value);
      }

      public IMvxCommand GetGreetingCommand =>
         _getGreetingCommand ?? (_getGreetingCommand =
            new MvxCommand(() => Greeting = _greetingService.GetGreetingText(UserName)));

      public IMvxAsyncCommand NavToPostListAsyncCommand => _navToPostListAsyncCommand
                                                           ?? (_navToPostListAsyncCommand =
                                                              new MvxAsyncCommand(() => /*await */
                                                                 _navigationService.Navigate<PostListViewModel>()));

      public IMvxAsyncCommand SendEmailCommand =>
         _sendEmailCommand ?? (_sendEmailCommand = new MvxAsyncCommand(SendEmailAsync));

      private static async Task SendEmailAsync()
      {
         try
         {
            var message = new EmailMessage
            {
               Subject = "Hello, Xamarin!",
               Body = "This is a message from Xamarin.Essemtials.",
               To = new List<string> {"yan_xiaodi@outlook.com"}
            };

            await Email.ComposeAsync(message).ConfigureAwait(false);
         }
         catch (FeatureNotSupportedException)
         {
            // Email is not supported on this device
         }
         catch
         {
            // Some other exception occurred
         }
      }
   }
}