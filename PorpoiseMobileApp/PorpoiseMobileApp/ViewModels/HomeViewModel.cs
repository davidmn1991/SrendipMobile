﻿using System;
using PorpoiseMobileApp.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Acr.Settings;
using MvvmCross.Platform;
using System.Collections;
using PorpoiseMobileApp.Client;

namespace PorpoiseMobileApp.ViewModels
{
	public class HomeViewModel : SrendipViewModel<HomeViewModel>
	{
		private ObservableCollection<MenuItemViewModel> menuItems;
		private ISettings _settings;

        private bool _editing = false;

        private string initialVC;

		public double latitude;

		public double longitude;

		public string profile_image;

		public string profile_id;

        private string viewController;

        public string ViewController{


            get{

                return this.viewController;

            }

            set{

                this.viewController = value;

            }

        }

        public bool Editing {

            get{

                return _editing;

            }
            set{

                _editing = value;

            }

        }

        public string InitialVC{

            get{

                return this.initialVC;

            }
            set{

                this.initialVC = value;

                RaisePropertyChanged(()=>InitialVC);

            }

        }
      

		public ObservableCollection<MenuItemViewModel> MenuItems
		{
			get
			{
				return menuItems;
			}
		}
        IPorpoiseWebApiClient client;
        public HomeViewModel(IPorpoiseWebApiClient client)
		{
			Signup4ViewModel.profile_photo = null;

			this._settings = Mvx.Resolve<ISettings>();
            this.client = client;
			if (AccountInfo.first_time == 0)
			{
				menuItems = new ObservableCollection<MenuItemViewModel>() {
				new MenuItemViewModel (() => Resource.Profile) { MenuItem = MenuItem.Profile, Target = typeof(MyProfileViewModel), Visible=true },
				new MenuItemViewModel (() => Resource.Activity) { MenuItem = MenuItem.Activity, Target = typeof(HomePageViewModel), Visible=true },
                //new MenuItemViewModel (() => Resource.Post){ MenuItem = MenuItem.LogHours, Target= typeof(IntroductionViewModel), Visible  = true},
                new MenuItemViewModel(()=> Resource.Settings) {MenuItem = MenuItem.Premium, Target=typeof(FireModeViewModel), Visible = true},
                //new MenuItemViewModel (() => Resource.Support) { MenuItem = MenuItem.Intercom, Target = typeof(ProfileViewModel), Visible=true},
                //new MenuItemViewModel(() => Resource.Challenges) { MenuItem = MenuItem.Challenges, Target = typeof(ChallengeLogHourViewModel), Visible = false },
                //new MenuItemViewModel(() => Resource.Challenges) { MenuItem = MenuItem.LogHourPostPhoto, Target = typeof(LogHourPhotoViewModel), Visible = false },
                //new MenuItemViewModel(() => Resource.Challenges) { MenuItem = MenuItem.CreatePost, Target = typeof(LogPostOrganizationViewModel), Visible = false },
                new MenuItemViewModel (() => Resource.Logout) {
					MenuItem = MenuItem.Logout,
					Visible = false,
					Action = () => {
						AccountInfo.Logout();
						PerformLogoutEvent (true, "");
						ShowViewModel<LoginFirstViewModel> ();
					}
				}

				};
			}
            else
            {
				
					menuItems = new ObservableCollection<MenuItemViewModel>() {
				new MenuItemViewModel (() => Resource.Profile) { MenuItem = MenuItem.Profile, Target = typeof(MyProfileViewModel), Visible=true },
				new MenuItemViewModel (() => Resource.Activity) { MenuItem = MenuItem.Activity, Target = typeof(HomePageViewModel), Visible=true },
                //new MenuItemViewModel (() => Resource.Post){ MenuItem = MenuItem.LogHours, Target= typeof(IntroductionViewModel), Visible  = true},
                new MenuItemViewModel(()=> Resource.Settings) {MenuItem = MenuItem.Premium, Target=typeof(FireModeViewModel), Visible = true},
                //new MenuItemViewModel (() => Resource.Support) { MenuItem = MenuItem.Intercom, Target = typeof(ProfileViewModel), Visible=true},
                //new MenuItemViewModel(() => Resource.Challenges) { MenuItem = MenuItem.Challenges, Target = typeof(ChallengeLogHourViewModel), Visible = false },
                //new MenuItemViewModel(() => Resource.Challenges) { MenuItem = MenuItem.LogHourPostPhoto, Target = typeof(LogHourPhotoViewModel), Visible = false },
                //new MenuItemViewModel(() => Resource.Challenges) { MenuItem = MenuItem.CreatePost, Target = typeof(LogPostOrganizationViewModel), Visible = false },
                new MenuItemViewModel (() => Resource.Logout) {
					MenuItem = MenuItem.Logout,
					Visible = false,
					Action = () => {
						AccountInfo.Logout();
						PerformLogoutEvent (true, "");
						ShowViewModel<LoginFirstViewModel> ();
					}
				}

				};

				}


		}

		public async void FlagTutorial()
		{
            

			Debug.WriteLine("Flagging tutorial in ViewModel");

			var employee = await client.GetEmployee();

			if (employee != null && employee.Successful)
			{

				Guid employeeUid = employee.Payload.EmployeeUid;

				var result = await client.FlagTutorial(employeeUid, "posting");

				if (result != null && result.Successful)
				{

					Debug.WriteLine("Tutorial flagged");

				}
			}

		}

