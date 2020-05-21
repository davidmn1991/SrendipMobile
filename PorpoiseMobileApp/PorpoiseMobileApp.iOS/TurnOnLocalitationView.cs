using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PorpoiseMobileApp.iOS
{
    public class TurnOnLocalitationView:UIView
    {
        public TurnOnLocalitationView()
        {
        }

        public delegate void PopWillCloseHandler();
        public event PopWillCloseHandler PopWillClose;

        private UILabel oops = new UILabel();

        private UILabel enable_location = new UILabel();

        private UILabel goSettings = new UILabel();

        

        private UIVisualEffectView effectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark));
        private UIButton btnClose = new UIButton();

        public TurnOnLocalitationView(CGSize size)
        {
            nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width);
            nfloat ly = (UIScreen.MainScreen.Bounds.Height - size.Height);
            this.Frame = new CGRect(new CGPoint(lx, ly), size);

            effectView.Alpha = 0;

            var attributes = new UIStringAttributes
            {
                Font = UIFont.FromName("System Semibold", 20f),
                ForegroundColor = UIColor.FromRGB(75, 171, 229)


            };
            var titleText = new NSAttributedString("Open Settings", attributes);

            nfloat btnHeight = 50;

            nfloat oopsHeight = 40;

            nfloat enable_location_height = 80;

            nfloat goSettings_height = 120;

            this.BackgroundColor = UIColor.White;

            btnClose.SetAttributedTitle(titleText,UIControlState.Normal);

            btnClose.SetTitleColor(UIColor.FromRGB(75, 171, 229),UIControlState.Normal);

            btnClose.Layer.BorderColor = UIColor.FromRGB(75, 171, 229).CGColor;

            btnClose.Layer.BorderWidth = 2f;

            btnClose.Layer.CornerRadius = 25;

            btnClose.Frame = new CGRect(this.Frame.Width/2 - 100, this.Frame.Height/2 - btnHeight/2, 200, btnHeight);

            goSettings.Frame = new CGRect(this.Frame.Width/2 - 150 , this.Frame.Height / 2 - btnHeight - goSettings_height/2 - 40 ,300, goSettings_height);

            enable_location.Frame = new CGRect(this.Frame.Width / 2 - 125, this.Frame.Height / 2 - btnHeight - goSettings_height/2 - enable_location_height - 20, 250, goSettings_height);

            oops.Frame = new CGRect(this.Frame.Width / 2 - 125, this.Frame.Height / 2 - btnHeight - goSettings_height/2 - enable_location_height - oopsHeight - 20, 250, oopsHeight);

            Console.WriteLine(this.Frame.Width);

            btnClose.TouchUpInside += delegate {

                UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));

                //Close();
            };
            oops.Text = "Oops";
            oops.Font = UIFont.FromName("Helvetica-Bold", 25f);
            oops.TextAlignment = UITextAlignment.Center;
            oops.TextColor = UIColor.FromRGB(112, 112, 112);
            

            enable_location.Text = "In order to use Srendip you need to enable your location";
            enable_location.Lines = 2;
            enable_location.TextAlignment = UITextAlignment.Center;
            enable_location.TextColor = UIColor.FromRGB(112, 112, 112);

            goSettings.Text = "Go to settings > Srendip > Location > Enable Location While Using the App";
            goSettings.Lines = 3;
            goSettings.TextAlignment = UITextAlignment.Center;
            goSettings.TextColor = UIColor.FromRGB(112, 112, 112);
            //goSettings.Frame = new CGRect(0, this.Frame.Height / 2 - btnHeight - goSettings_height / 2, this.Frame.Width, goSettings_height);



            this.AddSubview(btnClose);
            this.AddSubview(enable_location);
            this.AddSubview(goSettings);
            this.AddSubview(oops);
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
