
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
    public class Signup3ViewModel : SrendipViewModel<Signup3ViewModel>
    {
        public string email;

        public string password;

        public string name;

        public string last_name;

        public string gender;

        public string date_of_birth;

        public Signup3ViewModel()
        {
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {

                if (parameters.Data.ContainsKey("email"))
                {
                    email = parameters.Data["email"];

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

                if (parameters.Data.ContainsKey("password"))
                {
                    password = parameters.Data["password"];

                    Debug.WriteLine("REDIRECTING TO CONTROLLER ");
                }

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

        public void BackSignup2()
        {

            ShowViewModel<Signup2ViewModel>();
        }

        public void ShowSignup4()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("name", name);

            param.Add("last_name", last_name);

            param.Add("email", email);

            param.Add("password", password);

            param.Add("gender", gender);

            param.Add("date_of_birth", date_of_birth);

            ShowViewModel<Signup4ViewModel>(param);
        }
    }
}
