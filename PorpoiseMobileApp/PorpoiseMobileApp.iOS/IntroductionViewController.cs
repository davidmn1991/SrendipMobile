
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
    public partial class IntroductionViewController : MvvmViewController<IntroductionViewModel>
    {
        public IntroductionViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBarHidden = true;

            login_button.TouchUpInside += (sender, e) => {
                ViewModel.ShowLogin();
            };

            sign_up.TouchUpInside += (sender, e) => {
                ViewModel.ShowSignup();
            };


        }
    }
}