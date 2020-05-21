using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using MvvmCross.Platform;

namespace PorpoiseMobileApp.ViewModels
{
    public class Signup4ViewModel : SrendipViewModel<Signup4ViewModel>
    {
        public Signup4ViewModel()
        {
        }
        public string viewController;

        public string email;

        public string password;

        public string name;

        public string last_name;

        public string date_of_birth;

        public string gender;

        public string event_name;

        public byte[] Bytes;

        public static byte[] profile_photo;

        private IImageService _imageService;

        private IPorpoiseWebApiClient client;


        public Signup4ViewModel(IPorpoiseWebApiClient client)
        {
            this.client = client;
            //this._pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
            //this._settings = Mvx.Resolve<ISettings>();
            this._imageService = Mvx.Resolve<IImageService>();
            //this._rotator = Mvx.Resolve<IImageRotateService>();
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

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

                if (parameters.Data.ContainsKey("event_name"))
                {
                    event_name = parameters.Data["event_name"];

                    Debug.WriteLine("event_name" + event_name);
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

                if (parameters.Data.ContainsKey("date_of_birth"))
                {
                    date_of_birth = parameters.Data["date_of_birth"];

                    Debug.WriteLine("date_of birth " + date_of_birth);
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

        public void BackSignup3()
        {

            if (!string.IsNullOrEmpty(viewController))
            {
                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("ViewController", "Event");

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("event_name", event_name);

                ShowViewModel<Signup2ViewModel>(param);

            }
            else
            {

                var param = new System.Collections.Generic.Dictionary<string, string>();


                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                ShowViewModel<Signup3ViewModel>(param);

            }

            
        }

        public async void ChangeImage()
        {

            try
            {
                //InFlight = true;
                // Save Image
                Stream stream = new MemoryStream(Bytes);
                stream.Seek(0, SeekOrigin.Begin);

                var keyName = "davidmn1991@hotmail.com" + "/profile-image/image.jpeg";

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

        public void ShowSignup5()
        {

            if (!string.IsNullOrEmpty(viewController))
            {
                //ChangeImage();

                var param = new System.Collections.Generic.Dictionary<string, string>();

                var photo = new System.Collections.Generic.Dictionary<string, byte[]>();

                param.Add("ViewController", "Event");

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                param.Add("gender", gender);

                param.Add("event_name", event_name);

                param.Add("date_of_birth", date_of_birth);

                profile_photo = Bytes;

                ShowViewModel<Signup5ViewModel>(param);

            }

            else
            {

                //ChangeImage();

                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("name", name);

                param.Add("last_name", last_name);

                param.Add("email", email);

                param.Add("password", password);

                param.Add("gender", gender);

                param.Add("date_of_birth", date_of_birth);

                profile_photo = Bytes;

                ShowViewModel<Signup5ViewModel>(param);
            }

            
        }
    }
}
