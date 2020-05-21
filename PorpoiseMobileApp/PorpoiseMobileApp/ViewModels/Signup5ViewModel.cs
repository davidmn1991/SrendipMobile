using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using MvvmCross.Platform;
using System.IO;

namespace PorpoiseMobileApp.ViewModels
{
    public class Signup5ViewModel : SrendipViewModel<Signup5ViewModel>
    {
        public Signup5ViewModel()
        {
        }

        public string viewController;

        public string error_message;

        public string email;

        public string password;

        public string name;

        public string last_name;

        public string date_of_birth;

        public string gender;

        public string textView;

        public string event_name;

        public byte[] bytes;

        private const string TOKEN_KEY = "auth.token";

        private const string user_email = "user_email";

        public UserEventRequestModel @event = new UserEventRequestModel();

        public string image = "";

        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        private readonly ISettings settings;

        public Signup5ViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this._imageService = Mvx.Resolve<IImageService>();
            this.settings = settings;

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {

                if (parameters.Data.ContainsKey("ViewController"))
                {
                    viewController = parameters.Data["ViewController"];

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

                if (parameters.Data.ContainsKey("email"))
                {
                    email = parameters.Data["email"];

                    Debug.WriteLine("email " + email);
                }

                if (parameters.Data.ContainsKey("event_name"))
                {
                    event_name = parameters.Data["event_name"];

                    Debug.WriteLine("event_name " + event_name);
                }

                if (parameters.Data.ContainsKey("password"))
                {
                    password = parameters.Data["password"];

                    Debug.WriteLine("password " + password);
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

                if (parameters.Data.ContainsKey("date_of_birth"))
                {
                    date_of_birth = parameters.Data["date_of_birth"];

                    Debug.WriteLine("date of birth " + date_of_birth);
                }

                if (parameters.Data.ContainsKey("gender"))
                {
                    gender = parameters.Data["gender"];

                    Debug.WriteLine("gender " + gender);
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());

            }

        }

        public async void ChangeImage()
        {

            try
            {
                //InFlight = true;
                // Save Image
                Stream stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);

                var keyName = "cognito:"+email+ "/profile-image/image.jpeg";

                image = "https://" + "srendip-dev" + ".s3.amazonaws.com/" + keyName;

                var imageResponse = await _imageService.ProcessImage(keyName, stream);
                if (imageResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    var PhotoUrl = "https://" + "srendip-dev" + ".s3.amazonaws.com/" + keyName;
                    Debug.WriteLine("PHOTOURL " + PhotoUrl);
                    var PhotoContentType = "image/jpeg";
                    var PhotoKeyName = keyName;

                    // expect the stream to be disposed after immediately this method returns.
                    // PerformUploadPhotoEvent(true);
                }
                else
                {
                    Debug.WriteLine(imageResponse.HttpStatusCode);
                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);

            }
        }

        public void BackSignup4()
        {

            if (!string.IsNullOrEmpty(viewController))
            {
                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("ViewController", "Event");

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                param.Add("gender", gender);

                param.Add("event_name", event_name);

                param.Add("date_of_birth", date_of_birth);

                ShowViewModel<Signup4ViewModel>(param);
                

            }
            else
            {
                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                param.Add("gender", gender);

                param.Add("date_of_birth", date_of_birth);

                ShowViewModel<Signup4ViewModel>(param);
            }

            
        }

        public void ShowNext()
        {

            if (!string.IsNullOrEmpty(viewController))
            {
                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("ViewController", "Event");

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                param.Add("gender", gender);

                param.Add("event_name", event_name);

                param.Add("date_of_birth", date_of_birth);

                param.Add("bio", textView);

                ShowViewModel<EventLastViewModel>(param);


            }
            else
            {
                

                RegisterUser();
                //ShowViewModel<Signup4ViewModel>();
            }


        }

        public string errorMessage
        {
            get
            {
                return this.error_message;
            }

            set
            {
                this.error_message = value;

                RaisePropertyChanged(() => errorMessage);
            }
        }

        private async void RegisterUserEvent()
        {

            try
            {

                var result = await client.RegisterUserEvent("davidmn1991@mailing.com", "12345678", "User", "Event", "blabal", "", "MALE", "1991-07-10", @event);
                Debug.WriteLine("RESULT");
                if (result != null && result.Successful)
                {


                    ShowViewModel<IntroductionViewModel>();

                }
                else
                {
                    error_message = result.message;
                    RaisePropertyChanged(() => errorMessage);
                    Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

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

                    AccountInfo.token_id = result.data;
                    AccountInfo.Profile_email = email;
                    ShowViewModel<AllProfilesViewModel>();
                    settings.Set(TOKEN_KEY, result.data);
                    settings.Set(user_email, email);

                }
                else
                {
                    errorMessage = result.message;
                    
                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        private async void RegisterUser()
        {

            try
                {
                    
                    var result = await client.RegisterUser(email,password,name,last_name,textView,image,gender,date_of_birth);
                    Debug.WriteLine("RESULT");
                    if (result != null && result.Successful)
                    {
                        bytes = Signup4ViewModel.profile_photo;

                        ChangeImage();

                        Login();

                    }
                    else
                    {
                    error_message = result.message;
                    RaisePropertyChanged(() => errorMessage);
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
