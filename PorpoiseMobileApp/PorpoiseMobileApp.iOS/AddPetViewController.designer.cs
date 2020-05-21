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
    [Register ("AddPetViewController")]
    partial class AddPetViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel add_picturelbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView addImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField addPetName_txt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addtoList_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back_button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel petslbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (add_picturelbl != null) {
                add_picturelbl.Dispose ();
                add_picturelbl = null;
            }

            if (addImage != null) {
                addImage.Dispose ();
                addImage = null;
            }

            if (addPetName_txt != null) {
                addPetName_txt.Dispose ();
                addPetName_txt = null;
            }

            if (addtoList_button != null) {
                addtoList_button.Dispose ();
                addtoList_button = null;
            }

            if (back_button != null) {
                back_button.Dispose ();
                back_button = null;
            }

            if (petslbl != null) {
                petslbl.Dispose ();
                petslbl = null;
            }
        }
    }
}