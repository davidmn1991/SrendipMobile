
using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PorpoiseMobileApp.ViewModels
{
	public class LoginViewModel : SrendipViewModel<LoginViewModel>
	{
		private string _email;
		private bool _inFlight;
		private string _password;
		private bool _firstTimeUser = false;
		private readonly IPorpoiseWebApiClient client;
		private readonly ISettings settings;
		private IEncryptionService encryption;
		private IMvxCommand _signInCommand;
		private byte[] porpoiseLogo;
		private byte[] envelopeIcon;
		private byte[] keyIcon;



		private const string TOKEN_KEY = "auth.token";


		public LoginViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
		{
			this.client = client;
			this.settings = settings;
			this.encryption = encryption;
			Debug.WriteLine("LOADING LOGIN IMAGE");
			//Add key image to map
		}

		

		public byte[] EnvelopeIcon
		{

			get
			{

				return envelopeIcon;

			}
			set
			{

				envelopeIcon = value;

				RaisePropertyChanged(() => EnvelopeIcon);

			}

		}

		public byte[] KeyIcon
		{

			get
			{

				return keyIcon;

			}
			set
			{

				keyIcon = value;

				RaisePropertyChanged(() => KeyIcon);

			}

		}

		public byte[] PorpoiseLogo
		{

			get
			{

				return porpoiseLogo;

			}
			set
			{

				porpoiseLogo = value;

				RaisePropertyChanged(() => PorpoiseLogo);

			}

		}

		public async void Init()
		{
			//Add key to map

			if (!ImageController.Images.ContainsKey("key"))
			{

				await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-cdn/mobile-assets/key.png", "key");

			}

			//Add envelope image to map
			if (!ImageController.Images.ContainsKey("envelope"))
			{
				await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-mobile-assets/RequestAccountViewController/envelope.png", "envelope");
			}

			//Add porpoise image to map}
			if (!ImageController.Images.ContainsKey("porpoiseHeader"))
			{
				await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-mobile-assets/Image+1.png", "porpoiseHeader");
			}
			//Load Avatar Image
			if (!ImageController.Images.ContainsKey("avatar"))
			{
				await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-mobile-assets/RequestAccountViewController/avatar+(1).png", "avatar");
			}

			//Load company icon
			if (!ImageController.Images.ContainsKey("company"))
			{
				await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-mobile-assets/RequestAccountViewController/building+(1).png", "company");
			}
			if (!ImageController.Images.ContainsKey("check"))
			{
				//Load checkmark
				await ImageController.AddImageFromUrl("https://s3.amazonaws.com/porpoise-cdn/mobile-assets/checked.png", "check");
			}

			if (ImageController.Images.ContainsKey("porpoiseHeader"))
			{

				PorpoiseLogo = ImageController.Images["porpoiseHeader"];
			}

			if (ImageController.Images.ContainsKey("envelope"))
			{

				EnvelopeIcon = ImageController.Images["envelope"];
			}

			if (ImageController.Images.ContainsKey("key"))
			{

				KeyIcon = ImageController.Images["key"];
			}



		}


		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
				RaisePropertyChanged(() => Email);
				RaisePropertyChanged(() => SignInCommand);
			}
		}

		public bool EmailValid
		{
			get
			{
				return !string.IsNullOrEmpty(Email) && Validation.IsEmail(Email);
			}
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
				RaisePropertyChanged(() => SignInCommand);
			}
		}

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				RaisePropertyChanged(() => Password);
				RaisePropertyChanged(() => SignInCommand);
			}
		}

		public bool PasswordValid
		{
			get
			{
				return Validation.IsPassword(Password);
			}
		}


		public IMvxCommand SignInCommand
		{
			get
			{
				_signInCommand = new MvxCommand(PerformSignIn, () => !InFlight);
				return _signInCommand;
			}
		}


		private async void PerformSignIn()
		{
			if (EmailValid && PasswordValid)
			{
				InFlight = true;
				try
				{
					Debug.WriteLine(Email + " " + Password);
					var result = await client.PerformSignIn(Email, Password);
					Debug.WriteLine("RESULT");
					if (result != null && result.Successful)
					{
						PerformSignInEvent(true);
						AccountInfo.Email = Email;
						var employee = await client.GetEmployee();
						AccountInfo.UserId = employee.Payload.UserId;
						settings.Set(TOKEN_KEY, result.Token);

						AccountInfo.CompanyName = employee.Payload.CompanyName;

						Debug.WriteLine("SHOW PROFILE TUTORIAL: " + result.Payload.show_profile_tutorial);
						Debug.WriteLine("SHOW POST TUTORIAL: " + result.Payload.show_posting_tutorial);

						AccountInfo.show_posting_tutorial = result.Payload.show_posting_tutorial;

						AccountInfo.show_profile_tutorial = result.Payload.show_profile_tutorial;

						Debug.WriteLine("COMPANY NAME " + AccountInfo.CompanyName);

						//ShowViewModel<HomeViewModel>();
						AccountInfo.goDirectPosting = false;
						
							ShowViewModel<HomeViewModel>();
						



					}
					else
					{

						Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.Message);
						PerformSignInEvent(false, result.Message);
					}
				}
				catch (HttpRequestException hre)
				{
					Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");
					PerformSignInEvent(false, string.IsNullOrEmpty(hre.Message) ? Resource.LoginError : hre.Message);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("ERROR PERFORMING SIGN IN EXCEPTION " + ex.Message);
					// ShowViewModel<HomeViewModelAndroid>();
					//PerformSignInEvent(false, string.IsNullOrEmpty(ex.Message) ? Resource.LoginError : ex.Message);
				}
				InFlight = false;
			}
			else
			{
				RaisePropertyChanged(() => Email);
				RaisePropertyChanged(() => Password);
				Debug.WriteLine("ERROR PERFORMING SIGN IN WRONG EMAIL AND PASSWORD");
				PerformSignInEvent(false, Resource.LoginError);
			}
		}

		private void PerformSignInEvent(bool success, string message = null)
		{
			if (SignInEvent != null)
			{
				try
				{
					Debug.WriteLine("MESSAGE IN SIGN IN EVENT " + message);

					SignInEvent(this, new SdkEventArgs(success, message));
				}
				catch (Exception ex)
				{
					SignInEvent(this, new SdkEventArgs(false, ex.Message));
				}
			}
		}

		public event EventHandler<SdkEventArgs> SignInEvent;

		public string ForgotPasswordURL
		{
            get
			{
				return "https://app.porpoise.com/forgotpassword";
			}

		}

		public string JoinURL
		{
			get { return "https://connect.giving.company/join"; }
		}
	}
}
