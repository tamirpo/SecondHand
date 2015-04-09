using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterMVC.Models
{
    public class AddressCriteria
    {
        public int City { get; set; }
        public int[] Areas { get; set; }
        public int[] Locations { get; set; }

        //public List<string> AddressConclusionIds { get; set; }

        public AddressCriteria()
        {
            //AddressConclusionIds = new List<string>();
        }
    }
}