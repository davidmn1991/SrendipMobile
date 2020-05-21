using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using PorpoiseMobileApp.Models;
using MvvmCross.Platform;
using System.Diagnostics;

namespace PorpoiseMobileApp.ViewModels
{
    public class MyProfileViewModel : SrendipViewModel<MyProfileViewModel>
    {
        public MyProfileViewModel()
        {
        }

        public List<UserBubbles> ProfileBubbles = new List<UserBubbles>();

        public string image_url;

        public string profile_id = "profile";

        public string profile_name;

        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        public MyProfileViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this._imageService = Mvx.Resolve<IImageService>();

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
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

        public void ShowProfile()
        {

            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("Preview", "Myprofile");

            ShowViewModel<ProfileViewModel>(param);
        }

        public void ShowBubbleBoard()
        {


            ShowViewModel<BubbleBoardViewModel>();
        }

        public async void GetProfilesDetails()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.GetMyProfilesDetails();
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("Near Profiles Count " + result.data.first_name);


                    image_url = result.data.image_url;

                    profile_name = result.data.first_name;

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
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("Preview", "Myprofile");

            ShowViewModel<SettingsPageViewModel>(param);
        }
    }
}
