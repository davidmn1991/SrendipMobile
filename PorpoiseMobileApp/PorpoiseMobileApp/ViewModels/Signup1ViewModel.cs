
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
    public class Signup1ViewModel : SrendipViewModel<Signup1ViewModel>
    {
    
        public Signup1ViewModel()
        {
        }

        public string name;

        public string last_name;

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {

                if (parameters.Data.ContainsKey("name"))
                {
                    name = parameters.Data["name"];

                    Debug.WriteLine("name "+name);
                }

                if (parameters.Data.ContainsKey("last_name"))
                {
                    last_name = parameters.Data["last_name"];

                    Debug.WriteLine("last name "+last_name);
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());

            }

        }

        public void BackIntroduction()
        {

            ShowViewModel<IntroductionViewModel>();
        }

        public void ShowSignup2()
        {

            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("name", name);

            param.Add("last_name", last_name);

            ShowViewModel<Signup2ViewModel>(param);
        }

        public void ShowEventFirst()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("name", name);

            param.Add("last_name", last_name);

            //ShowViewModel<EventLastViewModel>(param);

            ShowViewModel<EventFirstViewModel>(param);


        }
    }
}
