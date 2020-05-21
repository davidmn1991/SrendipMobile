using System;
namespace PorpoiseMobileApp.Client
{
    public class RegisterUserResponseModel : ResponseModel<RegisterUser>
    {
        public RegisterUserResponseModel()
        {
        }

       
        public RegisterUserResponseModel(bool success, string message) : base(success)
        {

            this.Message = message;
        }


    }

    public class RegisterUser
    {


    }
}

