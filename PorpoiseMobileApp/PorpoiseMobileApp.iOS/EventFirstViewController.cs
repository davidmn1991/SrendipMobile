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
    public partial class EventFirstViewController : MvvmViewController<EventFirstViewModel>
    {
        public EventFirstViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            event_namelbl.TextColor = UIColor.FromRGB(112, 112, 112);
         
            NavigationController.NavigationBarHidden = true;

            var screenTap = new UITapGestureRecognizer(() =>
            {
                txteventName.ResignFirstResponder();


            });

            this.View.AddGestureRecognizer(screenTap);

            backButton.TouchUpInside += (sender, e) => {
                ViewModel.BackSignup1();
            };

            nextButton.TouchUpInside += (sender, e) => {

                if (!string.IsNullOrEmpty(txteventName.Text))
                {
                    ViewModel.eventName = txteventName.Text;

                    ViewModel.ShowSignup2();
                }

                else
                {
                    var okAlertController = UIAlertController.Create("Error message", "Please enter event name", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);
                }
                
            };

        }

       
    }
}