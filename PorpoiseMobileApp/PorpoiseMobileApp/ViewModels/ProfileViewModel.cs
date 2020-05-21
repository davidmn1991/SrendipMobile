using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using MvvmCross.Platform;
using System.Collections.Generic;
using PorpoiseMobileApp.Models;
using System.IO;

namespace PorpoiseMobileApp.ViewModels
{
    public class ProfileViewModel : SrendipViewModel<ProfileViewModel>
    {
        public ProfileViewModel()
        {
        }

        public List<UserBubbles> ProfileBubbles = new List<UserBubbles>();

        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        public byte[] Bytes;

        public string url_image;

        public ProfileViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this._imageService = Mvx.Resolve<IImageService>();

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        public string bubble_selected;

        public string image_url;

        public string profile_id = "profile";

        public string preview = "";

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {

                if (parameters.Data.ContainsKey("Preview"))
                {
                    preview = parameters.Data["Preview"];

                    Debug.WriteLine("PRE VIEW MODEL " + preview);
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
                Stream stream = new MemoryStream(Bytes);
                stream.Seek(0, SeekOrigin.Begin);

                var keyName = "cognito:" + AccountInfo.Profile_email + "/profile-image/image.jpeg";

                //image = "https://" + "srendip-dev" + ".s3.amazonaws.com/" + keyName;

                var imageResponse = await _imageService.ProcessImage(keyName, stream);
                if (imageResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    var PhotoUrl = "https://" + "srendip-dev" + ".s3.amazonaws.com/" + keyName;
                    url_image = PhotoUrl;
                    Debug.WriteLine("PHOTOURL " + PhotoUrl);
                    var PhotoContentType = "image/jpeg";
                    var PhotoKeyName = keyName;
                    UploadProfileImage();

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

        public List<UserBubbles> Profile_bubbles
        {
            get
            {
                return this.ProfileBubbles;
            }
            set
            {
                this.ProfileBubbles = value;
                RaisePropertyChanged(() => Profile_bubbles);
            }
        }

        public async void UploadProfileImage()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.UploadImageProfile(url_image);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {


                    Debug.WriteLine(ProfileBubbles.Count);


                }
                else
                {

                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        public string ImageUrl
        {
            get
            {
                return this.image_url;
            }
            set
            {
                this.image_url = value;
                RaisePropertyChanged(() => ImageUrl);
            }
        }

        public async void GetProfilesDetails()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.GetProfilesDetails(profile_id);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("Near Profiles Count " + result.data.first_name);


                    image_url = result.data.image_url;

                    RaisePropertyChanged(() => ImageUrl);


                    if (!result.data.user_bubble.Count.Equals(0))
                    {

                        foreach (UserBubbles userBubbles in result.data.user_bubble)
                        {
                            ProfileBubbles.Add(userBubbles);
                        }

                        RaisePropertyChanged(() => Profile_bubbles);

                    }


                    Debug.WriteLine(ProfileBubbles.Count);


                }
                else
                {

                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        public void ShowSettings()
        {
            if (preview.Equals("Myprofile"))
            {
                var param = new System.Collections.Generic.Dictionary<string, string>();

                param.Add("initial", "0");

                ShowViewModel<HomeViewModel>(param);

            }

            else
            {

                ShowViewModel<SettingsPageViewModel>();
            }
        }

        public void ShowBubbleBoard()
        {

            ShowViewModel<BubbleBoardViewModel>();
        }


    }
}
