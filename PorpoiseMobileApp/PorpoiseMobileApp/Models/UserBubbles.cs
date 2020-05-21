using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PorpoiseMobileApp.Models
{
    public class UserBubbles
    {
        public UserBubbles()
        {
        }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("icon")]
        public string icon { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("bubble_details")]
        public List<BubbleDetails> bubble_details { get; set; }


    }

    public class BubbleDetails
    {


        [JsonProperty("detail")]
        public string detail { get; set; }

        [JsonProperty("file_url")]
        public string file_url { get; set; }

    }

}
