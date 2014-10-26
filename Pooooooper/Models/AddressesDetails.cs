using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterMVC.Models
{
    public class AddressesDetails
    {
        public Dictionary<int, String> AreaIdVsName { get; set; }
        public Dictionary<int, String> SubAreaIdVsName { get; set; }
        public Dictionary<int, String> LocationIdVsName { get; set; }
        public Dictionary<int, List<int>> AreasVsSubAreas { get; set; }
        public Dictionary<int, List<int>> AreasVsLocations { get; set; }

    }
}