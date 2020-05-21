using Newtonsoft.Json;
using PorpoiseMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PorpoiseMobileApp.Client
{
    public class BubblesListResponseModel<T>
    {
        public BubblesListResponseModel()
        {
        }

        public string Result
        {
            get; set;
        }

        public List<BubbleList> data
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
    public class BubbleList
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("position")]
        public int position { get; set; }


    }
}
