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
    public class SingleSavedViewModel : SrendipViewModel<SingleSavedViewModel>
    {
        public SingleSavedViewModel()
        {
        }

        public List<UserBubbles> ProfileBubbles = new List<UserBubbles>();

        public ProfileDetails TappedProfiles = new ProfileDetails();

        public string image_url;

        public string profile_id;

        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        public string profile_name;

        public string profile_age;

        public bool save_profile = false;

        public SingleSavedViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
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

                if (parameters.Data.ContainsKey("profile_id"))
                {
                    profile_id = parameters.Data["profile_id"];

                    Debug.WriteLine("USER ID " + profile_id);
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());

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

        public string UserName
        {
            get
            {
                return this.profile_name;
            }
            set
            {
                this.profile_name = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public bool SavedProfile
        {
            get
            {
                return this.save_profile;
            }
            set
            {
                this.save_profile = value;
                RaisePropertyChanged(() => SavedProfile);
            }
        }

        public void GoBack()
        {
            ShowViewModel<SavedProfilesViewModel>();
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

                    if (!string.IsNullOrEmpty(result.data.first_name))
                    {
                        profile_name = result.data.first_name;

                    }

                    if (!string.IsNullOrEmpty(result.data.age.ToString()))
                    {
                        profile_age = result.data.age.ToString();

                    }

                    if (!string.IsNullOrEmpty(result.data.image_url))
                    {
                        image_url = result.data.image_url;

                    }

                    if (!result.data.user_bubble.Count.Equals(0))
                    {

                        foreach (UserBubbles userBubbles in result.data.user_bubble)
                        {
                            ProfileBubbles.Add(userBubbles);
                        }

                        RaisePropertyChanged(() => Profile_bubbles);

                    }

                    RaisePropertyChanged(() => UserName);

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

        public async void SaveProfile()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                if (save_profile.Equals(false))
                {

                    var result = await client.SaveProfile(profile_id);
                    Debug.WriteLine("RESULT");
                    if (result != null && result.code.Equals(200))
                    {
                        Debug.WriteLine("Near Profiles Count " + result.message);

                        save_profile = true;

                        RaisePropertyChanged(() => SavedProfile);

                    }
                    else
                    {

                        //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                    }
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

            param.Add("Preview", "Home");

            ShowViewModel<SettingsPageViewModel>(param);
        }
    }
}
