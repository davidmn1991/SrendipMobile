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
    [Register ("SavedProfilesViewController")]
    partial class SavedProfilesViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView collection_view { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel saved_profileslbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (back_button != null) {
                back_button.Dispose ();
                back_button = null;
            }

            if (collection_view != null) {
                collection_view.Dispose ();
                collection_view = null;
            }

            if (saved_profileslbl != null) {
                saved_profileslbl.Dispose ();
                saved_profileslbl = null;
            }
        }
    }
}