using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterMVC.Models
{
    public class AddressConclusion
    {
        public List<int> SubAreasIds { get; set; }
        public List<int> LocationsIds { get; set; }

        public List<String> SubAreas { get; set; }
        public List<String> Locations { get; set; }

        public AddressConclusion()
        {
            Locations = new List<string>();
            SubAreasIds = new List<int>();
            SubAreas = new List<string>();
            LocationsIds = new List<int>();
        }
    }
}