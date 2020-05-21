using System;
namespace PorpoiseMobileApp.Client
{
    public class AddBubbleRequestModel
    {
        public AddBubbleRequestModel()
        {
        }

        public int bubble_id { get; set; }

        public string description { get; set; }

        public string type { get; set; }
    }
}
