
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
    public class IntroductionViewModel : SrendipViewModel<IntroductionViewModel>
    
    {
        public IntroductionViewModel()
        {
        }

        public void ShowLogin()
        {

            ShowViewModel<LoginFirstViewModel>();
        }

        public void ShowSignup()
        {

            ShowViewModel<Signup1ViewModel>();
        }
    }
}
