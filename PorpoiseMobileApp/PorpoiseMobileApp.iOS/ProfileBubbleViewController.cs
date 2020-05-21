using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.ViewModels;
using UIKit;

namespace PorpoiseMobileApp.iOS
{
    public partial class ProfileBubbleViewController : MvvmViewController<ProfileBubbleViewModel>
    {
        public ProfileBubbleViewController(IntPtr handle) : base(handle)
        {
        }

        UIButton link_profile = new UIButton();

        List<string> readPermissions = new List<string> { "public_profile", "email", "user_birthday", "user_gender" };

        LoginButton loginView;



 


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            link_profile.Frame = new CGRect(this.View.Frame.Width / 2 - 150, 206, 300, 60);

            link_profile.TouchUpInside +=delegate {

                Console.WriteLine("presing facebook link");

                loginView.SendActionForControlEvents(UIControlEvent.TouchUpInside);

            };

            back_button.TouchUpInside += delegate
            {

                ViewModel.ShowBubbleBoard();


            };

            addBubble_button.TouchUpInside += delegate
            {

                {
                    ViewModel.description = txtfield.Text;

                    ViewModel.AddBubble();

                }
            };

            var screenTap = new UITapGestureRecognizer(() =>
            {
                txtfield.ResignFirstResponder();

            });

            this.View.AddGestureRecognizer(screenTap);

            txtfield.ShouldReturn = (textField) =>
            {
                txtfield.ResignFirstResponder();
                return true;
            };

            addBubble_button.BackgroundColor = UIColor.FromRGB(75, 171, 229);

