using System;
using UIKit;
using PorpoiseMobileApp.ViewModels;
using MvvmCross.iOS.Views;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using Facebook.LoginKit;
using Facebook.CoreKit;
using System.Threading.Tasks;
using Xamarin.Essentials;
//using System.Threading;
using System.Timers;

namespace PorpoiseMobileApp.iOS
{

	public class HomeTabBarDelegate : UITabBarControllerDelegate
	{
		private HomeViewController ctrl;
		private HomeViewModel viewmodel;
		public HomeTabBarDelegate(HomeViewController ctrl, HomeViewModel viewmodel)
		{
			this.ctrl = ctrl;
			this.viewmodel = viewmodel;
		}
		public override bool ShouldSelectViewController(UITabBarController tabBarController, UIViewController viewController)
		{Console.WriteLine("SHOULD SELECT");

			var screen = tabBarController.SelectedViewController as IDirty;

			if (screen == null)
			{
				return true;
			}
			//else {
			//	ctrl.MoreNavigationController.PopToRootViewController(false);
			//}
			if (screen.IsDirty && (screen as SrendipTabNavigationController).MenuItem.Equals(MenuItem.LogHours))
			{
				ctrl.Confirm(Resources.AreYouSure, Resources.UnsavedChanges, null, ok =>
				{
					if (ok)
					{
						screen.Cleanup();
						ctrl.SelectedViewController = viewController;
					}
				});
				return false;
			}

			return true;
		}


	}

	//[Register ("HomeViewController")]
	public partial class HomeViewController : MvxTabBarViewController<HomeViewModel>, IHasDisplayHint
	{


		UIViewController selected;

		

		public HomeViewController(IntPtr handle) : base(handle)
		{
		}

		public override UIViewController SelectedViewController
		{
			get
			{
				return base.SelectedViewController;
			}
			set
			{
				base.SelectedViewController = value;
			}
		}

		public void FlagTutorial()
		{

			ViewModel.FlagTutorial();

		}

		public async Task getLocation()
		{
			try
			{
				var location = await Geolocation.GetLastKnownLocationAsync();

				if (location != null)
				{
					ViewModel.latitude = location.Latitude;

					ViewModel.longitude = location.Longitude;

					ViewModel.UploadLocation();

					Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
				}
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				// Handle not supported on device exception
			}
			catch (FeatureNotEnabledException fneEx)
			{
				// Handle not enabled on device exception
			}
			catch (PermissionException pEx)
			{
				// Handle permission exception
			}
			catch (Exception ex)
			{
				// Unable to get location
			}

		}



		public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
			UIApplication.SharedApplication.StatusBarHidden = false;
			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
			getLocation();
            try {
				if (TabBarController.TabBar != null)
				{
					TabBarController.TabBar.UserInteractionEnabled = true;

					//TabBarController.TabBar.RemoveFromSuperview();
				}
            }
            catch (Exception)
            {

            }
           /* if (UIApplication.SharedApplication.StatusBarHidden)
            {

                UIApplication.SharedApplication.StatusBarHidden = false;

                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

                //UIApplication.SharedApplication.StatusBarHidden = false;
            }*/
        }

        public void showChallenges(){

            //this.ViewModel.showChallengesViewController();
            
            this.SelectedIndex = 2;

        }





		List<UIViewController> viewControllers;
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			



			if (ViewModel == null)
				return;

			this.Delegate = new HomeTabBarDelegate(this, this.ViewModel);

			var menuItems = ViewModel.MenuItems;
			 viewControllers = new List<UIViewController>();
            var realizedMenuItems = menuItems.Where(x => x.MenuItem != MenuItem.Logout && x.Visible).ToList();
			selected = null;

			foreach (var item in realizedMenuItems)
			{

                //Create Post
                // item.Target = typeof(ActivityViewModel);

                    var tabv = CreateTabFor(item);

					if (SelectedTabDefault.HasValue && SelectedTabDefault.Value == tabv.MenuItem)
					{
						selected = tabv;
						SelectedTabDefault = null;
					}



                    viewControllers.Add(tabv);
                
                
                
				
			}

