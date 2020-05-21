using System;
namespace PorpoiseMobileApp.Client
{
    public class LoginOauthRequestModel
    {
        public LoginOauthRequestModel()
        {
        }

        public string email { get; set; }

        public string provider { get; set; }
    }
}
