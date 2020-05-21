
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
    public class LoginFirstViewModel : SrendipViewModel<LoginFirstViewModel>
    {
        public LoginFirstViewModel()
        {
        }

        public string email;

        public string password;

        public string first_name;

        public string last_name;

        public string gender;

        public string birth_date;

        public string provider_id;

        public string errorMessage;

        private const string TOKEN_KEY = "auth.token";

        private const string user_email = "user_email";

        public UserLoginProviderRequestModel user_login_provider = new UserLoginProviderRequestModel();

        private readonly IPorpoiseWebApiClient client;

        private readonly ISettings settings;

        public LoginFirstViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this.settings = settings;
            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        public void BackIntroduction()
        {

            ShowViewModel<IntroductionViewModel>();
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

                RaisePropertyChanged(() => errorMessage);
            }
        }
        public async void Login()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.Login(email, password);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.data);

                    AccountInfo.token_id= result.data;
                    AccountInfo.Profile_email = email;
                    ShowViewModel<AllProfilesViewModel>();
                    settings.Set(TOKEN_KEY, result.data);
                    settings.Set(user_email, email);

                }
                else
                {
                     errorMessage = result.message;
                    RaisePropertyChanged(() => ErrorMessage);
                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);
                    

                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        public void ShowSignUp()
        {
            ShowViewModel<Signup1ViewModel>();
        }

        public async void LoginOauth()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.LoginOauth(user_login_provider.user_provider_id, user_login_provider.provider);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.data);

                    AccountInfo.token_id = result.data;
                    AccountInfo.Profile_email = email;
                    settings.Set(TOKEN_KEY, result.data);
                    settings.Set(user_email, email);

                    ShowViewModel<AllProfilesViewModel>();

                }
                else
                {
                    errorMessage = result.message;
                    RaisePropertyChanged(() => ErrorMessage);
                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        public async void EmailVerification()
        {

            try
            {


                var result = await client.EmailVerification(null,provider_id);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.data);
                    if (result.data.Equals(false))
                    {

                        RegisterOauthUser();
                    }
                    else
                    {
                        LoginOauth();
                    }


                }
                else
                {
                    errorMessage = result.message;
                    RaisePropertyChanged(() => ErrorMessage);
                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        private async void RegisterOauthUser()
        {

            try
            {


                var result = await client.RegisterOauthUser(email, first_name, last_name, gender ,birth_date ,user_login_provider);
                Debug.WriteLine("RESULT");
                if (result != null && result.Successful)
                {
                    LoginOauth();

                    //ShowViewModel<IntroductionViewModel>();

                }
                else
                {
                    //error_message = result.message;
                    //RaisePropertyChanged(() => errorMessage);
                    Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }
    }
}
