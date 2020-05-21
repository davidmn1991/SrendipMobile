using System;
namespace PorpoiseMobileApp.Client
{
    public class RegisterUserRequestModel
    {


        public string email { get; set; }

        public string password { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string bio { get; set; }

        public string image_url { get; set; }

        public string gender { get; set; }

        public string birth_date { get; set; }


    }
}
