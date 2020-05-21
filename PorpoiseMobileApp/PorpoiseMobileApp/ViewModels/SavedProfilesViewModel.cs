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
    public class SavedProfilesViewModel : SrendipViewModel<SavedProfilesViewModel>
    {
        public SavedProfilesViewModel()
        {
        }

        ProfileDetails profile = new ProfileDetails();

        public List<ProfileDetails> SavedProfiles = new List<ProfileDetails>();

        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        public string profile_id;

        public SavedProfilesViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this._imageService = Mvx.Resolve<IImageService>();

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        public  List<ProfileDetails> Saved_Profiles
        {
            get
            {
                return this.SavedProfiles;
            }
            set
            {
                this.SavedProfiles = value;
                RaisePropertyChanged(() => Saved_Profiles);
            }
        }

        public async void SaveProfile()
        {

            try
            {
                    var result = await client.SavedProfiles();
                    Debug.WriteLine("RESULT");
                    if (result != null && result.code.Equals(200))
                    {
                        Debug.WriteLine(" " + result.message);

                    foreach(ProfileDetails profiles in result.data)
                    {

                        SavedProfiles.Add(profiles);
                    }

                    RaisePropertyChanged(() => Saved_Profiles);

                    Debug.WriteLine(SavedProfiles.Count);

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

        public void ShowSettings()
        {

            ShowViewModel<SettingsPageViewModel>();
        }

        public void ShowSingleProfile()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("profile_id", profile_id);

            ShowViewModel<SingleSavedViewModel>(param);
        }
    }
}
