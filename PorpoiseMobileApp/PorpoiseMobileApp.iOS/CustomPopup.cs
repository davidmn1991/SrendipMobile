using System;
using CoreGraphics;
using Foundation;
using UIKit;
using PorpoiseMobileApp.Models;
using Amazon.S3;
using Amazon.Runtime;
using Amazon;
using Amazon.S3.Transfer;
using System.IO;
using System.Threading.Tasks;

namespace PorpoiseMobileApp.iOS
{
    public class CustomPopup : UIView
    {

        public delegate void PopWillCloseHandler();
        public event PopWillCloseHandler PopWillClose;

        private string bubble_name;

        UIImageView bubble_icon = new UIImageView();

        UIImageView Profile_pet = new UIImageView();

        UILabel pet_name = new UILabel();

        private UIVisualEffectView effectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark));

        UILabel bublle_title = new UILabel();

        UILabel bublle_description = new UILabel();

        UIButton Resume = new UIButton();

        UIButton next_pet = new UIButton();

        UIButton back_pet = new UIButton();

        private UIButton btnClose = new UIButton(UIButtonType.System);

        public int pet_count = 0;

        public string url_resume;





        public CustomPopup(CGSize size, UserBubbles selected_bubble)
        {
            

            this.bubble_name = selected_bubble.name;
            nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width) / 2;
            nfloat ly = (UIScreen.MainScreen.Bounds.Height - size.Height) / 2;
            this.Frame = new CGRect(new CGPoint(lx, ly), size);

            effectView.Alpha = 0;

            

            UIImageView background_image = new UIImageView(this.Bounds);

            

            background_image.Image = UIImage.FromBundle("bubble_background.png");

            background_image.ContentMode = UIViewContentMode.ScaleAspectFit;

            this.AddSubview(background_image);


            this.BackgroundColor = UIColor.White;

            nfloat bubble_title_height = 50;
            


            if (selected_bubble.name.Equals("Instagram"))
            {
                bublle_title.Text = "Visit Instagram profile";
            }
            else if(selected_bubble.name.Equals("Facebook"))
            {

                bublle_title.Text = "Visit Facebook profile";

            }

            else if (selected_bubble.name.Equals("My resume"))
            {
                url_resume = selected_bubble.bubble_details[0].file_url;

                bublle_title.Text = selected_bubble.name + " :";

                Resume.Frame = new CGRect((this.Frame.Width / 2 - 45), this.Frame.Height / 2 - 50, 90, 100);

                Resume.SetBackgroundImage(UIImage.FromBundle("Icon awesome-file-pdf.png"),UIControlState.Normal);

                this.AddSubview(Resume);


            }

            else if (selected_bubble.name.Equals("My Pets"))
            {

                bublle_title.Text = selected_bubble.name + " :";

                pet_name.TextColor = UIColor.FromRGB(75, 171, 229);

                next_pet.Frame = new CGRect(this.Frame.Width / 2 + 8, this.Frame.Height / 2 + 120, 16, 20);

                next_pet.SetImage(UIImage.FromBundle("Icon ionic-ios-arrow-forward.png"), UIControlState.Normal);

                back_pet.Frame = new CGRect(this.Frame.Width / 2 - 16, this.Frame.Height / 2 + 120, 16, 20);

                back_pet.SetImage(UIImage.FromBundle("Icon ionic-ios-arrow-back_blue.png"), UIControlState.Normal);

                this.AddSubview(next_pet);

                this.AddSubview(back_pet);

                back_pet.Hidden = true;

                next_pet.Hidden = true;

                try
                {
                    if (!string.IsNullOrEmpty(selected_bubble.bubble_details[0].file_url))
                    {
                        Profile_pet.Image = FromUrl(selected_bubble.bubble_details[0].file_url);

                    }

                    else
                    {
                        Profile_pet.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                    }


                    Profile_pet.Frame = new CGRect((this.Frame.Width / 2 - 70), this.Frame.Height / 2 - 80, 140, 160);

                        pet_name.Text = selected_bubble.bubble_details[0].detail;

                        pet_name.Frame = new CGRect(0, this.Frame.Height / 2 + 90, this.Frame.Width, 20);

                        pet_name.TextAlignment = UITextAlignment.Center;

                        Profile_pet.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

                        Profile_pet.Layer.BorderWidth = 3f;

                        this.AddSubview(Profile_pet);
                        this.AddSubview(pet_name);
                    



                    if (selected_bubble.bubble_details.Count > 1)
                    {
                        next_pet.Hidden = false;
                    }
                }
                catch(Exception ex)
                {

                }

                

            }

            else
            {

                bublle_title.Text = selected_bubble.name + " :";
                bublle_description.Text = selected_bubble.description;

            }



            bublle_title.TextColor = UIColor.FromRGB(75, 171, 229);

            bublle_description.TextColor = UIColor.FromRGB(75, 171, 229);

            bublle_description.TextAlignment = UITextAlignment.Center;

            bublle_title.TextAlignment = UITextAlignment.Center;


            bubble_icon.Image = FromUrl(selected_bubble.icon);

            bubble_icon.Frame = new CGRect(this.Frame.Width/2-45,0-45,90,90);


            bublle_title.Frame = new CGRect(0, bubble_icon.Frame.Height/2 - 15, this.Frame.Width, bubble_title_height);

            bublle_description.Frame = new CGRect((this.Frame.Width/2-this.Frame.Width/4), this.Frame.Height/2 - 20, this.Frame.Width/2, this.Frame.Height/3);


            this.Layer.CornerRadius = 25;
            
            this.AddSubview(bubble_icon);
            this.AddSubview(bublle_title);
            this.AddSubview(bublle_description);

            var CloseView = new UITapGestureRecognizer(() =>
            {
                Close();
            });

            effectView.AddGestureRecognizer(CloseView);

            effectView.UserInteractionEnabled = true;


                Resume.TouchUpInside += delegate

            {

                UIApplication.SharedApplication.OpenUrl(new NSUrl(url_resume));


            };

            next_pet.TouchUpInside += delegate {

                Profile_pet.Image = null;

                pet_count++;

                back_pet.Hidden = false;

                if (selected_bubble.bubble_details.Count.Equals(pet_count + 1))
                {
                    next_pet.Hidden = true;
                }

                try
                {
                    if (selected_bubble.bubble_details.Count > pet_count)
                    {
                        if (!string.IsNullOrEmpty(selected_bubble.bubble_details[pet_count].file_url))
                        {
                            Profile_pet.Image = FromUrl(selected_bubble.bubble_details[pet_count].file_url);
                        }

                        else
                        {
                            Profile_pet.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                        Profile_pet.Frame = new CGRect((this.Frame.Width / 2 - 70), this.Frame.Height / 2 - 80, 140, 160);

                            pet_name.Text = selected_bubble.bubble_details[pet_count].detail;

                            pet_name.Frame = new CGRect(0, this.Frame.Height / 2 + 90, this.Frame.Width, 20);

                            pet_name.TextAlignment = UITextAlignment.Center;

                            Profile_pet.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

                            Profile_pet.Layer.BorderWidth = 3f;

                            this.AddSubview(Profile_pet);
                            this.AddSubview(pet_name);
                        



                    }

                  
                        
                    
                 }
                catch(Exception ex)
                {


                }


	

	        };

            back_pet.TouchUpInside += delegate {

                Profile_pet.Image = null;

                pet_count--;

                next_pet.Hidden = false;

                try
                {
                    if (pet_count>= 0)
                    {

                        if (!string.IsNullOrEmpty(selected_bubble.bubble_details[pet_count].file_url))
                        {
                            Profile_pet.Image = FromUrl(selected_bubble.bubble_details[pet_count].file_url);
                        }

                        else
                        {
                            Profile_pet.Image = FromUrl("https://srendip-dev.s3.amazonaws.com/no-image-icon.png");
                        }

                            Profile_pet.Frame = new CGRect((this.Frame.Width / 2 - 70), this.Frame.Height / 2 - 80, 140, 160);

                            pet_name.Text = selected_bubble.bubble_details[pet_count].detail;

                            pet_name.Frame = new CGRect(0, this.Frame.Height / 2 + 90, this.Frame.Width, 20);

                            pet_name.TextAlignment = UITextAlignment.Center;

                            Profile_pet.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

                            Profile_pet.Layer.BorderWidth = 3f;

                            this.AddSubview(Profile_pet);
                            this.AddSubview(pet_name);
                        



                    }


                    if (pet_count.Equals(0))
                    {
                        back_pet.Hidden = true;
                    }
                }
                catch (Exception ex)
                {


                }



            };
            

        }

        static UIImage FromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

        public void PopUp(bool animated = true, Action popAnimationFinish = null)
        {
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            effectView.Frame = window.Bounds;
            window.EndEditing(true);
            window.AddSubview(effectView);
            window.AddSubview(this);
            

            if (animated)
            {
                Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
                UIView.Animate(0.15, delegate {
                    Transform = CGAffineTransform.MakeScale(1, 1);
                    effectView.Alpha = 0.8f;
                }, delegate {
                    if (null != popAnimationFinish)
                        popAnimationFinish();
                });
            }
            else
            {
                effectView.Alpha = 0.8f;
            }
        }

        public void Close(bool animated = true)
        {
            if (animated)
            {
                UIView.Animate(0.15, delegate {
                    Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
                    effectView.Alpha = 0;
                }, delegate {
                    this.RemoveFromSuperview();
                    
                    effectView.RemoveFromSuperview();
                    if (null != PopWillClose) PopWillClose();
                });
            }
            else
            {
                if (null != PopWillClose) PopWillClose();
            }
        }
    }
}
