using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
    public partial class AllProfilesViewController : MvvmViewController<AllProfilesViewModel>, ICLLocationManagerDelegate
    {
        public AllProfilesViewController (IntPtr handle) : base (handle)
        {
        }

        LoadingOverlay loading_View;

        TurnOnLocalitationView LocationView = new TurnOnLocalitationView();



        private readonly CLLocationManager locationManager = new CLLocationManager();

        public CLLocationManager lm = new CLLocationManager();

        void styleElements()
        {

            image_profile_1.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_2.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_3.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_4.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_5.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_6.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_7.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_8.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_9.Layer.CornerRadius = image_profile_1.Frame.Size.Height / 2;

            image_profile_1.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_1.Layer.BorderWidth = 4f;

            image_profile_2.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_2.Layer.BorderWidth = 4f;

            image_profile_3.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_3.Layer.BorderWidth = 4f;

            image_profile_4.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_4.Layer.BorderWidth = 4f;

            image_profile_5.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_5.Layer.BorderWidth = 4f;

            image_profile_6.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_6.Layer.BorderWidth = 4f;

            image_profile_7.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_7.Layer.BorderWidth = 4f;

            image_profile_8.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_8.Layer.BorderWidth = 4f;

            image_profile_9.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            image_profile_9.Layer.BorderWidth = 4f;

            home_page.SetTitleColor(UIColor.FromRGB(75, 171, 229),UIControlState.Normal);

            var attrString = new NSMutableAttributedString(home_page.TitleLabel.Text);
            attrString.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), new NSRange(0, attrString.Length));
            home_page.TitleLabel.AttributedText = attrString;

        }

       /* [Export("locationManager:didUpdateLocations:")]
        private void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            var location = locations.LastOrDefault();
            if (location != null)
            {

                Console.WriteLine(location.Coordinate.Latitude +","+ location.Coordinate.Longitude);
                ViewModel.GetProfiles();

                //altitudeLabel.Text = $"{location.Altitude} meters";
                //longitudeLabel.Text = $"{location.Coordinate.Longitude}º";
                //latitudeLabel.Text = $"{location.Coordinate.Latitude}º";
                //courseLabel.Text = $"{location.Course}º";
                //speedLabel.Text = $"{location.Speed} meters/s";

                // get the distance from here to paris
                //distanceLabel.Text = $"{location.DistanceFrom(new CLLocation(48.857, 2.351)) / 1000} km";
            }
            else
            {

            }
        }*/

            public async Task getLocation()
            {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    ViewModel.latitude = location.Latitude;

                    ViewModel.longitude = location.Longitude;

                    ViewModel.GetProfiles();

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


        public void loadingpage()
        {
            var bounds = UIScreen.MainScreen.Bounds;
            // show the loading overlay on the UI thread using the correct orientation sizing
            loading_View = new LoadingOverlay(bounds); // using field from step 2
            this.View.Add(loading_View);

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //getLocation();

            home_page.TouchUpInside +=delegate {

                ViewModel.ShowProfile();
	

	        };



            var manager = new CLLocationManager();
            manager.AuthorizationChanged += (sender, args) => {
                var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width, this.View.Frame.Height);
                Console.WriteLine("Authorization changed to: {0}", args.Status);

                switch (args.Status)
                {

                    case CLAuthorizationStatus.AuthorizedWhenInUse:

                        LocationView.Close();

                        getLocation();

                        break;

                    case CLAuthorizationStatus.Denied:
                        
                        LocationView = new TurnOnLocalitationView(popup_size);
                        ViewModel.allprofiles.Clear();
                        LocationView.PopUp(true, delegate
                        {
                            Console.WriteLine("OPEN SETTINGS");
                        });
                        Console.WriteLine("No Access");
                        break;


                    default:

                        break;
                }

            };
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                manager.RequestWhenInUseAuthorization();



            /*switch (CLLocationManager.Status)
            {
                case CLAuthorizationStatus.Authorized | CLAuthorizationStatus.AuthorizedAlways | CLAuthorizationStatus.AuthorizedWhenInUse:
                    Console.WriteLine("Access");
                    break;
                case CLAuthorizationStatus.Denied | CLAuthorizationStatus.NotDetermined | CLAuthorizationStatus.Restricted:
                    Console.WriteLine("No Access");
                    break;
                default:
                    Console.WriteLine("No Access");
                    break;
            }*/


            var ShowProfile1 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[0].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[0].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[0].id;
                        ViewModel.ShowProfile();
                    }
                }
                catch(Exception ex)
                {

                }

            });



            var ShowProfile2 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[1].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[1].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[1].id;
                        ViewModel.ShowProfile();
                    }
                }
                catch(Exception ex)
                {

                }

            });

            var ShowProfile3 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[2].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[2].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[2].id;
                        ViewModel.ShowProfile();
                    }
                }
                catch(Exception ex)
                {

                }

            });

            var ShowProfile4 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[3].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[3].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[3].id;
                        ViewModel.ShowProfile();
                    }
                }
                catch (Exception ex)
                {

                }

            });

            var ShowProfile5= new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[4].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[4].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[4].id;
                        ViewModel.ShowProfile();
                    }

                }catch(Exception ex)
                {

                }

            });

            var ShowProfile6 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[5].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[5].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[5].id;
                        ViewModel.ShowProfile();
                    }
                }
                catch(Exception ex)
                {

                }

            });

            var ShowProfile7 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[6].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[6].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[6].id;
                        ViewModel.ShowProfile();
                    }
                }
                catch(Exception ex)
                {

                }

            });

            var ShowProfile8 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[7].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[7].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[7].id;
                        ViewModel.ShowProfile();
                    }
                }
                catch(Exception ex)
                {

                }

            });

            var ShowProfile9 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.AllProfiles[8].id))
                    {
                        ViewModel.profile_image = ViewModel.AllProfiles[8].image_url;
                        ViewModel.profile_id = ViewModel.AllProfiles[8].id;
                        ViewModel.ShowProfile();
                    }

                }
                catch (Exception ex)
                {

                }

            });




            //ViewModel.GetProfiles();

            all_points_image.SendSubviewToBack(this.View);

            image_profile_1.AddGestureRecognizer(ShowProfile1);

            image_profile_2.AddGestureRecognizer(ShowProfile2);

            image_profile_3.AddGestureRecognizer(ShowProfile3);

            image_profile_4.AddGestureRecognizer(ShowProfile4);

            image_profile_5.AddGestureRecognizer(ShowProfile5);

            image_profile_6.AddGestureRecognizer(ShowProfile6);

            image_profile_7.AddGestureRecognizer(ShowProfile7);

            image_profile_8.AddGestureRecognizer(ShowProfile8);

            image_profile_9.AddGestureRecognizer(ShowProfile9);




        }

        static UIImage FromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }


        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            loadingpage();

            styleElements();

            // you can set the update threshold and accuracy if you want:
            //locationManager.DistanceFilter = 10d; // move ten meters before updating
            //locationManager.HeadingFilter = 3d; // move 3 degrees before updating

            // you can also set the desired accuracy:
            locationManager.DesiredAccuracy = 1000; // 1000 meters/1 kilometer
            // you can also use presets, which simply evalute to a double value:
            //locationManager.DesiredAccuracy = CLLocation.AccuracyNearestTenMeters;

            locationManager.Delegate = this;
            locationManager.RequestWhenInUseAuthorization();

            if (CLLocationManager.LocationServicesEnabled)
            {
                locationManager.StartUpdatingLocation();
            }

            if (CLLocationManager.HeadingAvailable)
            {
                locationManager.StartUpdatingHeading();
            }

            //ViewModel.GetProfiles();

            //AccountInfo.first_time = 1;

            ViewModel.ForPropertyChange(x => x.AllProfiles, y =>
            {
                for (int a = 0; a < ViewModel.allprofiles.Count; a++)
                {
                    if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(0))
                    {
                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_1.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }

                        else
                        {
                            image_profile_1.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_1.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(1))
                    {
                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_2.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }

                        else
                        {
                            image_profile_2.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_2.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(2))
                    {
                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_3.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }
                        else
                        {
                            image_profile_3.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_3.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(3))
                    {

                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_4.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }

                        else
                        {
                            image_profile_4.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_4.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(4))
                    {

                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_5.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }

                        else
                        {
                            image_profile_5.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_5.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(5))
                    {

                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_6.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }

                        else
                        {
                            image_profile_6.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_6.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(6))
                    {

                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_7.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }

                        else
                        {
                            image_profile_7.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_7.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(7))
                    {
                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_8.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }

                        else
                        {
                            image_profile_8.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        image_profile_8.UserInteractionEnabled = true;
                    }
                    else if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].id) && a.Equals(8))
                    {
                        if (!string.IsNullOrEmpty(ViewModel.allprofiles[a].image_url))
                        {
                            image_profile_9.Image = FromUrl(ViewModel.allprofiles[a].image_url);
                        }
                        else
                        {
                            image_profile_9.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }
                        image_profile_9.UserInteractionEnabled = true;
                    }
                }

                loading_View.Hide();

            });

            this.View.BackgroundColor = UIColor.FromRGB(246,194,96);

            NavigationBarSetUp();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel.allprofiles.Clear();
        }
        public async void NavigationBarSetUp()
        {
            if (this.NavigationController != null)
            {
                //TODO: figure out how to make the status bar opaque.

                this.NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
                this.NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                this.NavigationController.NavigationBar.ShadowImage = new UIImage();
                this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(100,170,224);
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

          

            //Image

            UIImage logout = new UIImage("logo.png");


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

            //leftButton.AddSubview(container);


            leftButton.TouchUpInside += delegate


            {
                Console.WriteLine("pushing here");
                //ViewModel.GoToNotifications();
            };

            rigthButton.TouchUpInside += delegate
            {
                //var user = NSUserDefaults.StandardUserDefaults;

                //user.SetBool(true, "logged");

                ViewModel.ShowSettings();

                //ViewModel.Logout();

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