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
    public partial class Signup5ViewController : MvvmViewController<Signup5ViewModel>
    {
        public Signup5ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            biolbl.TextColor = UIColor.FromRGB(112, 112, 112);
    

            NavigationController.NavigationBarHidden = true;

            var screenTap = new UITapGestureRecognizer(() =>
            {
                descriptionTextView.ResignFirstResponder();


            });

            this.View.AddGestureRecognizer(screenTap);

            if (!string.IsNullOrEmpty(ViewModel.viewController))
            {
                pointsButton.Image = UIImage.FromBundle("Points4.png");

            }

            ViewModel.ForPropertyChange(x => x.errorMessage, y =>
            {

                //Debug.WriteLine("PORPOISE LOGO CHANGED");

                var okAlertController = UIAlertController.Create("Error message", ViewModel.errorMessage, UIAlertControllerStyle.Alert);

                //Add Action
                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                // Present Alert
                PresentViewController(okAlertController, true, null);


            });

            backButton.TouchUpInside += (sender, e) => {
                ViewModel.BackSignup4();
            };

            nextButton.TouchUpInside += (sender, e) => {

                if (!string.IsNullOrEmpty(descriptionTextView.Text))
                {
                    ViewModel.textView = descriptionTextView.Text;

                    ViewModel.ShowNext();
                }

                else
                {

                

                    var okAlertController = UIAlertController.Create("Error message", "Enter bio", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);

                }
                
            };

            descriptionTextView.Layer.BorderColor = UIColor.Black.CGColor;

            descriptionTextView.Layer.BorderWidth = 1f;

            

            descriptionTextView.ShouldChangeText= (textField, range, replacementString) => {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;

                Console.WriteLine("LENGHT" + newLength);
                counterTextlbl.Text = newLength + "/144";

                if (newLength <= 144)
                {

                    //var total_entry = 280 - newLength;
                    //label_test.Text = total_entry.ToString();
                    //Console.WriteLine("LENGHT" + newLength);
                    //counterTextlbl.Text = newLength + "/144";

                }
                else
                {

                    //var total_entry = 280 - newLength;
                    //label_test.Text = total_entry.ToString();
                    //Console.WriteLine("LENGHT" + newLength);



                }


                return newLength <= 20000;

            };
        }
    }
}