            Profile.Notifications.ObserveDidChange((sender, e) => {

                if (e.NewProfile == null)
                    return;
                if (AccessToken.CurrentAccessToken != null)
                {
                    var request = new GraphRequest("/me?fields=first_name,last_name,name,email,picture,birthday,gender", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
                    request.Start((connection, result, error) =>
                    {
                        // Handle if something went wrong with the request
                        if (error != null)
                        {
                            //showAlert("Error", error.Description);
                            return;
                        }

                        //fbReponseFromSDK facebookSDKLoginItem = new fbReponseFromSDK();
                        // Get your profile name
                        var userInfo = result as NSDictionary;
                        if (userInfo["id"] != null)
                        {
                            //ViewModel.provider_id = userInfo["id"].ToString();

                            //ViewModel.user_login_provider.user_provider_id = userInfo["id"].ToString();

                            Console.WriteLine("id is: " + userInfo["id"].ToString());
                        }
                        if (userInfo["name"] != null)
                        {
                            //ViewModel.user_login_provider.user_provider_name = userInfo["name"].ToString();

                            Console.WriteLine("name is: " + userInfo["name"].ToString());
                        }

                        if (userInfo["first_name"] != null)
                        {
                            //ViewModel.first_name = userInfo["first_name"].ToString();

                            Console.WriteLine("first_name is: " + userInfo["first_name"].ToString());
                        }

                        if (userInfo["last_name"] != null)
                        {
                            //ViewModel.last_name = userInfo["last_name"].ToString();

                            Console.WriteLine("last_name is: " + userInfo["last_name"].ToString());
                        }

                        if (userInfo["email"] != null)
                        {
                            //ViewModel.email = userInfo["email"].ToString();

                            Console.WriteLine("email is: " + userInfo["email"].ToString());
                        }

                        if (userInfo["picture"] != null)
                        {
                            Console.WriteLine("profile image is: " + userInfo["picture"].ToString());
                        }

                        if (userInfo["birthday"] != null)
                        {
                            var formatted = DateTime.Parse(userInfo["birthday"].ToString());
                            //ViewModel.birth_date = formatted.ToString("yyyy-MM-dd");
                            Console.WriteLine("birthday is: " + userInfo["birthday"].ToString());
                        }

                        if (userInfo["gender"] != null)
                        {
                            if (userInfo["gender"].Equals("male"))
                            {
                                //ViewModel.gender = "MALE";
                            }
                            else if (userInfo["gender"].Equals("female"))
                            {
                                //ViewModel.gender = "FEMALE";

                            }
                            Console.WriteLine("gender is: " + userInfo["gender"].ToString());
                        }
                        //(Success) Do what you want next :
                        //doneFacbookLogin();

                        //loginView.LoggedOut();

                        //ViewModel.user_login_provider.provider = "FACEBOOK";

                        //ViewModel.EmailVerification();
                    });
                }

                //nameLabel.Text = e.NewProfile.Name;
            });

            loginView = new LoginButton(new CGRect(0, 0, link_profile.Frame.Width, link_profile.Frame.Height))
            {
                LoginBehavior = LoginBehavior.Browser,
                Permissions = readPermissions.ToArray()
            };



            var attributes = new UIStringAttributes
            {
                //BackgroundColor = UIColor.FromRGB(75, 171, 229),
                
                ForegroundColor = UIColor.White,
                Font = UIFont.FromName("Helvetica-Bold", 17f)
            };
            var titleText = new NSAttributedString("Link Facebook profile", attributes);

            loginView.SetAttributedTitle(titleText, UIControlState.Normal);

            loginView.SetBackgroundImage(null, UIControlState.Normal);

            

            loginView.BackgroundColor = UIColor.FromRGB(75, 171, 229);
            // Handle actions once the user is logged in
            loginView.Completed += (sender, e) => {
                if (e.Error != null)
                {
                    // Handle if there was an error
                }

                if (e.Result.IsCancelled)
                {
                    // Handle if the user cancelled the login request
                }

                // Handle your successful login
            };

            // Handle actions once the user is logged out
            loginView.LoggedOut += (sender, e) => {



                Profile.CurrentProfile = null;
                // Handle your logout
            };

            

            // The user image profile is set automatically once is logged in
 

            // Add views to main view

            Console.WriteLine();

            loginView.Layer.CornerRadius = 20;

            link_profile.BackgroundColor = UIColor.FromRGB(75, 171, 229);

            //link_profile.AddSubview(loginView);

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

            tittlelbl.TextColor = UIColor.FromRGB(112, 112, 112);

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
            tittlelbl.AddGestureRecognizer(GoBack);

            tittlelbl.UserInteractionEnabled = true;



            this.NavigationController.NavigationBarHidden = false;
            this.HidesBottomBarWhenPushed = true;

            if (ViewModel.bubble_selected.Equals("InstagramProfile"))
            {
                tittlelbl.Text = "Instagram Profile";
                txtfield.Placeholder = "Instagram profile name";
                icon_picture.Image = (UIImage.FromBundle("Group 47.png"));

            }

            else if (ViewModel.bubble_selected.Equals("FacebookProfile"))
            {
               
                tittlelbl.Text = "Facebook Profile";
                //txtfield.Placeholder = "Facebook profile name";
                txtfield.Hidden = true;
                icon_picture.Image = (UIImage.FromBundle("Group 46.png"));

                var attributes = new UIStringAttributes
                {
                    Font = UIFont.FromName("Helvetica-Bold", 17f),
                    ForegroundColor = UIColor.White,
                    //BackgroundColor = UIColor.FromRGB(75, 171, 229)


                };
                var titleText = new NSAttributedString("Link Facebook profile", attributes);
                

                link_profile.SetAttributedTitle(titleText, UIControlState.Normal);

                link_profile.BackgroundColor = UIColor.FromRGB(75, 171, 229);

                //link_profile.SetTitleColor(UIColor.FromRGB(75, 171, 229), UIControlState.Normal);

                //link_profile.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

                //link_profile.Layer.BorderWidth = 2f;

                link_profile.Layer.CornerRadius = 20;

                this.View.AddSubview(link_profile);

            }

            else if (ViewModel.bubble_selected.Equals("FavoriteTvShow"))
            {
                tittlelbl.Text = "Favorite TV Show";
                txtfield.Placeholder = "What's your favorite TV show";
                icon_picture.Image = (UIImage.FromBundle("Group 50.png"));

            }

            else if (ViewModel.bubble_selected.Equals("FavoriteBook"))
            {
                tittlelbl.Text = "Favorite Book";
                txtfield.Placeholder = "My Favorite Book";
                icon_picture.Image = (UIImage.FromBundle("Group 51.png"));

            }

            else if (ViewModel.bubble_selected.Equals("Profession"))
            {
                tittlelbl.Text = "Profession";
                txtfield.Placeholder = "My Profession";
                icon_picture.Image = (UIImage.FromBundle("Group 43.png"));

            }

            else if (ViewModel.bubble_selected.Equals("FavoriteMeal"))
            {
                tittlelbl.Text = "Favorite Meal";
                txtfield.Placeholder = "My Favorite Meal";
                icon_picture.Image = (UIImage.FromBundle("Group 170.png"));

            }

            NavigationBarSetUp();
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