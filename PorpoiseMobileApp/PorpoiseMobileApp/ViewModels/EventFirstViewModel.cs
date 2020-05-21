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
    public class EventFirstViewModel : SrendipViewModel<EventFirstViewModel>
    {
        public EventFirstViewModel()
        {
        }

        public string name;

        public string last_name;

        public string eventName;

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {


                if (parameters.Data.ContainsKey("name"))
                {
                    name = parameters.Data["name"];

                    Debug.WriteLine("name " + name);
                }

                if (parameters.Data.ContainsKey("last_name"))
                {
                    last_name = parameters.Data["last_name"];

                    Debug.WriteLine("last name " + last_name);
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());

            }

        }

        public void BackSignup1()
        {

            ShowViewModel<Signup1ViewModel>();
        }

       

        public void ShowSignup2()
        {

            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("ViewController", "Event");

            param.Add("name", name);

            param.Add("last_name", last_name);

            param.Add("event_name", eventName);

            ShowViewModel<Signup2ViewModel>(param);
        }
    }
}
