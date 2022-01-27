using System.Diagnostics;
using Xamanimation.Sample.Views;
using Xamarin.Forms;

namespace Xamanimation.Sample
{
   public class App : Application
   {
      public App() => MainPage = new AnimationsShell();

      protected override void OnStart()
      {
         Debug.WriteLine("OnStart");
      }

      protected override void OnSleep()
      {
         Debug.WriteLine("OnSleep");
      }

      protected override void OnResume()
      {
         Debug.WriteLine("OnResume");
      }
   }
}