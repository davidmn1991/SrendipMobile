using System;
namespace PorpoiseMobileApp.Client
{
    public class LoginResponse<T>
    {
        private bool _successful;

        public string Result
        {
            get; set;
        }

        public string data
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


        public LoginResponse()
        {

        }
    }
}
