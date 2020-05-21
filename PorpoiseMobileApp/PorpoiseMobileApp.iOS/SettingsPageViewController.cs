
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.ViewModels;
using UIKit;

namespace PorpoiseMobileApp.iOS
{
    public partial class SettingsPageViewController : MvvmViewController<SettingsPageViewModel>
    {
        public SettingsPageViewController(IntPtr handle) : base(handle)
        {
        }

        public void styleElements()
        {
            settingslbl.TextColor = UIColor.FromRGB(112, 112, 112);
            incognito_label.TextColor = UIColor.FromRGB(112, 112, 112);
            gender_filter_label.TextColor = UIColor.FromRGB(112, 112, 112);
            saved_profile_label.TextColor = UIColor.FromRGB(112, 112, 112);
            edit_profile_label.TextColor = UIColor.FromRGB(112, 112, 112);
            ignored_profile_label.TextColor = UIColor.FromRGB(112, 112, 112);
            premium_label.TextColor = UIColor.FromRGB(112, 112, 112);
            about_label.TextColor = UIColor.FromRGB(112, 112, 112);
            Faqlbl.TextColor = UIColor.FromRGB(112, 112, 112);
            push_notificationlbl.TextColor = UIColor.FromRGB(112, 112, 112);

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            styleElements();

            var GoBack = new UITapGestureRecognizer(() =>
            {
                try
                {
                    ViewModel.ShowHome();
                }
                catch (Exception ex)
                {

                }

            });
            settingslbl.AddGestureRecognizer(GoBack);

            settingslbl.UserInteractionEnabled = true;

            //all_points_image.SendSubviewToBack(this.View);



            var GenderFilter = new UITapGestureRecognizer(() =>
            {
                ViewModel.ShowGenderFilter();

            });

            var SavedProfiles = new UITapGestureRecognizer(() =>
            {
                ViewModel.ShowSavedProfiles();

            });

            back_button.TouchUpInside += delegate
            {
                ViewModel.ShowHome();

            };

            var ProfileDetails = new UITapGestureRecognizer(() =>
            {
                ViewModel.ShowProfile();

            });

            var Logout = new UITapGestureRecognizer(() =>
            {

                ViewModel.Showlogin();


            });

            logout_button.TouchUpInside +=delegate {

                ViewModel.Showlogin();
            };


            gender_filter_label.AddGestureRecognizer(GenderFilter);
            gender_filter_label.UserInteractionEnabled = true;

            saved_profile_label.AddGestureRecognizer(SavedProfiles);
            saved_profile_label.UserInteractionEnabled = true;

            edit_profile_label.AddGestureRecognizer(ProfileDetails);
            edit_profile_label.UserInteractionEnabled = true;

            //logout_button.AddGestureRecognizer(Logout);
            //logout_button.UserInteractionEnabled = true;

            logout_label.AddGestureRecognizer(Logout);
            logout_label.UserInteractionEnabled = true;
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //this.View.BackgroundColor = UIColor.FromRGB(246, 194, 96);

            NavigationBarSetUpAsync();

            //TabBarController.TabBar.Hidden = true;

            //this.NavigationController.NavigationBar.Hidden = false;
            this.NavigationController.NavigationBarHidden = false;
            UIApplication.SharedApplication.StatusBarHidden = false;


            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;



            //TabBarController.TabBar.UnselectedItemTintColor = UIColor.White;
            //TabBarController.TabBar.SelectedImageTintColor = UIColor.White;
        }

        async Task NavigationBarSetUpAsync()
        {

            if (this.NavigationController != null)
            {
                //TODO: figure out how to make the status bar opaque.

                this.NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
                this.NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                this.NavigationController.NavigationBar.ShadowImage = new UIImage();
                this.NavigationController.NavigationBar.BackgroundColor = PorpoiseColors.Turquoise;
                NavigationController.NavigationBar.Translucent = false;
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
                //ViewModel.ShowHome();
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