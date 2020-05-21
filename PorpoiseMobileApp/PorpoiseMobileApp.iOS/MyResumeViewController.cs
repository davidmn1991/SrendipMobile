using System;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Foundation;
using Plugin.FilePicker;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.ViewModels;
using UIKit;


namespace PorpoiseMobileApp.iOS
{
    public partial class MyResumeViewController : MvvmViewController<MyResumeViewModel>
    {
        public MyResumeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            resumelbl.TextColor = UIColor.FromRGB(112, 112, 112);
            this.NavigationController.NavigationBarHidden = false;
            this.HidesBottomBarWhenPushed = true;

            NavigationBarSetUp();
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

            

            addPdf_button.TouchUpInside += async delegate
            {


                var pickedFile = await CrossFilePicker.Current.PickFile();

                if (pickedFile != null)
                {
                    ViewModel.file_name = pickedFile.FileName;

                    ViewModel.file_path = pickedFile.FilePath;

                    ViewModel.file_content = pickedFile.DataArray;

                    Console.WriteLine(pickedFile.FileName);

                    Console.WriteLine(pickedFile.FilePath);

                    if (pickedFile.FileName.EndsWith("png") || pickedFile.FileName.EndsWith("jpg"))
                    {

                    }
                    //FileImagePreview.Image = NSImage.FromStream(pickedFile.GetStream());
                }
                else
                {

                }



            };

            addBubble_button.TouchUpInside += delegate
            {

                try
                {
                    if (!ViewModel.file_content.Equals(null))
                    {
                        ViewModel.UploadFile();
                    }


                }
                catch (Exception ex)
                {

                }


            };

            var ShowProfile2 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    CrossFilePicker.Current.Equals(null);
                }
                catch (Exception ex)
                {

                }

            });

            var GoBack = new UITapGestureRecognizer(() =>
            {
                try
                {
                    ViewModel.ShowBubbleBoard();
                }
                catch (Exception ex)
                {

                }

            });
            resumelbl.AddGestureRecognizer(GoBack);

            resumelbl.UserInteractionEnabled = true;

            back_button.TouchUpInside += delegate
            {

                ViewModel.ShowBubbleBoard();


            };

            this.View.AddGestureRecognizer(ShowProfile2);

            addBubble_button.BackgroundColor = UIColor.FromRGB(75, 171, 229);
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