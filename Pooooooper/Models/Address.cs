using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterMVC.Models
{
    public class AddressCriteria
    {
        public string City { get; set; }
        public string[] Areas { get; set; }
        public string[] SubAreas { get; set; }
        public string[] Locations { get; set; }
    }
}