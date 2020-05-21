
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
    public class SettingsPageViewModel : SrendipViewModel<SettingsPageViewModel>
    {
        public SettingsPageViewModel()
        {
        }

        private readonly IPorpoiseWebApiClient client;

        private readonly ISettings settings;

        public static string preview = "Home";

        public SettingsPageViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this.settings = settings;
            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

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

        public void ShowGenderFilter()
        {

            ShowViewModel<GenderFilterViewModel>();
        }

        public void ShowHome()
        {

            var param = new System.Collections.Generic.Dictionary<string, string>();

            if (preview.Equals("FireMode"))
            {

                param.Add("initial", "2");

            }
            else if (preview.Equals("Myprofile"))
            {

                param.Add("initial", "0");

            }
            else if (preview.Equals("Home"))
            {
                param.Add("initial", "1");
            }



                ShowViewModel<HomeViewModel>(param);
        }

        public void ShowSavedProfiles()
        {

            ShowViewModel<SavedProfilesViewModel>();
        }

        public void ShowProfile()
        {

            ShowViewModel<ProfileViewModel>();
        }

        public void Showlogin()
        {
            settings.Remove(AccountInfo.TOKENKEY);

            settings.Remove(AccountInfo.user_email);

            ShowViewModel<LoginFirstViewModel>();
        }
    }
}
