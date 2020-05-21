using Newtonsoft.Json;
using PorpoiseMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorpoiseMobileApp.Client
{
    public class SavedProfilesResponseModel<T>
    {
        public SavedProfilesResponseModel()
        {
        }

        public string Result
        {
            get; set;
        }

        public List <ProfileDetails> data
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
