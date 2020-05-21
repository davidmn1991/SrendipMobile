﻿using System;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PorpoiseMobileApp.Models
{
    public class Event
    {
        public string name { get; set; }

        public string description { get; set; }

        public double radius { get; set; }

        public int min_age { get; set; }

        public int max_age { get; set; }

        public int duration_days { get; set; }

        public int total_cost { get; set; }

        public List<string> Genders { get; set; }


    }
}
