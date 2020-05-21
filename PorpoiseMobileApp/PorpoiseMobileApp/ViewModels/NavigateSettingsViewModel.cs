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
    public class NavigateSettingsViewModel : SrendipViewModel<NavigateSettingsViewModel>
    {
        public NavigateSettingsViewModel()
        {
        }

        public void ShowAllProfiles()
        {

            ShowViewModel<AllProfilesViewModel>();
        }
    }
}
