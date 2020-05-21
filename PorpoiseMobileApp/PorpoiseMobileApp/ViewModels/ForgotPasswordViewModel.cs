using System;
using System.Diagnostics;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Android.Widget;

namespace PorpoiseMobileApp.ViewModels
{
    public class ForgotPasswordViewModel : PorpoiseViewModel<ForgotPasswordViewModel>
    {
        IPorpoiseWebApiClient _client;

        public Android.Content.Context _context;

        private byte[] porpoiseLogo;

		private byte[] computerIcon;

        private string errorMessage = "";

		public byte[] ComputerIcon { 
		
			get {

				if (computerIcon == null) {

					Debug.WriteLine("COMPUTER ICON IS NULL");

					if (ImageController.Images.ContainsKey("computericon"))
					{

						computerIcon = ImageController.Images["computericon"];

					}
					else { 
					
						Debug.WriteLine("computericon not found in map");
					
					}
				
				}
				if (computerIcon != null) {

					Debug.WriteLine("COMPUTER ICON IS NOT NULL");
				
				}
				return computerIcon;
			}
		
		}

		public byte[] PorpoiseLogo { 
		
			get {

				if (porpoiseLogo == null) {

					if (ImageController.Images.ContainsKey("porpoiseHeader")) {

						porpoiseLogo = ImageController.Images["porpoiseHeader"];
					
					}
				
				}

				return porpoiseLogo;
			
			}
		
		}

        public string ErrorMessage
        {

            get
            {

                return this.errorMessage;

            }
            set
            {

                this.errorMessage = value;

                RaisePropertyChanged(() => ErrorMessage);

            }

        }

        public ForgotPasswordViewModel()
        {
        }

        public ForgotPasswordViewModel(IPorpoiseWebApiClient client)
        {

            _client = client;

        }

        private IMvxCommand _backCommand;

        private IMvxCommand _continueCommand;

        private string _email;

        public string Email{

            get{

                return this._email;

            }
            set{

                this._email = value;

                RaisePropertyChanged(()=>this.Email);

            }

        }


        public IMvxCommand RequestCommand        {

            get{

                _continueCommand = new MvxCommand(Continue);

                return _continueCommand;

            }

        }

        public IMvxCommand BackCommand{

            get{

                _backCommand = new MvxCommand(Back);

                return _backCommand;

            }

        }

        private async void Continue()
        {
            
            try
            {
                var response = await _client.ForgotPassword(Email);
                if(response!= null && response.Successful)
                {
                    Debug.WriteLine("Forgot Password -- response.Successful");
                    Toast.MakeText(_context, "Alright ! We've sent you an email to help you reset your password. Go Check it out !", ToastLength.Long).Show();
                    
                    ShowViewModel<LoginViewModel>();
                }
                else if (response != null){

                    this.ErrorMessage = response.Message;

                }
                else
                {
                    this.ErrorMessage = "User not found";
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());
                this.ErrorMessage = "User not found";

            }
        }
        private void Back(){

            ShowViewModel<LoginViewModel>();

        }


		
    }
}
