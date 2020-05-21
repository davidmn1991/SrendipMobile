using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using PorpoiseMobileApp.Services;
using Acr.Settings;
using System.Diagnostics;
using System.Net.Http;

namespace PorpoiseMobileApp.ViewModels
{
    public class LoadingViewModel : SrendipViewModel<LoadingViewModel>
    {
        private bool _inFlight;
        public string language;
        private IMvxCommand _authenticateCommand;

        public LoadingViewModel(ISettings settings, IPorpoiseWebApiClient client)
        {
            this.client = client;
            this.settings = settings;
           
        }

        public bool InFlight
        {
            get
            {

                return _inFlight;
            }

            set
            {
                _inFlight = value;
                RaisePropertyChanged(() => InFlight);
                RaisePropertyChanged(() => AuthenticateCommand);

            }

        }

        public IMvxCommand AuthenticateCommand
        {

            get
            {
                return _authenticateCommand ?? (_authenticateCommand = new MvxCommand(PerformAuthentication, () => !InFlight));
            }
        }
        
        bool authenticated = false;
        private ISettings settings;
        private IPorpoiseWebApiClient client;
        private async void PerformAuthentication()
        {

            InFlight = false;
            try
            {
                //if (settings.Contains(AccountInfo.TOKENKEY))
                //{
                //    var token = settings.Get<string>(AccountInfo.TOKENKEY);
                //    AccountInfo.Token = token;
                //    authenticated = string.IsNullOrEmpty(AccountInfo.Token);
                //}
                var a = settings.Contains(AccountInfo.TOKENKEY);
                var b = settings.Get<string>(AccountInfo.TOKENKEY);

                if (settings.Contains(AccountInfo.user_email))
                {
                    AccountInfo.Profile_email = settings.Get<string>(AccountInfo.user_email);
                }
                
                authenticated = settings.Contains(AccountInfo.TOKENKEY) && settings.Get<string>(AccountInfo.TOKENKEY).Length > 0;


				if (!ImageController.Images.ContainsKey("key"))
				{

					await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-cdn/mobile-assets/key.png", "key");

				}

				//Add envelope image to map
				if (!ImageController.Images.ContainsKey("envelope"))
				{
					await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-mobile-assets/RequestAccountViewController/envelope.png", "envelope");
				}

                if (!authenticated)
                {
                    Debug.WriteLine(AccountInfo.token_id);

                    InFlight = true;
                    ShowViewModel<IntroductionViewModel>();

                }
                else
                {
                    //AccountInfo.language = "EN";

                    AccountInfo.token_id = settings.Get<string>(AccountInfo.TOKENKEY);

                    PerformAuthenticateEvent(true);
                    /*
                        var param = new System.Collections.Generic.Dictionary<string, string>();

                        param.Add("initial", "1");

                    ShowViewModel<HomeViewModel>(param);*/

                    ShowViewModel<AllProfilesViewModel>();

                    

                    InFlight = true;

                    
                    
                }

				
            }
            catch (Exception ex)
            //TODO: ensure to log error
            {
                
            }
        }

        private void PerformAuthenticateEvent(bool success, string message = null)
        {
            if (AuthenticateEvent != null)
            {
				AuthenticateEvent(this, new SdkEventArgs(success, message));
            }
        }

        public event EventHandler<SdkEventArgs> AuthenticateEvent;
    }
}

