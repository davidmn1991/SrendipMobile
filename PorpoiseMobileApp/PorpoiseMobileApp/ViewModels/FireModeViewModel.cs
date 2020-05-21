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
    public class FireModeViewModel : SrendipViewModel<FireModeViewModel>
    {
        public FireModeViewModel()
        {
        }

        public void ShowSettings()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("Preview", "FireMode");

            ShowViewModel<SettingsPageViewModel>(param);
        }
    }
}
