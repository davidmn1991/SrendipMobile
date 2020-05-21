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
    [Register ("Signup4ViewController")]
    partial class Signup4ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel add_profilelbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton backButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton nextButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView pointsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView profilePicture { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (add_profilelbl != null) {
                add_profilelbl.Dispose ();
                add_profilelbl = null;
            }

            if (backButton != null) {
                backButton.Dispose ();
                backButton = null;
            }

            if (nextButton != null) {
                nextButton.Dispose ();
                nextButton = null;
            }

            if (pointsButton != null) {
                pointsButton.Dispose ();
                pointsButton = null;
            }

            if (profilePicture != null) {
                profilePicture.Dispose ();
                profilePicture = null;
            }
        }
    }
}