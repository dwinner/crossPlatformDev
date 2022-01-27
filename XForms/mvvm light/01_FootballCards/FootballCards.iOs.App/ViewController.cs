using System;
using FootballCards.iOs.App.Model;
using FootballCards.SharedLib.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using UIKit;

namespace FootballCards.iOs.App
{
   public partial class ViewController : UIViewController
   {
      public ViewController(IntPtr handle) : base(handle)
      {
      }

      public MainViewModel ViewModel => AppDelegate.Locator.Main;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         BindLabels();
         BindTextField();
         SetCommands();
      }

      private void BindLabels()
      {
         this.SetBinding(
            () => ViewModel.TeamName,
            () => ClubNameLabel.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.StadiumName,
            () => StadiumLabel.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Capacity,
            () => CapacityLabel.Text,
            BindingMode.TwoWay);
      }

      private void SetCommands()
      {
         // TODO: You can use nameof(ShuffleTeamsButton.TouchUpInside)
         ShuffleTeamsButton.SetCommand(Events.TouchUpInside, ViewModel.ButtonClicked);
         ButtonShowMap.SetCommand(Events.TouchUpInside, ViewModel.ButtonShowMap);
      }

      private void BindTextField()
      {
         this.SetBinding(
               () => ShufflesNumTextField.Text)
            .ObserveSourceEvent(Events.EditingChanged)
            .WhenSourceChanges(
               () => ViewModel.NumberShuffles = Convert.ToInt32(ShufflesNumTextField.Text));
      }
   }
}