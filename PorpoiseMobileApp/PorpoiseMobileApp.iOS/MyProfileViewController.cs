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
using System.Collections.Generic;
using PorpoiseMobileApp.Models;

namespace PorpoiseMobileApp.iOS
{
    public partial class MyProfileViewController : MvvmViewController<MyProfileViewModel>
    {
        public MyProfileViewController(IntPtr handle) : base(handle)
        {
        }

        LoadingOverlay loading_View;

        public UserBubbles selected_bubble = new UserBubbles();

        CustomPopup cpuv;

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

            profile_image.Layer.BorderColor = UIColor.White.CGColor;

            profile_image.Layer.BorderWidth = 4f;

            edit_profile.Layer.BorderColor = UIColor.White.CGColor;

            edit_profile.Layer.BorderWidth = 2f;

            header_view.BackgroundColor = UIColor.FromRGB(75, 171, 229);

            edit_profile.TouchUpInside += delegate
            {

                ViewModel.ShowProfile();


            };

            var ShowBubble1 = new UITapGestureRecognizer(() =>
            {
                try
                {

                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[0].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[0].name;

                        selected_bubble = ViewModel.Profile_bubbles[0];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            //this.HidesBottomBarWhenPushed = true;
                            Console.WriteLine("cpuv will close");
                        });

                    }
                    else
                    {
                        Console.WriteLine("Add bubble 01");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 01");
                }

            });

            var ShowBubble2 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[1].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[1].name;

                        selected_bubble = ViewModel.Profile_bubbles[1];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });

                    }
                    else
                    {
                        Console.WriteLine("Add bubble 02");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 02");
                }

            });

            var ShowBubble3 = new UITapGestureRecognizer(() =>
            {
                try
                {

                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[2].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[2].name;

                        selected_bubble = ViewModel.Profile_bubbles[2];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Add bubble 03");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 03");
                }

            });

            var ShowBubble4 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[3].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[3].name;

                        selected_bubble = ViewModel.Profile_bubbles[3];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }

                    else
                    {
                        Console.WriteLine("Add bubble 04");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 04");
                }

            });

            var ShowBubble5 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[4].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[4].name;

                        selected_bubble = ViewModel.Profile_bubbles[4];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Add bubble 05");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 05");
                }

            });

            var ShowBubble6 = new UITapGestureRecognizer(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[5].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[5].name;

                        selected_bubble = ViewModel.Profile_bubbles[5];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Add bubble 06");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 06");
                }

            });

            var ShowBubble7 = new UITapGestureRecognizer(() =>
            {
                try
                {

                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[6].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[6].name;

                        selected_bubble = ViewModel.Profile_bubbles[6];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Add bubble 07");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 07");
                }

            });

            var ShowBubble8 = new UITapGestureRecognizer(() =>
            {
                try
                {

                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[7].name))
                    {
                        //bubble_name = ViewModel.Profile_bubbles[7].name;

                        selected_bubble = ViewModel.Profile_bubbles[7];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Add bubble 08");
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 08");
                }

            });

            var ShowBubble9 = new UITapGestureRecognizer(() =>
            {
                try
                {

                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[8].name))
                    {

                        //bubble_name = ViewModel.Profile_bubbles[8].name;

                        selected_bubble = ViewModel.Profile_bubbles[8];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);

                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Add bubble 09");
                    }
                }

                catch (Exception ex)
                {
                    ViewModel.ShowBubbleBoard();
                    Console.WriteLine("Add bubble 09");
                }

            });

            var ShowBubble10 = new UITapGestureRecognizer(() =>
            {
                try
                {

                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[9].name))
                    {

                        //bubble_name = ViewModel.Profile_bubbles[9].name;

                        selected_bubble = ViewModel.Profile_bubbles[9];

                        var popup_size = new CoreGraphics.CGSize(this.View.Frame.Width - 20, 300);

                        CustomPopup cpuv = new CustomPopup(popup_size, selected_bubble);
                        cpuv.PopUp(true, delegate
                        {
                            Console.WriteLine("cpuv will close");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Add bubble 10");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Add bubble 10");
                }

            });

            bubble_01.AddGestureRecognizer(ShowBubble1);
            bubble_02.AddGestureRecognizer(ShowBubble2);
            bubble_03.AddGestureRecognizer(ShowBubble3);
            bubble_04.AddGestureRecognizer(ShowBubble4);
            bubble_05.AddGestureRecognizer(ShowBubble5);
            bubble_06.AddGestureRecognizer(ShowBubble6);
            bubble_07.AddGestureRecognizer(ShowBubble7);
            bubble_08.AddGestureRecognizer(ShowBubble8);
            bubble_09.AddGestureRecognizer(ShowBubble9);
            bubble_01.UserInteractionEnabled = true;
            bubble_02.UserInteractionEnabled = true;
            bubble_03.UserInteractionEnabled = true;
            bubble_04.UserInteractionEnabled = true;
            bubble_05.UserInteractionEnabled = true;
            bubble_06.UserInteractionEnabled = true;
            bubble_07.UserInteractionEnabled = true;
            bubble_08.UserInteractionEnabled = true;
            bubble_09.UserInteractionEnabled = true;


            //bubble_10.AddGestureRecognizer(ShowBubble10);

        }



        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ViewModel.ProfileBubbles.Clear();

            //bubble_01.Hidden = true;

            //bubble_02.Hidden = true;

            //bubble_03.Hidden = true;

            //bubble_04.Hidden = true;

            //bubble_05.Hidden = true;

            //bubble_07.Hidden = true;

            //bubble_08.Hidden = true;

            //bubble_09.Hidden = true;

            bubble_10.Hidden = true;



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

            loadingpage();

            Console.WriteLine(this.View.Frame.Height);

            ViewModel.GetProfilesDetails();

            ViewModel.ForPropertyChange(x => x.ImageUrl, y =>
            {

                //profile_name.AttributedText = new NSAttributedString(ViewModel.profile_name + "," + ViewModel.profile_age, underlineStyle: NSUnderlineStyle.None);

                profile_name.Text = ViewModel.profile_name;

                try
                {
                    if (!string.IsNullOrEmpty(ViewModel.image_url))
                    {
                        profile_image.Image = FromUrl(ViewModel.image_url);

                    }

                    else
                    {
                        profile_image.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                    }
                }
                catch (Exception ex)
                {

                }

                loading_View.Hide();
            });

            this.View.BackgroundColor = UIColor.FromRGB(246, 194, 96);

            var bubble01Height = bubble_01.HeightAnchor.ConstraintEqualTo(0);
            //bubble01Height.Active = true;

            var bubble05Height = bubble_05.HeightAnchor.ConstraintEqualTo(0);
            //bubble05Height.Active = true;

            var bubble08Height = bubble_08.HeightAnchor.ConstraintEqualTo(0);
            //bubble08Height.Active = true;

            ViewModel.ForPropertyChange(x => x.Profile_bubbles, y =>
            {
                for (int a = 0; a < ViewModel.Profile_bubbles.Count; a++)
                {
                    if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(0))
                    {
                        bubble_01.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_01.Hidden = false;

                        bubble_02.Hidden = false;
                        //bubble01Height.Active = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(1))
                    {
                        bubble_02.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_02.Hidden = false;

                        bubble_03.Hidden = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(2))
                    {
                        bubble_03.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_03.Hidden = false;

                        bubble_04.Hidden = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(3))
                    {
                        bubble_04.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_04.Hidden = false;

                        bubble_05.Hidden = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(4))
                    {
                        bubble_05.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_05.Hidden = false;

                        bubble_06.Hidden = false;

                        //bubble05Height.Active = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(5))
                    {
                        bubble_06.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_06.Hidden = false;

                        bubble_07.Hidden = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(6))
                    {
                        bubble_07.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_07.Hidden = false;

                        bubble_08.Hidden = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(7))
                    {
                        bubble_08.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_08.Hidden = false;

                        bubble_09.Hidden = false;

                        //bubble08Height.Active = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(8))
                    {
                        bubble_09.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_09.Hidden = false;
                    }

                    else if (!string.IsNullOrEmpty(ViewModel.Profile_bubbles[a].icon) && a.Equals(9))
                    {
                        bubble_10.Image = FromUrl(ViewModel.Profile_bubbles[a].icon);

                        bubble_10.Hidden = false;
                    }
                }

                

                Console.WriteLine(ViewModel.ProfileBubbles.Count);

            });

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

            leftButton.UserInteractionEnabled = true;


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
                //ViewModel.ShowSettings();
                Console.WriteLine("pushing here");
                //ViewModel.GoToNotifications();
            };

            rigthButton.TouchUpInside += delegate


            {
                ViewModel.ShowSettings();
                Console.WriteLine("pushing here");
                //ViewModel.GoToNotifications();
            };

            rigthButton.UserInteractionEnabled = true;


            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(leftButton);

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(rigthButton);


            //UILabel label = new UILabel();

            //NavigationItem.LeftBarButtonItem.CustomView.Subviews.Append(label);

            //NavigationItem.Title = "Porpoise";

            NavigationItem.TitleView = new UIImageView(UIImage.FromBundle("DIP_logo.png"));
        }
    }
}