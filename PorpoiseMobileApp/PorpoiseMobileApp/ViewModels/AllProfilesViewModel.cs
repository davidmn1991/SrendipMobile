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
using System.Collections.Generic;
using PorpoiseMobileApp.Models;

namespace PorpoiseMobileApp.ViewModels
{
    public class AllProfilesViewModel : SrendipViewModel<AllProfilesViewModel>
    {
        public AllProfilesViewModel()
        {
        }

        public int page = 1;

        public int items = 9;

        public int total_pages;

        public double latitude;

        public double longitude;

        public string profile_image;

        public string profile_id;

        public List<NearProfiles> allprofiles = new List<NearProfiles>();



        

        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        public AllProfilesViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this._imageService = Mvx.Resolve<IImageService>();

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        public List<NearProfiles> AllProfiles
        {
            get
            {
                return this.allprofiles;
            }
            set
            {
                this.allprofiles = value;
                RaisePropertyChanged(() => AllProfiles);
            }
        }

        public void ShowProfile()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("profile_image", profile_image);

            param.Add("profile_id", profile_id);

            param.Add("initial", "1");


            ShowViewModel<HomeViewModel>(param);
        }


        public async void GetProfiles()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.GetNearProfiles(latitude,longitude,page,items);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("Near Profiles Count " + result.data.users_near_me.Count);

                    total_pages = result.data.total_pages;

                    if (allprofiles.Count.Equals(0))
                    {

                        foreach (NearProfiles profiles in result.data.users_near_me)
                        {
                            allprofiles.Add(profiles);

                        } 
                        
                    }

                    RaisePropertyChanged(() => AllProfiles);


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

            param.Add("Preview", "Home");

            ShowViewModel<SettingsPageViewModel>(param);
        }

        public void showSettings()
        {

            ShowViewModel<NavigateSettingsViewModel>();
        }

        
    }
}
