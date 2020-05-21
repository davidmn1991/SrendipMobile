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
    public partial class AddPetViewController : MvvmViewController<AddPetViewModel>
    {
        public AddPetViewController(IntPtr handle) : base(handle)
        {
        }

        public PickingMediaFromGallery pick_photo = new PickingMediaFromGallery();

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationController.NavigationBarHidden = false;
            this.HidesBottomBarWhenPushed = true;

            NavigationBarSetUp();

            addImage.Layer.CornerRadius = addImage.Frame.Size.Height / 2;

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

                    Console.WriteLine(myByteArray.ToString());

                    ViewModel.PetBytes = myByteArray;

                }



                addImage.Image = pick_photo.image;


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


        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            try
            {

                if (NavigationController.NavigationBar != null)
                {

                    this.NavigationController.NavigationBarHidden = true;
                    this.HidesBottomBarWhenPushed = false;
                }
            }
            catch (Exception)
            {

            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            petslbl.TextColor = UIColor.FromRGB(112, 112, 112);

            add_picturelbl.TextColor = UIColor.FromRGB(112, 112, 112);

            var GoBack = new UITapGestureRecognizer(() =>
            {
                try
                {
                    ViewModel.ShowPetList();
                }
                catch (Exception ex)
                {

                }

            });
            petslbl.AddGestureRecognizer(GoBack);

            petslbl.UserInteractionEnabled = true;

            back_button.TouchUpInside += delegate
            {

                ViewModel.ShowPetList();


            };

            addtoList_button.TouchUpInside += delegate
            {

                if (!string.IsNullOrEmpty(addPetName_txt.Text))
                {
                    ViewModel.petName = addPetName_txt.Text;

                    ViewModel.GoToPets();


                }


            };

            addtoList_button.BackgroundColor = UIColor.FromRGB(75, 171, 229);

            var screenTap = new UITapGestureRecognizer(() =>
            {
                addPetName_txt.ResignFirstResponder();

            });

            this.View.AddGestureRecognizer(screenTap);

            addPetName_txt.ShouldReturn = (textField) =>
            {
                addPetName_txt.ResignFirstResponder();
                return true;
            };

            UITapGestureRecognizer photoTapped = new UITapGestureRecognizer(() =>
            {
                Debug.WriteLine("PHOTO TAPPED");
                pick_photo.GetImageStreamAsync(this);
                //profilePicture.Image = pick_photo.image;





            });

            addImage.AddGestureRecognizer(photoTapped);

            addImage.UserInteractionEnabled = true;


        }

        public async void NavigationBarSetUp()
        {
            if (this.NavigationController != null)
            {
                //TODO: figure out how to make the status bar opaque.

                this.NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
                this.NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                this.NavigationController.NavigationBar.ShadowImage = new UIImage();
                this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(100, 170, 224);
            }
            else
            {

            }
            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.Translucent = false;
            float imageSize = 20f;

            float gap = 5f;

            float borderSize = 0f;

            float textHeight = 1f;

            float buttonWidth = 60;

            float buttonHeight = borderSize * 2 + gap * 3 + imageSize + textHeight;

            float imageOrigin = borderSize + gap;

            float textTop = imageOrigin + imageSize + gap;

            float textBottom = borderSize + gap;

            float imageBottom = textBottom + textHeight + gap;

            UIButton rigthButton = UIButton.FromType(UIButtonType.Custom);

            rigthButton.Center = this.View.Center;

            //Image

            UIImage logout = new UIImage("logo.png");

            rigthButton.Frame = new CGRect(0, 15, buttonWidth, buttonHeight + 10);

            rigthButton.SetImage(logout, UIControlState.Normal);

            rigthButton.ImageEdgeInsets = new UIEdgeInsets(0, 15, 0, 10);

            rigthButton.ContentEdgeInsets = new UIEdgeInsets(0, 0, 15, 0);

            rigthButton.SetTitle(Resource.Logout, UIControlState.Normal);

            rigthButton.TitleEdgeInsets = new UIEdgeInsets(textTop, -logout.Size.Width, textBottom, 0.0f);

            rigthButton.TitleLabel.Font = UIFont.FromName("Ubuntu-Light", 15f);

            rigthButton.TouchUpInside += delegate
            {
                var user = NSUserDefaults.StandardUserDefaults;

                user.SetBool(true, "logged");

                //ViewModel.Logout();

            };


            UIImage post = new UIImage("notification_bell.png");

            //UIImage post = new UIImage("HomeIcon.png");

            UIImage right_image = new UIImage("menu_icon.png");

            UIImageView bar_image = new UIImageView(UIImage.FromBundle("DIP_logo.jpg"));

            UIView container = new UIView();

            UIView center_view = new UIView();

            container.Frame = new CGRect(20, 18, this.View.Frame.Width / 14, this.View.Frame.Height / 23);

            UILabel notification = new UILabel();

            //notification.Text = ViewModel.Notificationscount.ToString();

            notification.Font = UIFont.FromName("Helvetica-Bold", 12f);

            notification.TextColor = UIColor.White;

            notification.TextAlignment = UITextAlignment.Center;

            notification.Frame = new CGRect((container.Frame.Width / 2) - ((this.View.Frame.Width / 16) / 2), (container.Frame.Height / 2) - ((this.View.Frame.Height / 23) / 2), this.View.Frame.Width / 16, this.View.Frame.Height / 23);

            container.Layer.CornerRadius = container.Frame.Height / 2;

            //NavigationItem.LeftBarButtonItem = new UIBarButtonItem(notification);

            UIButton leftButton = UIButton.FromType(UIButtonType.Custom);

            UIButton rightButton = UIButton.FromType(UIButtonType.Custom);

            leftButton.UserInteractionEnabled = true;


            //this.View.InsertSubview(new UIImageView(UIImage.FromBundle("Images/bg.jpg")), 0);

            //container.AddSubview(notification);

            //container.BackgroundColor = PorpoiseColors.Magenta;

            leftButton.Bounds = new CGRect(0, 0, post.Size.Width, post.Size.Height);

            leftButton.Frame = new CGRect(0, 0, 30, 30);

            leftButton.SetImage(post, UIControlState.Normal);

            leftButton.SetImage(post, UIControlState.Disabled);

            rightButton.Bounds = new CGRect(0, 0, right_image.Size.Width, right_image.Size.Height);

            rightButton.Frame = new CGRect(0, 0, 30, 30);

            rightButton.SetImage(right_image, UIControlState.Normal);

            rightButton.SetImage(right_image, UIControlState.Disabled);

            var leftButtonWidth = leftButton.WidthAnchor.ConstraintEqualTo(post.Size.Width);

            var leftButtonHeight = leftButton.HeightAnchor.ConstraintEqualTo(post.Size.Height);

            var rightButtonWidth = rightButton.WidthAnchor.ConstraintEqualTo(right_image.Size.Width);

            var rightButtonHeight = rightButton.HeightAnchor.ConstraintEqualTo(right_image.Size.Height);

            leftButtonWidth.Active = true;

            leftButtonHeight.Active = true;

            rightButtonWidth.Active = true;

            rightButtonHeight.Active = true;

            //leftButton.AddSubview(container);


            leftButton.TouchUpInside += delegate


            {
                //ViewModel.ShowSettings();
                Console.WriteLine("pushing here");
                //ViewModel.GoToNotifications();
            };

            rightButton.Hidden = true;


            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(leftButton);

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(rightButton);


            //UILabel label = new UILabel();

            //NavigationItem.LeftBarButtonItem.CustomView.Subviews.Append(label);

            //NavigationItem.Title = "Porpoise";

            NavigationItem.TitleView = new UIImageView(UIImage.FromBundle("DIP_logo.png"));
        }
    }
}