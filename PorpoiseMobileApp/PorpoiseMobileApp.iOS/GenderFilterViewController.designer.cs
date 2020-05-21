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
    [Register ("GenderFilterViewController")]
    partial class GenderFilterViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel femalelbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel gender_filterlbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel malelbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel otherlbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (back_button != null) {
                back_button.Dispose ();
                back_button = null;
            }

            if (femalelbl != null) {
                femalelbl.Dispose ();
                femalelbl = null;
            }

            if (gender_filterlbl != null) {
                gender_filterlbl.Dispose ();
                gender_filterlbl = null;
            }

            if (malelbl != null) {
                malelbl.Dispose ();
                malelbl = null;
            }

            if (otherlbl != null) {
                otherlbl.Dispose ();
                otherlbl = null;
            }
        }
    }
}