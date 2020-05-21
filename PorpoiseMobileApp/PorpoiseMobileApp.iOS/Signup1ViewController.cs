
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
    public partial class Signup1ViewController : MvvmViewController<Signup1ViewModel>
    {
        public Signup1ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            namelbl.TextColor = UIColor.FromRGB(112, 112, 112);
      
            

            NavigationController.NavigationBarHidden = true;

            var screenTap = new UITapGestureRecognizer(() =>
            {
                nameField.ResignFirstResponder();
                lastNameField.ResignFirstResponder();

            });

            this.View.AddGestureRecognizer(screenTap);


            backButton.TouchUpInside += (sender, e) => {
                ViewModel.BackIntroduction();
            };

            nextButton.TouchUpInside += (sender, e) => {

                if (!string.IsNullOrEmpty(nameField.Text) && !string.IsNullOrEmpty(lastNameField.Text))
                {

                    ViewModel.name = nameField.Text;

                    ViewModel.last_name = lastNameField.Text;

                    ViewModel.ShowSignup2();

                }

                else
                {
                    if (string.IsNullOrEmpty(nameField.Text))
                    {
                        var okAlertController = UIAlertController.Create("Error message", "Enter name", UIAlertControllerStyle.Alert);

                        //Add Action
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Present Alert
                        PresentViewController(okAlertController, true, null);
                    }

                    else if (string.IsNullOrEmpty(lastNameField.Text))
                    {
                        var okAlertController = UIAlertController.Create("Error message", "Enter last name", UIAlertControllerStyle.Alert);

                        //Add Action
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Present Alert
                        PresentViewController(okAlertController, true, null);

                    }
                }
                
            };

            createEventButton.TouchUpInside += (sender, e) => {

                if (!string.IsNullOrEmpty(nameField.Text) && !string.IsNullOrEmpty(lastNameField.Text))
                {

                    ViewModel.name = nameField.Text;

                    ViewModel.last_name = lastNameField.Text;

                    ViewModel.ShowEventFirst();

                }

                else
                {
                    if (string.IsNullOrEmpty(nameField.Text))
                    {
                        var okAlertController = UIAlertController.Create("Error message", "Enter name", UIAlertControllerStyle.Alert);

                        //Add Action
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Present Alert
                        PresentViewController(okAlertController, true, null);
                    }

                    else if (string.IsNullOrEmpty(lastNameField.Text))
                    {
                        var okAlertController = UIAlertController.Create("Error message", "Enter last name", UIAlertControllerStyle.Alert);

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