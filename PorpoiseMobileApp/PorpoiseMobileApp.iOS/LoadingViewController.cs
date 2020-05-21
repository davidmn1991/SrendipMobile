// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using PorpoiseMobileApp.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PorpoiseMobileApp.iOS
{
	public partial class LoadingViewController : MvvmViewController<LoadingViewModel>, IHasDisplayHint
	{
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }


        public LoadingViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();
			if (NavigationController != null) {
				NavigationController.NavigationBarHidden = true;
			}

            Waiting.Color = UIColor.Yellow;
            
            Bindings.Bind(Waiting).For(x => x.Hidden).To(x => x.InFlight);
            Bindings.Apply();

			ViewModel.AuthenticateEvent += (object sender, SdkEventArgs e) => {
				if(!e.Successful){
					this.Alert("Error", "cannot contact server, please check your internet connection");
				}
			};

			Task.Delay (1000);
			ViewModel.AuthenticateCommand.Execute ();



		}
        public DisplayHint Hint
        {
            get
            {
                DisplayHint hint = new DisplayHint
                {
                    ClearToRoot = true,
                    FullScreen = false,
                    Animate = true
                };

                return hint;
            }
        }
    }
}