// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PorpoiseMobileApp.iOS
{
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        PorpoiseMobileApp.iOS.PorpoiseButton btnLogin { get; set; }


        [Outlet]
        UIKit.UIView ClickHereTextView { get; set; }


        [Outlet]
        UIKit.UITextView ForgotPwText { get; set; }


        [Outlet]
        UIKit.UIView FormContainer { get; set; }


        [Outlet]
        UIKit.UIImageView logoImage { get; set; }


        [Outlet]
        PorpoiseMobileApp.iOS.PorpoiseEditText txtEmail { get; set; }


        [Outlet]
        PorpoiseMobileApp.iOS.PorpoiseEditText txtPassword { get; set; }


        [Outlet]
        UIKit.UIActivityIndicatorView Waiting { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView LoginContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LoginContainer != null) {
                LoginContainer.Dispose ();
                LoginContainer = null;
            }
        }
    }
}