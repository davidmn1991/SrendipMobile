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
    [Register ("SavedProfilesCell")]
    partial class SavedProfilesCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView profile_image { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel profile_name { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (profile_image != null) {
                profile_image.Dispose ();
                profile_image = null;
            }

            if (profile_name != null) {
                profile_name.Dispose ();
                profile_name = null;
            }
        }
    }
}