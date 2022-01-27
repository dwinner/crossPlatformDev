using System;
using System.Diagnostics;
using System.Timers;
using Foundation;
using JetBrains.Annotations;
using WatchKit;

namespace HelloWatchKit.WatchExtension
{
   public partial class InterfaceController : WKInterfaceController
   {
      private Timer _timer;

      protected InterfaceController(IntPtr handle) : base(handle)
      {
         // Note: this .ctor should not contain any initialization logic.
      }

      public override void Awake(NSObject context)
      {
         base.Awake(context);

         SetTitle("Hello, watch!");
         ConfigureTimer();
#if DEBUG
         DisplayInfo(nameof(Awake));
#endif
      }

      public override void WillActivate()
      {
         UpdateTimer();
#if DEBUG
         DisplayInfo(nameof(WillActivate));
#endif
      }

      public override void DidDeactivate()
      {
         UpdateTimer(false);
#if DEBUG
         DisplayInfo(nameof(DidDeactivate));
#endif
      }

      public override void DidAppear() => DisplayInfo(nameof(DidAppear));

      public override void WillDisappear() => DisplayInfo(nameof(WillDisappear));

      public override void HandleUserActivity(NSDictionary userActivity)
      {
         if (userActivity != null && userActivity.ContainsKey(GlanceHelper.Key))
         {
            SetTitle(userActivity.ValueForKey(GlanceHelper.Key).ToString());
         }
      }

      [UsedImplicitly]
      partial void SanFranciscoItem_Tapped() => DisplayCityGeolocationController(City.SanFrancisco);

      [UsedImplicitly]
      partial void NewYorkItem_Tapped() => DisplayCityGeolocationController(City.NewYork);

      private void DisplayCityGeolocationController(City city)
      {
         var location = LocationHelper.GetLocationForCity(city);
         PushController(nameof(CityGeolocationController), location);
      }

      private void ConfigureTimer()
      {
         if (_timer == null)
         {
            _timer = new Timer();
            _timer.Elapsed += (sender, e) => { DisplayCurrentTime(); };
            _timer.Interval = 1000;
         }
      }

      private void DisplayCurrentTime()
      {
         var time = $"{DateTime.Now:HH:mm:ss}";
         LabelTime.SetText(time);
      }

      private void UpdateTimer(bool start = true)
      {
         if (start)
         {
            DisplayCurrentTime();
            _timer.Start();
         }
         else
         {
            _timer.Stop();
         }
      }

      [UsedImplicitly]
      partial void ButtonInput_Activated()
      {
         var colors = new[]
         {
            "Red",
            "Green",
            "Blue",
            "Orange",
            "Purple"
         };

         PresentTextInputController(colors, WKTextInputMode.AllowEmoji, DisplayUserResponse);
      }

      private void DisplayUserResponse(NSArray result)
      {
         var answer = result != null && result.Count > 0
            ? result.GetItem<NSObject>(0).ToString()
            : "No answer";

         LabelAnswer.SetText(answer);
         ComplicationHelper.Answer = answer;
         ComplicationHelper.UpdateComplications();
      }

      private void DisplayInfo(string eventName) => Debug.WriteLine($"{Class.Name}, view event: {eventName}");
   }
}