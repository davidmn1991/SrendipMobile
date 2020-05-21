
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
    public class Signup2ViewModel : SrendipViewModel<Signup2ViewModel>
    {
        public Signup2ViewModel()
        {
        }

        public string viewController;

        public string email;

        public string password;

        public string name;

        public string last_name;

        public string event_name;

        public bool errorMessage;

        private readonly IPorpoiseWebApiClient client;

        public Signup2ViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {

                if (parameters.Data.ContainsKey("ViewController"))
                {
                    viewController= parameters.Data["ViewController"];

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

                if (parameters.Data.ContainsKey("email"))
                {
                   email = parameters.Data["email"];

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

                if (parameters.Data.ContainsKey("event_name"))
                {
                    event_name = parameters.Data["event_name"];

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

                

                if (parameters.Data.ContainsKey("password"))
                {
                    password = parameters.Data["password"];

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

                if (parameters.Data.ContainsKey("name"))
                {
                    name = parameters.Data["name"];

                    Debug.WriteLine("name " + name);
                }

                if (parameters.Data.ContainsKey("last_name"))
                {
                    last_name = parameters.Data["last_name"];

                    Debug.WriteLine("last name " + last_name);
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());

            }

        }

        public void BackSignup1()
        {

            if (!string.IsNullOrEmpty(viewController))
            {

                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("ViewController", "Event");

                param.Add("name", name);

                param.Add("last_name", last_name);

                ShowViewModel<EventFirstViewModel>(param);
            }
            else
            {

                ShowViewModel<Signup1ViewModel>();
            }

            
        }

        public bool ErrorMessage
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
        public async void EmailVerification()
        {

            try
            {


                var result = await client.EmailVerification(email, null);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.data);


                    errorMessage = result.data;
                    if (errorMessage.Equals(true))
                    {
                        RaisePropertyChanged(() => ErrorMessage);

                    }
                    else
                    {
                        showSignup3();
                    }
                    
                }
                else
                {
                    errorMessage = result.data;
                    RaisePropertyChanged(() => ErrorMessage);
                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);
                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }
        public void showSignup3()
        {

            if (!string.IsNullOrEmpty(viewController))
            {

                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("ViewController", "Event");

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                param.Add("event_name", event_name);

                ShowViewModel<Signup4ViewModel>(param);
            }
            else {

                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                ShowViewModel<Signup3ViewModel>(param);
            }

            
        }
    }
}
