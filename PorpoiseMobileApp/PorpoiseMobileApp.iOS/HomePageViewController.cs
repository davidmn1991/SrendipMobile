using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Timers;
using CoreGraphics;
using CoreLocation;
using Foundation;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.ViewModels;
using UIKit;
using Xamarin.Essentials;

namespace PorpoiseMobileApp.iOS
{
    public partial class HomePageViewController : MvvmViewController<HomePageViewModel>
    {
        public HomePageViewController(IntPtr handle) : base(handle)
        {
        }

        Timer timer = new Timer();

        TurnOnLocalitationView LocationView = new TurnOnLocalitationView();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime myDate = DateTime.Now;
            InvokeOnMainThread(() => {
                UploadLocation();

                
            });
        }

        public async Task UploadLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    ViewModel.latitude = location.Latitude;

                    ViewModel.longitude = location.Longitude;

                    ViewModel.UploadLocation();

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }

        public async Task GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    ViewModel.latitude = location.Latitude;

                    ViewModel.longitude = location.Longitude;

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // 1000= 1 second , the timer will run every second  
            timer.Interval = 300000;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();


            var manager = new CLLocationManager();
            manager.AuthorizationChanged += (sender, args) => {
                var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width, this.View.Frame.Height + TabBarController.TabBar.Frame.Height + NavigationController.NavigationBar.Frame.Height);
                Console.WriteLine("Authorization changed to: {0}", args.Status);

                switch (args.Status)
                {

                    case CLAuthorizationStatus.AuthorizedWhenInUse:
                        LocationView.Close();
                        //getLocation();
                        break;

                    case CLAuthorizationStatus.Denied:

                        LocationView = new TurnOnLocalitationView(popup_size);
                        LocationView.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                        Console.WriteLine("No Access");
                        break;


                    default:

                        break;
                }

            };
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                manager.RequestWhenInUseAuthorization();

            profile_view.Layer.BorderColor = UIColor.White.CGColor;

            profile_view.Layer.BorderWidth = 4f;

            var EyeTapped = new UITapGestureRecognizer(() =>
            {
                ViewModel.ShowProfile();

            });

            eye_view.AddGestureRecognizer(EyeTapped);

            eye_view.UserInteractionEnabled = true;

            next_button.TouchUpInside += delegate
            {

                profile_view.Image = null;

                GetLocation();

                ViewModel.GetProfilesDetails();
            };

            back_button.TouchUpInside += delegate
            {

                profile_view.Image = null;

                GetLocation();

                ViewModel.GetProfilesDetails();
            };

            //all_points_image.SendSubviewToBack(this.View);

            //TabBarController.TabBar.RemoveFromSuperview();
        }
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);




        }
        static UIImage FromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //getLocation();

            ViewModel.profile_image = AccountInfo.first_profile_image;

            ViewModel.profile_id = AccountInfo.first_profile_id;

            if (string.IsNullOrEmpty(ViewModel.profile_image))
            {

                GetLocation();

                ViewModel.GetProfilesDetails();
            }
            ViewModel.ForPropertyChange(x => x.ProfileImage, y =>
            {

                try
                {
                    eye_view.UserInteractionEnabled = true;

                    if (!string.IsNullOrEmpty(ViewModel.profile_image))
                    {

                        profile_view.Image = FromUrl(ViewModel.profile_image);
                    }
                    else
                    {
                        profile_view.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                    }
                }
                catch (Exception ex)
                {

                }


            });

            ViewModel.ForPropertyChange(x => x.Message, y =>
            {

                try
                {

                    if (ViewModel.message.Equals("There are no users near you"))
                    {

                        profile_view.Image = UIImage.FromBundle("No_one_arround.png");

                        profile_view.ContentMode = UIViewContentMode.ScaleAspectFill;

                        eye_view.UserInteractionEnabled = false;


                    }
                    else
                    {
                        profile_view.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                    }
                }
                catch (Exception ex)
                {

                }


            });

            try
            {

                if (!string.IsNullOrEmpty(ViewModel.profile_image))
                {

                    profile_view.Image = FromUrl(ViewModel.profile_image);
                }
            }
            catch (Exception ex)
            {

            }

            header_view.BackgroundColor = UIColor.FromRGB(75, 171, 229);

            this.NavigationController.NavigationBarHidden = false;

            if (TabBarController.TabBar != null)
            {
                //this.View.Frame = new CGRect(0, 0, this.View.Frame.Width, this.View.Frame.Height - 49f);
                //TabBarController.TabBar.Frame = new CGRect(0,this.View.Frame.Height-this.TabBarController.TabBar.Frame.Height,this.View.Frame.Width, this.TabBarController.TabBar.Frame.Height);
                Console.WriteLine(TabBarController.TabBar.Frame + this.View.Frame.Height.ToString());

                this.HidesBottomBarWhenPushed = false;
            }
            this.View.BackgroundColor = UIColor.FromRGB(246, 194, 96);

            NavigationBarSetUp();

            //TabBarController.TabBar.UnselectedItemTintColor = UIColor.White;
            //TabBarController.TabBar.SelectedImageTintColor = UIColor.White;
        }

        public async void NavigationBarSetUp()
        {
            if (this.NavigationController != null)
            {
                //TODO: figure out how to make the status bar opaque.

                this.NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
                this.NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                this.NavigationController.NavigationBar.ShadowImage = new UIImage();
                this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(75, 171, 229);
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

            UIButton rigthButton = UIButton.FromType(UIButtonType.Custom);

            //leftButton.UserInteractionEnabled = false;


            //this.View.InsertSubview(new UIImageView(UIImage.FromBundle("Images/bg.jpg")), 0);

            //container.AddSubview(notification);

            //container.BackgroundColor = PorpoiseColors.Magenta;

            leftButton.Bounds = new CGRect(0, 0, post.Size.Width, post.Size.Height);

            leftButton.Frame = new CGRect(0, 0, 30, 30);

            leftButton.SetImage(post, UIControlState.Normal);

            leftButton.SetImage(post, UIControlState.Disabled);

            rigthButton.Bounds = new CGRect(0, 0, right_image.Size.Width, right_image.Size.Height);

            rigthButton.Frame = new CGRect(0, 0, 30, 30);

            rigthButton.SetImage(right_image, UIControlState.Normal);

            rigthButton.SetImage(right_image, UIControlState.Disabled);

            var leftButtonWidth = leftButton.WidthAnchor.ConstraintEqualTo(post.Size.Width);

            var leftButtonHeight = leftButton.HeightAnchor.ConstraintEqualTo(post.Size.Height);

            var rightButtonWidth = rigthButton.WidthAnchor.ConstraintEqualTo(right_image.Size.Width);

            var rightButtonHeight = rigthButton.HeightAnchor.ConstraintEqualTo(right_image.Size.Height);

            leftButtonWidth.Active = true;

            leftButtonHeight.Active = true;

            rightButtonWidth.Active = true;

            rightButtonHeight.Active = true;

            rigthButton.TouchUpInside += delegate
            {
                ViewModel.ShowSettings();

                //ViewModel.Logout();

            };

            rigthButton.UserInteractionEnabled = true;

            //leftButton.AddSubview(container);


            leftButton.TouchUpInside += delegate


            {
                Console.WriteLine("pushing here");
                //ViewModel.GoToNotifications();
            };


            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(leftButton);

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(rigthButton);


            //UILabel label = new UILabel();

            //NavigationItem.LeftBarButtonItem.CustomView.Subviews.Append(label);

            //NavigationItem.Title = "Porpoise";

            NavigationItem.TitleView = new UIImageView(UIImage.FromBundle("DIP_logo.png"));
        }
    }
}