		public async void UploadLocation()
		{


			Debug.WriteLine("Uploading Location");

			try
			{

				var result = await client.UploadLocation(latitude, longitude);

				if (result != null && result.Successful)
				{

					Debug.WriteLine("location uploaded");
				}
			}


            catch(Exception ex)
            {

            }
		}




		protected override void InitFromBundle(IMvxBundle parameters){

            try{

                if (parameters.Data.ContainsKey("initial"))
                {
                    var key = parameters.Data["initial"];

                    this.InitialVC = key;

                    Debug.WriteLine("REDIRECTING TO CONTROLLER "+this.InitialVC);
                }

				if (parameters.Data.ContainsKey("vc"))
				{
                    
					var key = parameters.Data["vc"];

                    this.ViewController = key;

					
				}


				if (parameters.Data.ContainsKey("profile_image"))
				{
					profile_image = parameters.Data["profile_image"];

					AccountInfo.first_profile_image = profile_image;

					Debug.WriteLine("URL IMAGE " + profile_image);
				}

				if (parameters.Data.ContainsKey("profile_id"))
				{
					profile_id = parameters.Data["profile_id"];
					AccountInfo.first_profile_id = profile_id;

					Debug.WriteLine("USER ID " + profile_id);
				}


				if (parameters.Data.ContainsKey("editing")){

                    Debug.WriteLine("YOU ARE EDITING");
                    
                    if (parameters.Data["editing"].Equals("true"))
                    {

                        this.Editing = true;
                    }
                    else{

                        this.Editing = false;

                    }
                }
                else{

                    this.Editing = false;

                }


            }
            catch(Exception ex){

                Debug.WriteLine(ex.ToString());

            }

        }

     

		private IMvxCommand _logoutCommand;

		public IMvxCommand LogoutCommand
		{
			get
			{
				return _logoutCommand ?? (_logoutCommand = new MvxCommand(Logout));
			}
		}


        public void showChallengesViewController(){

            this._settings = Mvx.Resolve<ISettings>();

            IDictionary dict = new Dictionary<string, string>();

            dict.Add("goal","14256372");

			MenuItemViewModel item =   new MenuItemViewModel(() => Resource.Post)
			{
				MenuItem = MenuItem.LogHours,
                //Target = typeof(ChallengeLogHourViewModel),			
                Parameters = dict
			};

            this.menuItems[2] = item;

        }

		public void Logout()
		{
			_settings.Remove(AccountInfo.EMAILID);
			_settings.Remove(AccountInfo.PASSWORDID);
			_settings.Remove(AccountInfo.USERKEY);
			_settings.Remove(AccountInfo.TOKENKEY);
			//good place to add anylitics or segment calls saying the user logged out
			ShowViewModel<LoginViewModel>();
		}

		public event EventHandler<SdkEventArgs> LogoutEvent;
		public void PerformLogoutEvent(bool success, string message)
		{
			if (LogoutEvent != null)
			{
				try
				{
					LogoutEvent(this, new SdkEventArgs(success, message));
				}
				catch (Exception ex)
				{
					LogoutEvent(this, new SdkEventArgs(false, ex.Message));
				}
			}
		}

		public event EventHandler<SdkEventArgs> OpenAccountSettingsEvent;
		public void PerformOpenSettingsWebviewEvent(bool success)
		{
			if (OpenAccountSettingsEvent != null)
			{
				try
				{

					OpenAccountSettingsEvent(this, new SdkEventArgs(success));
				}

				catch (Exception ex)
				{
					OpenAccountSettingsEvent(this, new SdkEventArgs(false, ex.Message));
				}
			}
		}


		private IMvxCommand _selectMenuItemCommand;

		public IMvxCommand SelectMenuItemCommand
		{
			get
			{
				return _selectMenuItemCommand ?? (_selectMenuItemCommand = new MvxCommand<MenuItemViewModel>(SelectMenuItem));
			}
		}

		private void SelectMenuItem(MenuItemViewModel item)
		{
			Debug.WriteLine("REDICTERING SelectMenuItem");
			//LogPost.clear();
			if (MenuItemClicked != null)
			{
				MenuItemClicked(this, new MenuItemClickEventArgs { MenuItem = item });
			}
			if (item.Target != null)
			{
				if (item.Parameters != null)
				{
                    Debug.WriteLine("PARAMETERS IS NOT NULL");
					ShowViewModel(item.Target, item.Parameters);
				}
				else {
					ShowViewModel(item.Target);
				}
			}
			else if (item.Action != null)
			{
				item.Action();
			}
		}

		public void ShowMenuItem(MenuItemViewModel item, Dictionary<string, string> args = null)
		{Debug.WriteLine("REDICTERING ShowMenuItem");
			if (args != null)
			{
				ShowViewModel(item.Target, new MvxBundle(args));
			}
			else
			{
				ShowViewModel(item.Target);

			}
		}

		public event EventHandler<MenuItemClickEventArgs> MenuItemClicked;

		public MenuItemViewModel GetMenuItemForViewModelType(Type type)
		{
			var result = MenuItems.FirstOrDefault(x => x.Target == type);
			return result;
		}

		public void GoToHome()
		{
			
		}
	}
}

