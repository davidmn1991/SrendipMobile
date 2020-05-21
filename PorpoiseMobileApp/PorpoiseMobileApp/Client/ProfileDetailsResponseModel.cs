using Newtonsoft.Json;
using PorpoiseMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PorpoiseMobileApp.Client
{
    public class ProfileDetailsResponseModel <T>
    {
        public ProfileDetailsResponseModel()
        {
        }

        public string Result
        {
            get; set;
        }

        public ProfileDetails data
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

    public class ProfileDetails
    {
        [JsonProperty("first_name")]
        public string first_name { get; set; }

        [JsonProperty("middle_name")]
        public string middle_name { get; set; }

        [JsonProperty("last_name")]
        public string last_name { get; set; }

        [JsonProperty("image_url")]
        public string image_url { get; set; }

        [JsonProperty("age")]
        public int age { get; set; }

        [JsonProperty("user_id")]
        public string user_id { get; set; }

        [JsonProperty("user_bubble")]
        public List<UserBubbles> user_bubble { get; set; }


    }

        
}
