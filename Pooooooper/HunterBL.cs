using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HunterMVC.Models;

namespace HunterMVC
{
    public class HunterBL
    {
        HunterDAL dal;
        public HunterBL()
        {
            dal = new HunterDAL();
        }

        public Boolean SaveExpressions(Dictionary<String, String> expressions)
        {
            return dal.SaveExpressions(expressions);
        }

        public PostImplementation GetNewPost()
        {
            return null;
        }

        public House GetNewHouse(int cityId)
        {

            House returnedHouse = dal.GetNewHouse(cityId);
            if (returnedHouse != null)
            {
                if (returnedHouse.TypeId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.TypeId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "סוג בית");
                }
                if (returnedHouse.CityId > 0)
                {
                    string city = dal.GetCityNameFromId(returnedHouse.CityId);
                    returnedHouse.ExpressionsVsTypes.Add(city, "עיר");
                }
                if (returnedHouse.RenovatedId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.RenovatedId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "משופצת");
                }
                if (returnedHouse.Floor > 0)
                {
                    returnedHouse.ExpressionsVsTypes.Add(" קומה " + returnedHouse.Floor, "קומה");
                }
                if (returnedHouse.ParkingId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.ParkingId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "חניה");
                }
                if (returnedHouse.PurposeId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.PurposeId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "סוג חיפוש");
                }
                if (returnedHouse.RoommatesNumber > 0)
                {
                    returnedHouse.ExpressionsVsTypes.Add(" שותפים " + returnedHouse.RoommatesNumber.ToString() , "מספר שותפים סהכ");
                }
                if (returnedHouse.RoomsNumber > 0)
                {
                    returnedHouse.ExpressionsVsTypes.Add(" חדרים " + returnedHouse.RoomsNumber.ToString() , "מספר חדרים");
                }
                if (returnedHouse.SideId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.SideId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "צד");
                }
                if (returnedHouse.Price > 0)
                {
                    string price = returnedHouse.Price.ToString();
                    returnedHouse.ExpressionsVsTypes.Add(price, "מחיר");
                }
                if (returnedHouse.Size > 0)
                {
                    string size = returnedHouse.Size.ToString();
                    returnedHouse.ExpressionsVsTypes.Add(size, "גודל");
                }
                if (returnedHouse.PetsId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.PetsId);;
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "בעלי חיים");
                }
                if (returnedHouse.SmokeId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.SmokeId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "עישון");
                }
                if (returnedHouse.FurnituredId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.FurnituredId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מרוהטת");
                }
                if (!String.IsNullOrEmpty(returnedHouse.PhoneNumber))
                {
                    returnedHouse.ExpressionsVsTypes.Add(returnedHouse.PhoneNumber, "מס טלפון");
                }
                if (returnedHouse.BalconyId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.BalconyId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מרפסת");
                }
                if (returnedHouse.SubletId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.SubletId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "סאבלט");
                }
                if (returnedHouse.FromAgencyId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.FromAgencyId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מתיווך");
                }
                if (returnedHouse.ElevatorId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.ElevatorId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מעלית");
                }

                List<String> areas = dal.GetHouseAddressesIds(returnedHouse.PostId, 1, returnedHouse.CityId);
                if (areas.Count > 0)
                    returnedHouse.AreasFound = string.Join(", ", areas);

                List<String> locations = dal.GetHouseAddressesIds(returnedHouse.PostId, 2, returnedHouse.CityId);
                if (locations.Count > 0)
                    returnedHouse.LocationsFound = string.Join(", ", locations);
                /*foreach (AddressConclusion currentAddress in returnedHouse.Addresses)
                {
                    foreach (int currentLocationId in currentAddress.LocationsIds)
                    {
                        currentAddress.Locations.Add(dal.GetLocationNameFromId(currentLocationId, returnedHouse.CityId)); 
                    }
                    foreach (int currentSubAreaId in currentAddress.SubAreasIds)
                    {
                        currentAddress.SubAreas.Add(dal.GetSubAreaNameFromId(currentSubAreaId, returnedHouse.CityId));
                    }
                }*/


                /*

                string locationsString = String.Empty;
                List<House> housesWithLocation = returnedHouses.FindAll(o => o.LocationId > 0);
                foreach (House currentHouse in housesWithLocation)
                {
                    string location = dal.GetLocationNameFromId(currentHouse.LocationId, cityId);

                    locationsString += location + ", ";
                }
                returnedHouse.ExpressionsVsTypes.Add(locationsString, "לוקיישן");



                string neighborhoodsString = String.Empty;
                string subAreasString = String.Empty;
                List<string> subAreas = new List<string>();
                List<House> housesWithNeighborhood = returnedHouses.FindAll(o => o.NeighborhoodId > 0);
                foreach (House currentHouse in housesWithNeighborhood)
                {
                    string neighborhood = dal.GetNeighborhoodFromId(currentHouse.NeighborhoodId, cityId);
                    neighborhoodsString += neighborhood + ", ";

                    string subArea = dal.GetSubAreaNameFromNeighborhoodId(currentHouse.NeighborhoodId, cityId);
                    if (!subAreas.Contains(subArea))
                    {
                        subAreas.Add(subArea);
                        subAreasString += subArea + ", ";
                    }
                }

                returnedHouse.ExpressionsVsTypes.Add(subAreasString + " ", "תת איזור");
                 * 
                 * */
            }
            
            return returnedHouse;
        }

        private string GetLocationNameFromId(int locationId, int cityId)
        {
            string answer = "";

            answer = dal.GetLocationNameFromId(locationId, cityId);

            return answer;
        }

        private string GetLocationAliasNameFromId(int locationId, int cityId)
        {
            string answer = "";

            answer = dal.GetLocationAliasNameFromId(locationId, cityId);

            return answer;
        }

        internal string GetCityFromId(int cityId)
        {
            string answer = "";

            answer = dal.GetCityFromId(cityId);

            return answer;
        }

        internal string GetRootExpressionNameFromRootExpressionId(double rootExpressionId)
        {
            string answer = "";

            answer = dal.GetRootExpressionNameFromRootExpressionId(rootExpressionId);

            return answer;
        }

        internal string GetStreetNameFromId(int streetId, int cityId)
        {
            string answer = "";

            answer = dal.GetStreetNameFromId(streetId, cityId);

            return answer;
        }

        internal bool UpdateHouseFields(Dictionary<int, int> rootExpressionIdsVsCategoryIds, int houseId)
        {
            return dal.UpdateHouseFields(rootExpressionIdsVsCategoryIds, houseId);
        }

        internal Dictionary<string, string> GetRootExpressionsIdsVsCategoriesIds(int cityId)
        {
            return dal.GetRootExpressionsIdsVsCategoriesIds(cityId);
        }

        internal Dictionary<string, List<string>> GetCategoriesIdsVsAddressesIds(int cityId)
        {
            return dal.GetCategoriesIdsVsAddressesIds(cityId);
        }

        internal Dictionary<string, string> GetAllCategories()
        {
            return dal.GetAllCategories();
        }

        internal Dictionary<string, string> GetAllRootExpressions()
        {
            return dal.GetAllRootExpressions();
        }

        internal Dictionary<int, int> GetExpressionsIds(Dictionary<string, string>.KeyCollection keyCollection)
        {
            return dal.GetExpressionsIds(keyCollection);
        }

        internal bool VerifyHouse(int houseId)
        {
            return dal.VerifyHouse(houseId);
        }

        internal void AddBug(int houseId, string comment)
        {
            dal.AddBug(houseId, comment);
        }

        internal string LoginUser(string imei)
        {
            return dal.LoginUser(imei);
        }

        internal bool VerifyUserLogin(string token, string imei)
        {
            return dal.VerifyUserLogin(token, imei);
        }

        internal List<House> GetApartments(UserSearch searchCriteria, DateTime startDate)
        {
            return dal.GetApartments(searchCriteria, startDate);
        }

        internal List<House> GetApartments(UserSearch searchCriteria, DateTime startDate, DateTime endDate)
        {
            return dal.GetApartments(searchCriteria, startDate, endDate);
        }

        internal List<HouseNames> GetApartmentsExact(UserSearch searchCriteria, DateTime startDate, DateTime endDate)
        {
            return dal.GetApartmentsExact(searchCriteria, startDate, endDate);
        }

        internal string GetApartments(string searchCriteria)
        {
            throw new NotImplementedException();
        }

        internal House GetNewHouseFromHouseId(string houseId)
        {
            House returnedHouse = dal.GetNewHouseById(houseId);
            if (returnedHouse != null)
            {
                if (returnedHouse.TypeId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.TypeId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "סוג בית");
                }
                if (returnedHouse.CityId > 0)
                {
                    string city = dal.GetCityNameFromId(returnedHouse.CityId);
                    returnedHouse.ExpressionsVsTypes.Add(city, "עיר");
                }
                if (returnedHouse.RenovatedId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.RenovatedId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "משופצת");
                }
                if (returnedHouse.Floor > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.Floor);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "קומה");
                }
                if (returnedHouse.ParkingId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.ParkingId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "חניה");
                }
                if (returnedHouse.PurposeId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.PurposeId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "סוג חיפוש");
                }
                if (returnedHouse.RoommatesNumber > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.RoommatesNumber);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מספר שותפים סהכ");
                }
                if (returnedHouse.RoomsNumber > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.RoomsNumber);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מספר חדרים");
                }
                if (returnedHouse.SideId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.SideId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "צד");
                }
                if (returnedHouse.Price > 0)
                {
                    string price = returnedHouse.Price.ToString();
                    returnedHouse.ExpressionsVsTypes.Add(price, "מחיר");
                }
                if (returnedHouse.Size > 0)
                {
                    string size = returnedHouse.Size.ToString();
                    returnedHouse.ExpressionsVsTypes.Add(size, "גודל");
                }
                if (returnedHouse.PetsId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.PetsId); ;
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "בעלי חיים");
                }
                if (returnedHouse.SmokeId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.SmokeId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "עישון");
                }
                if (returnedHouse.FurnituredId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.FurnituredId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מרוהטת");
                }
                if (!String.IsNullOrEmpty(returnedHouse.PhoneNumber))
                {
                    returnedHouse.ExpressionsVsTypes.Add(returnedHouse.PhoneNumber, "מס טלפון");
                }
                if (returnedHouse.BalconyId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.BalconyId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מרפסת");
                }
                if (returnedHouse.SubletId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.SubletId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "סאבלט");
                }
                if (returnedHouse.FromAgencyId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.FromAgencyId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מתיווך");
                }
                if (returnedHouse.ElevatorId > 0)
                {
                    string rootExpression = GetRootExpressionNameFromRootExpressionId(returnedHouse.ElevatorId);
                    returnedHouse.ExpressionsVsTypes.Add(rootExpression, "מעלית");
                }

                /*returnedHouse.Addresses = dal.GetHouseAddressesIds(returnedHouse.AddressesIds);
                foreach (AddressConclusion currentAddress in returnedHouse.Addresses)
                {
                    foreach (int currentLocationId in currentAddress.LocationsIds)
                    {
                        currentAddress.Locations.Add(dal.GetLocationNameFromId(currentLocationId, returnedHouse.CityId));
                    }
                    foreach (int currentSubAreaId in currentAddress.SubAreasIds)
                    {
                        currentAddress.SubAreas.Add(dal.GetSubAreaNameFromId(currentSubAreaId, returnedHouse.CityId));
                    }
                }
                */

                /*

                string locationsString = String.Empty;
                List<House> housesWithLocation = returnedHouses.FindAll(o => o.LocationId > 0);
                foreach (House currentHouse in housesWithLocation)
                {
                    string location = dal.GetLocationNameFromId(currentHouse.LocationId, cityId);

                    locationsString += location + ", ";
                }
                returnedHouse.ExpressionsVsTypes.Add(locationsString, "לוקיישן");



                string neighborhoodsString = String.Empty;
                string subAreasString = String.Empty;
                List<string> subAreas = new List<string>();
                List<House> housesWithNeighborhood = returnedHouses.FindAll(o => o.NeighborhoodId > 0);
                foreach (House currentHouse in housesWithNeighborhood)
                {
                    string neighborhood = dal.GetNeighborhoodFromId(currentHouse.NeighborhoodId, cityId);
                    neighborhoodsString += neighborhood + ", ";

                    string subArea = dal.GetSubAreaNameFromNeighborhoodId(currentHouse.NeighborhoodId, cityId);
                    if (!subAreas.Contains(subArea))
                    {
                        subAreas.Add(subArea);
                        subAreasString += subArea + ", ";
                    }
                }

                returnedHouse.ExpressionsVsTypes.Add(subAreasString + " ", "תת איזור");
                 * 
                 * */
            }

            return returnedHouse;
        }

        internal String SaveUserSearch(UserSearch searchCriteria)
        {
            String result = String.Empty;
            /*foreach (AddressCriteria currentAddress in searchCriteria.Addresses)
            {
                if (currentAddress.City == 1)
                {
                    if (currentAddress.Areas != null && currentAddress.Areas.Length > 0)
                    {
                        currentAddress.AddressConclusionIds.AddRange(dal.GetAddressConclusionsByObjectIds(currentAddress.Areas, 1));
                    }
                    if (currentAddress.Locations != null && currentAddress.Locations.Length > 0)
                    {
                        currentAddress.AddressConclusionIds.AddRange(dal.GetAddressConclusionsByObjectIds(currentAddress.Locations, 2));
                    }
                }
                else
                {
                    if (currentAddress.Locations != null && currentAddress.Locations.Length > 0)
                    {
                        currentAddress.AddressConclusionIds.AddRange(dal.GetAddressConclusionsByObjectIds(currentAddress.Locations, 2));
                    }
                    else
                    {
                        currentAddress.AddressConclusionIds.Add(currentAddress.City.ToString());
                    }
                }
            }*/

            String existingSearchId = dal.GetSearchId(searchCriteria);
            result = dal.SaveUserSearch(searchCriteria, existingSearchId);

            return result;
        }

        internal UserSearch GetUserSearchById(string searchId)
        {
            return dal.GetUserSearchById(searchId);
        }

        internal Dictionary<string, string> GetNeighborhoods(int cityId)
        {
            return dal.GetNeighborhoods(cityId);
        }

        internal Dictionary<string, string> GetLocations(int cityId)
        {
            return dal.GetLocations(cityId);
        }

        internal string SaveHouseAddress(int houseId, List<int> selectedNeighborhoods, List<int> selectedLocations)
        {
            List<House> houses = new List<House>();
            House currentHouse = GetNewHouseFromHouseId(houseId.ToString());

            /*foreach (int currentNeighborhoodId in selectedNeighborhoods)
            {
                House dupHouse = new House(currentHouse);
                dupHouse.NeighborhoodId = currentNeighborhoodId;
                dupHouse.HasMoreThanOneSubArea = 0;
                houses.Add(dupHouse);
            }

            foreach (int currentLocationId in selectedLocations)
            {
                House dupHouse = new House(currentHouse);
                dupHouse.LocationId = currentLocationId;
                dupHouse.HasMoreThanOneSubArea = 0;
                houses.Add(dupHouse);
            }
            */
            foreach (House currentNewHouse in houses)
	        {
                dal.SaveHouse(currentNewHouse);
	        }

            dal.DeleteHouse(currentHouse.id);

            return "Success"; 
        }

        internal List<BusinessReportResult> GetAllFromBusinessHouses(DateTime startDate, DateTime endDate)
        {
            return dal.GetAllFromBusinessHouses(startDate, endDate);
        }

        internal List<string> GetAddressConclusionIdsFromUserSearches(List<string> searches)
        {
            return dal.GetAddressConclusionIdsFromUserSearches(searches);
        }

        internal void AttachPostsImages(ref List<House> resultFiltered)
        {
            dal.AttachPostsImages(ref resultFiltered);
        }

        internal Dictionary<string, string> GetAreasByCityId(int cityId)
        {
            return dal.GetAreasByCityId(cityId);
        }
        internal List<string> GetAddressConclusionsByObjectIds(int[] objectIds, int objectType)
        {
            return dal.GetAddressConclusionsByObjectIds(objectIds, objectType);
        }


        internal List<string> GetSubAreasByAreaId(int currentAreaId, int cityId)
        {
            return dal.GetSubAreasByAreaId(currentAreaId, cityId);
        }

        internal List<string> GetLocationsByAreaId(int currentAreaId, int cityId)
        {
            return dal.GetLocationsByAreaId(currentAreaId, cityId);
        }

        internal Dictionary<string, string> GetSubAreasByCityId(int cityId)
        {
            return dal.GetSubAreasByCityId(cityId);
        }

        internal Dictionary<string, string> GetLocationsByCityId(int cityId)
        {
            return dal.GetLocationsByCityId(cityId);
        }
    }
}