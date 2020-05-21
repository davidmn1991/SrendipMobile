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
    [Register ("LoginFirstViewController")]
    partial class LoginFirstViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView background_image { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField emailText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel get_helplbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView image_icon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton loginButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton loginFacebookButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView mainView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField passwordText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel signup_lbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (back_button != null) {
                back_button.Dispose ();
                back_button = null;
            }

            if (background_image != null) {
                background_image.Dispose ();
                background_image = null;
            }

            if (emailText != null) {
                emailText.Dispose ();
                emailText = null;
            }

            if (get_helplbl != null) {
                get_helplbl.Dispose ();
                get_helplbl = null;
            }

            if (image_icon != null) {
                image_icon.Dispose ();
                image_icon = null;
            }

            if (loginButton != null) {
                loginButton.Dispose ();
                loginButton = null;
            }

            if (loginFacebookButton != null) {
                loginFacebookButton.Dispose ();
                loginFacebookButton = null;
            }

            if (mainView != null) {
                mainView.Dispose ();
                mainView = null;
            }

            if (passwordText != null) {
                passwordText.Dispose ();
                passwordText = null;
            }

            if (signup_lbl != null) {
                signup_lbl.Dispose ();
                signup_lbl = null;
            }
        }
    }
}