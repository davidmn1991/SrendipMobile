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
    public class GenderFilterViewModel : SrendipViewModel<GenderFilterViewModel>
    {
        public GenderFilterViewModel()
        {
        }

        public void ShowSettings()
        {

            ShowViewModel<SettingsPageViewModel>();
        }
    }
}
