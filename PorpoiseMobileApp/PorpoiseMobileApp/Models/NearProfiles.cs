using System;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PorpoiseMobileApp.Models
{

    public class AllProfilesNear
    {

        public AllProfilesNear()
        {
        }

        [JsonProperty("users_near_me")]
        public List<NearProfiles> users_near_me { get; set; }

        [JsonProperty("total_pages")]
        public int total_pages
        {
            get; set;
        }


    }
    public class NearProfiles
    {
        public NearProfiles()
        {
        }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("image_url")]
        public string image_url { get; set; }
    }
}
