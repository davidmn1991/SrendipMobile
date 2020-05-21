using System;
namespace PorpoiseMobileApp.Client
{
    public class EmailVerificationResponseModel<T>
    {
        public EmailVerificationResponseModel()
        {
        }


        public string Result
        {
            get; set;
        }

        public bool data
        {
            get; set;
        }

        public string message
        {
            get; set;
        }

        public int code
        {
            get; set;
        }

        public T Payload { get; set; }


    }
}