			if (selected == null)
            {

				this.TabBar.UnselectedItemTintColor = UIColor.White;
				//change to 0
				selected = viewControllers[1];

                if(!string.IsNullOrEmpty(this.ViewModel.InitialVC)){

                    selected = viewControllers[Int32.Parse(this.ViewModel.InitialVC)];

                }

				SetViewControllers(viewControllers.ToArray(), false);

				this.TabBar.TintColor = UIColor.White; ;
				this.TabBar.SelectedImageTintColor = UIColor.FromRGB(246, 194, 96);
				this.TabBar.BarTintColor = UIColor.FromRGB(75, 171, 229);
				
               
				//UITabBar.Appearance.SelectedImageTintColor = PorpoiseColors.Orange;
				
				CustomizableViewControllers = new UIViewController[] { };

				SelectedViewController = selected;
                //SelectedViewController.View.BackgroundColor = PorpoiseColors.Orange;
                //SelectedViewController.TabBarController.TabBar.Items[2].Image = UIImage.FromFile("plusIcon.png");

               //SelectedViewController.TabBarController.TabBar.Items[2].ImageInsets = new UIEdgeInsets(6, 0, -6, 0);
			}

		}


		public virtual bool CloseChildViewModel(IMvxViewModel viewModel)
		{
			// current implementation assumes the ViewModel to close is the currently shown ViewController 
			var navController = SelectedViewController as UINavigationController;
			if (navController != null)
			{
				navController.PopViewController(true);
				return true;
			}
			return false;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			//set up for TabBar having more than 5 items
			//			var more = this.MoreNavigationController;
			//			more.Delegate = new MoreNavigationDelegate ();
			//
			//			//make the icons in the more menu Turquoise
			//			more.TopViewController.View.TintColor = PorpoiseColors.Turquoise;

		}

        

		private int _createdSoFarCount = 0;

		private SrendipTabNavigationController CreateTabFor(MenuItemViewModel menuItem)
		{
			string imageName;

			var controller = new SrendipTabNavigationController(menuItem);
            if (menuItem.MenuItem != MenuItem.LogHours && menuItem.Visible)
            {
                var request = new MvxViewModelRequest(menuItem.Target, null, null);

                var screen = Mvx.Resolve<IMvxIosViewCreator>().CreateView(request) as UIViewController;

                imageName = SelectTabImage(menuItem.MenuItem);
                SetTitleAndTabBarItem(screen, menuItem.Title, imageName);
                controller.PushViewController(screen, false);

            }
            else if(menuItem.MenuItem == MenuItem.LogHours) {

                //var screen = new AddCoworkerViewController();

			

            }

			return controller;
		}

		private string SelectTabImage(MenuItem menuItem)
		{
			string imageName;
			switch (menuItem)
			{
				case MenuItem.Activity:
					imageName = "HomeIcon";
					break;
				case MenuItem.Profile:
					imageName = "ProfileIcon";
					break;
				case MenuItem.Premium:
					imageName = "FireIcon";
                    break;
				/*case MenuItem.Profile:
					imageName = "plusIcon";
					break;
				case MenuItem.Intercom:
					imageName = "intercom";
					break;*/
                default:
					imageName = null;
					break;
			}
			return imageName;
		}


		private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
		{
			//screen.Title = title;
			screen.TabBarItem = new UITabBarItem(null, GetTabImage(imageName), GetTabImage(imageName, true));
            UIImage image = GetTabImage(imageName, false);



            screen.TabBarItem.Tag = _createdSoFarCount;
            //screen.TabBarItem.Image = image.Scale(new CoreGraphics.CGSize(30,30));
			//Tab bar icon in the middle
			screen.TabBarItem.ImageInsets = new UIEdgeInsets(6, 0, -6, 0);

			
            //screen.TabBarItem.Image = screen.TabBarItem.Image.Scale(new CoreGraphics.CGSize(Services.PorpoiseImage.getAppropriateWidth(screen.TabBarItem.Image,(nfloat)(this.TabBar.Frame.Height * .7)), (nfloat)(this.TabBar.Frame.Height * .7)));
			_createdSoFarCount++;
		}

		private UIImage GetTabImage(string imageName, bool selected = false)
		{
            

            return  UIImage.FromFile(imageName+".png");
		}

		public void ShowGrandChild(IMvxIosView view)
		{
			var currentNav = SelectedViewController as UINavigationController;
			currentNav.PushViewController(view as UIViewController, true);
		}

		public bool ShowView(IMvxIosView view)
		{
			if (TryShowViewInCurrentTab(view))
				return true;

			return false;
		}

		private bool TryShowViewInCurrentTab(IMvxIosView view)
		{
			var navigationController = (UINavigationController)this.SelectedViewController;
			navigationController.PushViewController((UIViewController)view, true);
			return true;
		}

		#region IHasDisplayHint implementation

		public DisplayHint Hint
		{
			get
			{
				var hint = new DisplayHint();
				hint.FullScreen = true;
				hint.ClearToRoot = true;
				hint.Animate = false;
				return hint;
			}
		}

		#endregion

		public static MenuItem? SelectedTabDefault
		{
			get;
			set;
		}
	}
}
