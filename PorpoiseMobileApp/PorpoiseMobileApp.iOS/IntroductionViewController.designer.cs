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

namespace PorpoiseMobileApp.iOS
{
    [Register ("IntroductionViewController")]
    partial class IntroductionViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView background_image { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton login_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton sign_up { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (background_image != null) {
                background_image.Dispose ();
                background_image = null;
            }

            if (login_button != null) {
                login_button.Dispose ();
                login_button = null;
            }

            if (sign_up != null) {
                sign_up.Dispose ();
                sign_up = null;
            }
        }
    }
}