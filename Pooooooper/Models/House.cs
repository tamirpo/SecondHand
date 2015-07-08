using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HunterMVC.Models.Enums;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace HunterMVC.Models
{
    public class House : PostImplementation , IComparable
    {
        public int id { get; set; }
        public int RoommatesNumber { get; set; }
        public double RoomsNumber { get; set; }
        public int CityId { get; set; }
        public int ParkingId { get; set; }
        public int RenovatedId { get; set; }
        public int PurposeId { get; set; }
        public int SideId { get; set; }
        public int PetsId { get; set; }
        public int SmokeId { get; set; }
        public int BalconyId { get; set; }
        public int ElevatorId { get; set; }
        public int FurnituredId { get; set; }
        public int SubletId { get; set; }
        public int FromAgencyId { get; set; }
        public int Floor { get; set; }
        public int TypeId { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }
        public int HouseNumber { get; set; }
        public String PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }

        public List<String> Areas { get; set; }

        public String UserSearchId { get; set; }

        [ScriptIgnore]
        public int PostId { get; set; }
        [ScriptIgnore]
        public string Comment { get; set; }
        [ScriptIgnore]
        public string StreetsComment { get; set; }
        [ScriptIgnore]
        public string StreetsFound { get; set; }
        public string LocationsFound { get; set; }
        [ScriptIgnore] 
        public string NeighborhoodsFound { get; set; }
        [ScriptIgnore] 
        public string SubAreasFound { get; set; }
        public string AreasFound { get; set; }

        public List<int> AreaIds { get; set; }
        public List<int> LocationIds { get; set; }

        //public List<AddressConclusion> Addresses { get; set; }
        //public List<string> AddressesIds { get; set; }

        public House(String message) : base(message)
        {
            //AddressesIds = new List<string>();
            //Addresses = new List<AddressConclusion>();
            Areas = new List<String>();
        }

        public House() : base() 
        {
            //AddressesIds = new List<string>();
            //Addresses = new List<AddressConclusion>();
            Areas = new List<String>();
            AreaIds = new List<int>();
            LocationIds = new List<int>();
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
        public House(House otherHouse)        
        {
            this.id = otherHouse.id;
            this.RoommatesNumber = otherHouse.RoommatesNumber;
            this.RoomsNumber = otherHouse.RoomsNumber;
            this.CityId = otherHouse.CityId;
            this.ParkingId = otherHouse.ParkingId;
            this.RenovatedId = otherHouse.RenovatedId;
            this.PurposeId = otherHouse.PurposeId;
            this.SideId = otherHouse.SideId;
            this.PostId = otherHouse.PostId;
            this.Floor = otherHouse.Floor;
            this.TypeId = otherHouse.TypeId;
            this.Price = otherHouse.Price;
            this.Size = otherHouse.Size;
            this.HouseNumber = otherHouse.HouseNumber;
            this.Comment = otherHouse.Comment;
            this.PetsId = otherHouse.PetsId;
            this.SmokeId = otherHouse.SmokeId;
            this.BalconyId = otherHouse.BalconyId;
            this.ElevatorId = otherHouse.ElevatorId;
            this.FurnituredId = otherHouse.FurnituredId;
            this.SubletId = otherHouse.SubletId;
            this.FromAgencyId = otherHouse.FromAgencyId;
            this.StreetsComment = otherHouse.StreetsComment;
            this.StreetsFound = otherHouse.StreetsFound;
            this.LocationsFound = otherHouse.LocationsFound;
            this.NeighborhoodsFound = otherHouse.NeighborhoodsFound;
            this.SubAreasFound = otherHouse.SubAreasFound;
            this.AreasFound = otherHouse.AreasFound;

            this.PhoneNumber = otherHouse.PhoneNumber;

            //this.Addresses = otherHouse.Addresses;
            //this.AddressesIds = otherHouse.AddressesIds;
        }

        public int CompareTo(object obj)
        {
            House otherHouse = (House)obj;
            return (this.DateCreated.CompareTo(otherHouse.DateCreated));
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            House otherHouse = (House)obj;
            return this.PostId.Equals(otherHouse.PostId);   
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }

        public string SenderPictureURL { get; set; }

        public string FacebookPostURL { get; set; }
    }
}