// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FootballCards.iOs.App
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonShowMap { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CapacityLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ClubNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ShufflesNumTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ShuffleTeamsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StadiumLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonShowMap != null) {
                ButtonShowMap.Dispose ();
                ButtonShowMap = null;
            }

            if (CapacityLabel != null) {
                CapacityLabel.Dispose ();
                CapacityLabel = null;
            }

            if (ClubNameLabel != null) {
                ClubNameLabel.Dispose ();
                ClubNameLabel = null;
            }

            if (ShufflesNumTextField != null) {
                ShufflesNumTextField.Dispose ();
                ShufflesNumTextField = null;
            }

            if (ShuffleTeamsButton != null) {
                ShuffleTeamsButton.Dispose ();
                ShuffleTeamsButton = null;
            }

            if (StadiumLabel != null) {
                StadiumLabel.Dispose ();
                StadiumLabel = null;
            }
        }
    }
}