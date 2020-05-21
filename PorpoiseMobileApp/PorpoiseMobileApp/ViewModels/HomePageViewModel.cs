using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using MvvmCross.Platform;

namespace PorpoiseMobileApp.ViewModels
{
    public class HomePageViewModel : SrendipViewModel<HomePageViewModel>
    {
        public HomePageViewModel()
        {
        }
        public string profile_image;

        public string profile_id;

        public double latitude;

        public double longitude;

        public string message;


        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        public HomePageViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this._imageService = Mvx.Resolve<IImageService>();

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {

                if (parameters.Data.ContainsKey("profile_image"))
                {
                    profile_image = parameters.Data["profile_image"];

                    Debug.WriteLine("URL IMAGE "+ profile_image);
                }

                if (parameters.Data.ContainsKey("profile_id"))
                {
                    profile_id = parameters.Data["profile_id"];

                    Debug.WriteLine("USER ID "+ profile_id);
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());

            }

        }

        public string  ProfileImage
        {
            get
            {
                return this.profile_image;
            }
            set
            {
                this.profile_image = value;
                RaisePropertyChanged(() => ProfileImage);
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        public void ShowSettings()
        {

            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("Preview", "Home");

            ShowViewModel<SettingsPageViewModel>(param);
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


            catch (Exception ex)
            {

            }
        }

        public async void GetProfilesDetails()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.GetOtherProfile(latitude,longitude);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("Near Profiles Count " + result.data.id);

                    profile_id = result.data.id;

                    profile_image = result.data.image_url;

                    RaisePropertyChanged(() => ProfileImage);


                    message = result.message;

                    RaisePropertyChanged(() => Message);

                    //Debug.WriteLine(ProfileBubbles.Count);


                }
                else if (result != null && result.code.Equals(204))
                {
             
                        message = result.message;

                        RaisePropertyChanged(() => Message);
                    
                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }

                else
                {
                    Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);
                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        public void ShowProfile()
        {

            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("profile_id", profile_id);

            ShowViewModel<TappedProfileViewModel>(param);
        }
    }
}
