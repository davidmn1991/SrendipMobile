using System;
namespace PorpoiseMobileApp.Client
{
    public class RegisterOauthUserRequestModel
    {
        public RegisterOauthUserRequestModel()
        {
        }

        public string email { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string gender { get; set; }

        public string birth_date { get; set; }

        public UserLoginProviderRequestModel user_login_provider { get; set; }
    }
}
