using System;
using System.Collections.Generic;
using PorpoiseMobileApp.Models;
namespace PorpoiseMobileApp.Client
{
    public class AddBubbleFileRequestModel
    {
        public AddBubbleFileRequestModel()
        {
        }


        public int bubble_id { get; set; }

        public string type { get; set; }

        public List<Bubbledetails> bubble_details { get; set; }





    }

}
