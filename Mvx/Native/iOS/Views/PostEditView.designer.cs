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

namespace MvvmCrossDemo.iOS.Views
{
    [Register ("PostEditView")]
    partial class PostEditView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnOk { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtBody { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (btnOk != null) {
                btnOk.Dispose ();
                btnOk = null;
            }

            if (txtBody != null) {
                txtBody.Dispose ();
                txtBody = null;
            }

            if (txtTitle != null) {
                txtTitle.Dispose ();
                txtTitle = null;
            }
        }
    }
}