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
    [Register ("HomePageViewController")]
    partial class HomePageViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView eye_view { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView header_view { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton next_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView profile_view { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (back_button != null) {
                back_button.Dispose ();
                back_button = null;
            }

            if (eye_view != null) {
                eye_view.Dispose ();
                eye_view = null;
            }

            if (header_view != null) {
                header_view.Dispose ();
                header_view = null;
            }

            if (next_button != null) {
                next_button.Dispose ();
                next_button = null;
            }

            if (profile_view != null) {
                profile_view.Dispose ();
                profile_view = null;
            }
        }
    }
}