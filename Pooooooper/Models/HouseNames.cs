using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HunterMVC.Models.Enums;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace HunterMVC.Models
{
    public class HouseNames : PostImplementation, IComparable
    {
        public int id { get; set; }
        public int RoommatesNumber { get; set; }
        public double RoomsNumber { get; set; }
        public String City { get; set; }
        public String Parking { get; set; }
        public String Renovated { get; set; }
        public String Purpose { get; set; }
        public String Side { get; set; }
        public String Pets { get; set; }
        public String Smoke { get; set; }
        public String Balcony { get; set; }
        public String Elevator { get; set; }
        public String Furnitured { get; set; }
        public String Sublet { get; set; }
        public String FromAgency { get; set; }
        public int Floor { get; set; }
        public String Type { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }

        public DateTime DateCreated { get; set; }

        public String UserSearchId { get; set; }

        [ScriptIgnore]
        public int PostId { get; set; }
        [ScriptIgnore]
        

        public List<AddressConclusion> Addresses { get; set; }
        public List<string> AddressesIds { get; set; }

        public HouseNames(String message)
            : base(message)
        {
            AddressesIds = new List<string>();
            Addresses = new List<AddressConclusion>();

        }

        public HouseNames()
            : base()
        {
            AddressesIds = new List<string>();
            Addresses = new List<AddressConclusion>();
        }

        /**
         * 
         * , int id, int RoommatesNumber, double RoomsNumber, int cityId, int parkingId, int renovatedId, int purposeId, 
            int sideId, int streetId, int postId, int floor, int typeId, int price, int size, int locationId, int houseNumber, 
            String comment, int petsId, int smokeId, int balconyId, int elevatorId, int furnituredId, int subletId, int fromAgencyId,
            string streetsComment, String streetsFound, string locationsFound, string neighborhoodsFound, string subAreasFound, 
            string areasFound
         * /
         * */
        public HouseNames(HouseNames otherHouse)
        {
            this.id = otherHouse.id;
            this.RoommatesNumber = otherHouse.RoommatesNumber;
            this.RoomsNumber = otherHouse.RoomsNumber;
            this.City = otherHouse.City;
            this.Parking = otherHouse.Parking;
            this.Renovated = otherHouse.Renovated;
            this.Purpose = otherHouse.Purpose;
            this.Side = otherHouse.Side;
            this.PostId = otherHouse.PostId;
            this.Floor = otherHouse.Floor;
            this.Type = otherHouse.Type;
            this.Price = otherHouse.Price;
            this.Size = otherHouse.Size;
            this.Pets = otherHouse.Pets;
            this.Smoke = otherHouse.Smoke;
            this.Balcony = otherHouse.Balcony;
            this.Elevator = otherHouse.Elevator;
            this.Furnitured = otherHouse.Furnitured;
            this.Sublet = otherHouse.Sublet;
            this.FromAgency = otherHouse.FromAgency;

            this.Addresses = otherHouse.Addresses;
            this.AddressesIds = otherHouse.AddressesIds;
        }

        public int CompareTo(object obj)
        {
            HouseNames otherHouse = (HouseNames)obj;
            return (this.DateCreated.CompareTo(otherHouse.DateCreated));
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            HouseNames otherHouse = (HouseNames)obj;
            return this.PostId.Equals(otherHouse.PostId);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }
    }
}