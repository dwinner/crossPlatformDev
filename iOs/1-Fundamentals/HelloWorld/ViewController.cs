using System;
using System.Diagnostics;
using JetBrains.Annotations;
using UIKit;

namespace HelloWorld
{
   public partial class ViewController : UIViewController
   {
      private const string ApressTitle = "Apress";
      private const string Message = "Hello, Xamarin.iOS!";
      private readonly UIAlertAction _cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
      private readonly UIAlertAction _okAction = UIAlertAction.Create("OK", UIAlertActionStyle.Default, null);
      private readonly Person _person = new Person();

      protected ViewController(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         // Perform any additional setup after loading the view, typically from a nib.
         DisplayInfo(nameof(ViewDidLoad));
      }

      public override void ViewWillAppear(bool animated)
      {
         base.ViewWillAppear(animated);

         DisplayInfo(nameof(ViewWillAppear));
         DisplayPersonData();
      }

      public override void ViewWillDisappear(bool animated)
      {
         base.ViewWillDisappear(animated);

         DisplayInfo(nameof(ViewWillDisappear));
         StorePersonData();
      }

      public override void ViewDidAppear(bool animated)
      {
         base.ViewDidAppear(animated);

         DisplayInfo(nameof(ViewDidAppear));
      }

      public override void ViewDidDisappear(bool animated)
      {
         base.ViewDidDisappear(animated);

         DisplayInfo(nameof(ViewDidAppear));
      }

      [UsedImplicitly]
      partial void ButtonAlert_TouchUpInside(UIButton sender)
      {
         var alertController = UIAlertController.Create(ApressTitle, Message, UIAlertControllerStyle.Alert);
         alertController.AddAction(_okAction);
         PresentViewController(alertController, false, null);
      }

      [UsedImplicitly]
      partial void ButtonActionSheet_TouchUpInside(UIButton sender)
      {
         var actionSheetController = new UIAlertController
         {
            Title = ApressTitle,
            Message = Message
         };

         actionSheetController.AddAction(_okAction);
         actionSheetController.AddAction(_cancelAction);

         PresentViewController(actionSheetController, true, null);
      }

      private static void DisplayInfo(string eventName) => Debug.WriteLine($"View event: {eventName}");

      private void DisplayPersonData()
      {
         _person.RestoreValues();
         TextFieldFirstName.Text = _person.FirstName;
         TextFieldLastName.Text = _person.LastName;
      }

      private void StorePersonData()
      {
         _person.FirstName = TextFieldFirstName.Text;
         _person.LastName = TextFieldLastName.Text;
         _person.StoreValues();
      }
   }
}