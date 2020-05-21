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
    [Register ("BiggestPetPeeveViewController")]
    partial class BiggestPetPeeveViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addBubble_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView biggestpetPevee_textView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel biggets_petlbl { get; set; }

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

            if (biggestpetPevee_textView != null) {
                biggestpetPevee_textView.Dispose ();
                biggestpetPevee_textView = null;
            }

            if (biggets_petlbl != null) {
                biggets_petlbl.Dispose ();
                biggets_petlbl = null;
            }
        }
    }
}