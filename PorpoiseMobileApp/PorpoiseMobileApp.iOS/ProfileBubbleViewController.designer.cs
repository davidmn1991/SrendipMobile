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
    [Register ("ProfileBubbleViewController")]
    partial class ProfileBubbleViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addBubble_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView icon_picture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel tittlelbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtfield { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (addBubble_button != null) {
                addBubble_button.Dispose ();
                addBubble_button = null;
            }

            if (back_button != null) {
                back_button.Dispose ();
                back_button = null;
            }

            if (icon_picture != null) {
                icon_picture.Dispose ();
                icon_picture = null;
            }

            if (tittlelbl != null) {
                tittlelbl.Dispose ();
                tittlelbl = null;
            }

            if (txtfield != null) {
                txtfield.Dispose ();
                txtfield = null;
            }
        }
    }
}