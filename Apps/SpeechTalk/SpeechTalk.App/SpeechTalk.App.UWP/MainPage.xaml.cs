using Windows.UI.Xaml.Navigation;
using SpeechTalk.App.IoC;
using SpeechTalk.App.Modules;
using SpeechTalk.App.UWP.Modules;

namespace SpeechTalk.App.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
         InitIoc();
         NavigationCacheMode = NavigationCacheMode.Required;
         LoadApplication(new SpeechTalk.App.App());
      }

      private static void InitIoc()
      {
         IoCConfuguration.CreateContainer();
         IoCConfuguration.RegisterModule(new WinPhoneModule());
         IoCConfuguration.RegisterModule(new PclModule());
         IoCConfuguration.StartContainer();
      }
   }
}