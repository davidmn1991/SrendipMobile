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
    [Register ("MyResumeViewController")]
    partial class MyResumeViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView add_pdflbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addBubble_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addPdf_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel resumelbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (add_pdflbl != null) {
                add_pdflbl.Dispose ();
                add_pdflbl = null;
            }

            if (addBubble_button != null) {
                addBubble_button.Dispose ();
                addBubble_button = null;
            }

            if (addPdf_button != null) {
                addPdf_button.Dispose ();
                addPdf_button = null;
            }

            if (back_button != null) {
                back_button.Dispose ();
                back_button = null;
            }

            if (resumelbl != null) {
                resumelbl.Dispose ();
                resumelbl = null;
            }
        }
    }
}