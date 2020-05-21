
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
    public partial class Signup2ViewController : MvvmViewController<Signup2ViewModel>
    {
        public Signup2ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            signuplbl.TextColor = UIColor.FromRGB(112, 112, 112);

            txt_password.SecureTextEntry = true;

            txt_confirmPassword.SecureTextEntry = true;

            NavigationController.NavigationBarHidden = true;

            var screenTap = new UITapGestureRecognizer(() =>
            {
                txt_email.ResignFirstResponder();
                txt_password.ResignFirstResponder();
                txt_confirmPassword.ResignFirstResponder();

            });

            this.View.AddGestureRecognizer(screenTap);


            backButton.TouchUpInside += (sender, e) => {
                ViewModel.BackSignup1();
            };

            ViewModel.ForPropertyChange(x => x.errorMessage, y =>
            {

                //Debug.WriteLine("PORPOISE LOGO CHANGED");

                var okAlertController = UIAlertController.Create("Error message", "Email already registered", UIAlertControllerStyle.Alert);

                //Add Action
                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                // Present Alert
                PresentViewController(okAlertController, true, null);


            });
            nextButton.TouchUpInside += (sender, e) => {

                

                if (!string.IsNullOrEmpty(txt_email.Text) && !string.IsNullOrEmpty(txt_password.Text) && !string.IsNullOrEmpty(txt_confirmPassword.Text) && txt_password.Text == txt_confirmPassword.Text)
                {
                    if (txt_password.Text.Length < 6)
                    {

                        var okAlertController = UIAlertController.Create("Error message", "Password size must be between 6 and 64", UIAlertControllerStyle.Alert);

                        //Add Action
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Present Alert
                        PresentViewController(okAlertController, true, null);

                    }
                    else
                    {
                        


                        ViewModel.email = txt_email.Text;

                        ViewModel.password = txt_password.Text;

                        ViewModel.EmailVerification();

                        //ViewModel.showSignup3();
                    }

                }

                else
                {
                    if (string.IsNullOrEmpty(txt_email.Text))
                    {
                        var okAlertController = UIAlertController.Create("Error message", "Enter email", UIAlertControllerStyle.Alert);

                        //Add Action
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Present Alert
                        PresentViewController(okAlertController, true, null);
                    }

                    else if (string.IsNullOrEmpty(txt_password.Text))
                    {
                        var okAlertController = UIAlertController.Create("Error message", "Enter password", UIAlertControllerStyle.Alert);

                        //Add Action
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Present Alert
                        PresentViewController(okAlertController, true, null);

                    }

                    else if (txt_password.Text != txt_confirmPassword.Text)
                    {
                        var okAlertController = UIAlertController.Create("Error message", "password does not match", UIAlertControllerStyle.Alert);

                        //Add Action
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Present Alert
                        PresentViewController(okAlertController, true, null);

                    }
                }
                
            };



        }
    }
}