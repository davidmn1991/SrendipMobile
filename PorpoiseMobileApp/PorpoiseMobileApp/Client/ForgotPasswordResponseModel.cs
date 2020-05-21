using System;
namespace PorpoiseMobileApp.Client
{
    
    public class ForgotPasswordResponseModel : ResponseModel<SignupPayload>
    {
        public ForgotPasswordResponseModel()
        {
        }

        public ForgotPasswordResponseModel(bool successful) : base(successful)
        {
        }
        public ForgotPasswordResponseModel(bool success, string message) : base(success)
        {

            this.Message = message;
        }


    }

   
}
