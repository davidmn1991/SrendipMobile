using System;
namespace PorpoiseMobileApp.Client
{
    public class EmailVerificationRequestModel
    {
        public EmailVerificationRequestModel()
        {
        }

        public string email { get; set; }

        public string provider_id { get; set; }
    }
}
