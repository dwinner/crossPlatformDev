using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InfiniteScrollDemo.ViewModels
{
   public class AboutViewModel : BaseViewModel
   {
      public AboutViewModel()
      {
         Title = "About";
         var openUrlTask = Launcher.OpenAsync(new Uri("https://xamarin.com/platform"));
         OpenWebCommand = new Command(async () => await openUrlTask.ConfigureAwait(true));
      }

      public ICommand OpenWebCommand { get; }
   }
}