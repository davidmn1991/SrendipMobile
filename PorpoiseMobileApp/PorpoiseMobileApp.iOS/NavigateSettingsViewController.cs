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

namespace PorpoiseMobileApp.iOS
{
    public partial class NavigateSettingsViewController : MvvmViewController<NavigateSettingsViewModel>
    {
        public NavigateSettingsViewController (IntPtr handle) : base (handle)
        {
        }



        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AppDelegate.LoadLocationStatus += ReloadLocationStatus;

            openSettings.TouchUpInside += delegate {

                UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));

                

            };

            switch (CLLocationManager.Status)
            {

                case CLAuthorizationStatus.AuthorizedWhenInUse:
                    Console.WriteLine("Access");
                    ViewModel.ShowAllProfiles();
                    break;
                case CLAuthorizationStatus.Denied:
                    
                    Console.WriteLine("No Access");
                    break;
                default:
                    Console.WriteLine("No Access other reason");
                    break;
            }





        }

        public void ReloadLocationStatus()
        {
            BeginInvokeOnMainThread(delegate
            {
                this.ViewDidLoad();
            });
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}