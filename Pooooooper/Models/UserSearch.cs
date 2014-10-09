using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterMVC.Models
{
    public class UserSearch
    {
        public string id { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int Purpose { get; set; }
        public int Sublet { get; set; }
        public int FromPrice { get; set; }
        public int ToPrice { get; set; }

        public int City { get; set; }
        public int[] SubAreas { get; set; }
        public int[] Locations { get; set; }
        public int FromSize { get; set; }
        public int ToSize { get; set; }
        public double FromRoomsNumber { get; set; }
        public double ToRoomsNumber { get; set; }
        public int FromTotalRoommatesNumber { get; set; }
        public int ToTotalRoommatesNumber { get; set; }
        public int Parking { get; set; }
        public int Renovated { get; set; }
        public int Pets { get; set; }
        public int Smoke { get; set; }
        public int Balcony { get; set; }
        public int Furnitured { get; set; }
        public int FromAgency { get; set; }

        public List<int> SubAreasIds { get; set; }
        public List<int> LocationsIds { get; set; }
        public List<string> AddressConclusionIds { get; set; }

        public UserSearch()
        {
            AddressConclusionIds = new List<string>();
            SubAreasIds = new List<int>();
            LocationsIds = new List<int>();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            UserSearch other = (UserSearch)obj;
            return this.id.Equals(other.id);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}