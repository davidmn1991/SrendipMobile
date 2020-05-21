
using System;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Foundation;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.ViewModels;
using UIKit;

namespace PorpoiseMobileApp.iOS
{
    public partial class Signup4ViewController : MvvmViewController<Signup4ViewModel>
    {
        Signup4ViewController viewController;

        public string hello = "hey you";


        public PickingMediaFromGallery pick_photo = new PickingMediaFromGallery();


        public Signup4ViewController (IntPtr handle) : base (handle)
        {

            

        }

        


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

    
            add_profilelbl.TextColor = UIColor.FromRGB(112, 112, 112);
          

            NavigationController.NavigationBarHidden = true;

            if (!string.IsNullOrEmpty(ViewModel.viewController))
            {
                pointsButton.Image = UIImage.FromBundle("points3.png");

            }


            backButton.TouchUpInside += (sender, e) => {
                ViewModel.BackSignup3();
            };

            nextButton.TouchUpInside += (sender, e) => {
                if(ViewModel.Bytes == null)
                {
                    var okAlertController = UIAlertController.Create("Error message", "Please select profile image", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);

                }
                else
                {
                    ViewModel.ShowSignup5();

                }
                
            };

            profilePicture.UserInteractionEnabled = true;

            

            UITapGestureRecognizer photoTapped = new UITapGestureRecognizer(() =>
            {
                Debug.WriteLine("PHOTO TAPPED");
                pick_photo.GetImageStreamAsync(this);
                //profilePicture.Image = pick_photo.image;

                
                 


            });

            profilePicture.AddGestureRecognizer(photoTapped);







        }





        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            profilePicture.Layer.CornerRadius = profilePicture.Frame.Size.Height / 2;

            if (pick_photo.image != null)
            {


                using (NSData imageData = pick_photo.image.AsPNG())
                {
                    var orientation = pick_photo.image.Orientation;
                    var Orientation_picture = orientation.ToString();
                    Byte[] myByteArray = new Byte[imageData.Length];
                    System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
                    //ViewModel.Bytes = myByteArray;

                    if (!Orientation_picture.Equals("Up"))
                    {
                        UIImage rotated_image = ChangeOrientation(pick_photo.image);
                        using (NSData image_Data = rotated_image.AsPNG())
                        {
                            Byte[] myByte_Array = new Byte[image_Data.Length];
                            System.Runtime.InteropServices.Marshal.Copy(image_Data.Bytes, myByte_Array, 0, Convert.ToInt32(image_Data.Length));
                            
                        }

                    }

                    ViewModel.Bytes = myByteArray;

                }



                profilePicture.Image = pick_photo.image;


            }
        }

        UIImage ChangeOrientation(UIImage rotatedImage)
        {
            float width = rotatedImage.CGImage.Width;
            float height = rotatedImage.CGImage.Height;
            CGImage imgRef = rotatedImage.CGImage;
            CGAffineTransform transform = CGAffineTransform.MakeIdentity();
            CGRect bounds = new CGRect(0, 0, width, height);
            float scaleRatio = (float)(bounds.Size.Width / width);
            CGSize imageSize = new CGSize(imgRef.Width, imgRef.Height);

            var orient = rotatedImage.Orientation;
            switch (orient)
            {

                case UIImageOrientation.Right:
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeTranslation(imageSize.Height, 0.0f);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(Math.PI / 2.0));
                    break;

                case UIImageOrientation.Up: //EXIF = 1
                    transform = CGAffineTransform.MakeIdentity();
                    break;

                case UIImageOrientation.UpMirrored: //EXIF = 2
                    transform = CGAffineTransform.MakeTranslation(imageSize.Width, 0.0f);
                    transform = CGAffineTransform.Scale(transform, -1.0f, 1.0f);
                    break;

                case UIImageOrientation.Down: //EXIF = 3
                    transform = CGAffineTransform.MakeTranslation(imageSize.Width, imageSize.Height);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)Math.PI);
                    break;

                case UIImageOrientation.DownMirrored: //EXIF = 4
                    transform = CGAffineTransform.MakeTranslation(0.0f, imageSize.Height);
                    transform = CGAffineTransform.Scale(transform, 1.0f, -1.0f);
                    break;

                case UIImageOrientation.LeftMirrored: //EXIF = 5
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeTranslation(imageSize.Height, imageSize.Width);
                    transform = CGAffineTransform.Scale(transform, -1.0f, 1.0f);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(3.0 * Math.PI / 2.0));
                    break;

                case UIImageOrientation.Left: //EXIF = 6
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeTranslation(0.0f, imageSize.Width);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(3.0 * Math.PI / 2.0));
                    break;

                case UIImageOrientation.RightMirrored: //EXIF = 7
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(Math.PI / 2.0));
                    break;

            }

            UIGraphics.BeginImageContext(bounds.Size);
            CGContext context = UIGraphics.GetCurrentContext();
            if (orient == UIImageOrientation.Right || orient == UIImageOrientation.Left)
            {
                context.ScaleCTM(-scaleRatio, scaleRatio);
                context.TranslateCTM(-height, 0);
            }
            else
            {
                context.ScaleCTM(scaleRatio, -scaleRatio);
                context.TranslateCTM(0, -height);
            }

            context.ConcatCTM(transform);
            context.DrawImage(new CGRect(0, 0, width, height), imgRef);
            UIImage imageCopy = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            NSData str = imageCopy.AsPNG();

            return imageCopy;

        }
    }
}