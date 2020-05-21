using Foundation;
using PorpoiseMobileApp.Client;
using System;
using System.Collections.Generic;
using UIKit;

namespace PorpoiseMobileApp.iOS
{
    public partial class SavedProfilesCell : UICollectionViewCell
    {
        public SavedProfilesCell(IntPtr handle) : base(handle)
        {

        }

        //

        public static readonly NSString Key = new NSString("SavedProfilesCell");
        //public static readonly UINib Nib;

        static UIImage FromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

        internal void UpdateCel(ProfileDetails Saved_profile)
        {

            this.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            this.Layer.BorderWidth = 3f;

            this.Layer.CornerRadius = 20f;

            this.profile_image.Frame = new CoreGraphics.CGRect(0,0,this.Frame.Width,this.Frame.Height);

            profile_name.BackgroundColor = UIColor.FromRGB(246, 194, 96);

            try
            {
                if (!string.IsNullOrEmpty(Saved_profile.image_url))
                {
                    profile_image.Image = FromUrl(Saved_profile.image_url);

                    profile_image.ContentMode = UIViewContentMode.ScaleToFill;
                }
                else
                {
                    profile_image.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                }
            }
            catch(Exception ex)
            {

            }
            profile_name.AttributedText = new NSAttributedString(Saved_profile.first_name + "," + Saved_profile.age, underlineStyle: NSUnderlineStyle.None);

            profile_name.TextAlignment = UITextAlignment.Center;
        }

        public UIImageView ProfileImage
        {

            get { return this.profile_image; }
            set { profile_image = value; }
        }
    }
}