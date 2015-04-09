using HunterMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HunterMVC.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Management/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetAddressesForCity(Dictionary<string, string> parameters)
        {
            List<Dictionary<string, List<string>>> result = new List<Dictionary<string, List<string>>>();
            if (parameters.ContainsKey("cityId"))
            {
                int cityId = Int32.Parse(parameters["cityId"]);

                // Save Expressions in DB
                HunterBL bl = new HunterBL();

                Dictionary<string, List<string>> areasVsSubAreas = new Dictionary<string, List<string>>();
                Dictionary<string, List<string>> areasVsLocations = new Dictionary<string, List<string>>();

                Dictionary<string, String> areaIdVsName = bl.GetAreasByCityId(cityId);
                foreach (string currentAreaIdString in areaIdVsName.Keys)
                {
                    int currentAreaId = Int32.Parse(currentAreaIdString);
                    areasVsSubAreas.Add(currentAreaIdString, bl.GetSubAreasByAreaId(currentAreaId, cityId));
                    areasVsLocations.Add(currentAreaIdString, bl.GetLocationsByAreaId(currentAreaId, cityId)); 
                }

                result.Add(areasVsSubAreas);
                result.Add(areasVsLocations);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAddressesIdVsNamesForCity(Dictionary<string, string> parameters)
        {
            List<Dictionary<string, String>> result = new List<Dictionary<string, String>>();
            if (parameters.ContainsKey("cityId"))
            {
                int cityId = Int32.Parse(parameters["cityId"]);

                // Save Expressions in DB
                HunterBL bl = new HunterBL();

                Dictionary<String, String> areaIdVsName = bl.GetAreasByCityId(cityId);
                Dictionary<String, String> subAreaIdVsName = bl.GetSubAreasByCityId(cityId);
                Dictionary<String, String> locationIdVsName = bl.GetLocationsByCityId(cityId);

                result.Add(areaIdVsName);
                result.Add(subAreaIdVsName);
                result.Add(locationIdVsName);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GenerateApartmentSearchPostsReport(Dictionary<string, string> parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class

            DateTime startDate = new DateTime();
            if (parameters.ContainsKey("startDate"))
            {
                startDate = DateTime.Parse(parameters["startDate"]);
            }
            DateTime endDate = new DateTime();
            if (parameters.ContainsKey("endDate"))
            {
                endDate = DateTime.Parse(parameters["endDate"]);
            }

            // Save Expressions in DB
            HunterBL bl = new HunterBL();

            List<BusinessReportResult> result = bl.GetAllFromBusinessHouses(startDate, endDate);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GenerateApartmentsDetailsReport(Dictionary<string, string> parameters)
        {
            List<HouseNames> result = new List<HouseNames>();
            List<HouseNames> resultFiltered = new List<HouseNames>();

            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class

            DateTime startDate = new DateTime();
            if (parameters.ContainsKey("startDate"))
            {
                startDate = DateTime.Parse(parameters["startDate"]);
            }
            DateTime endDate = new DateTime();
            if (parameters.ContainsKey("endDate"))
            {
                endDate = DateTime.Parse(parameters["endDate"]);
            }
            int cityId = 0;
            if (parameters.ContainsKey("cityId"))
            {
                cityId = Int32.Parse(parameters["cityId"]);
            }
            int areaId = 0;
            if (parameters.ContainsKey("areaId"))
            {
                areaId = Int32.Parse(parameters["areaId"]);
            }
            int subAreaId = 0;
            if (parameters.ContainsKey("subAreaId"))
            {
                subAreaId = Int32.Parse(parameters["subAreaId"]);
            }
            int locationId = 0;
            if (parameters.ContainsKey("locationId"))
            {
                locationId = Int32.Parse(parameters["locationId"]);
            }
            int fromPrice = 0;
            if (parameters.ContainsKey("fromPrice") && !String.IsNullOrEmpty(parameters["fromPrice"]))
            {
                fromPrice = Int32.Parse(parameters["fromPrice"]);
            }
            int toPrice = 0;
            if (parameters.ContainsKey("toPrice") && !String.IsNullOrEmpty(parameters["toPrice"]))
            {
                toPrice = Int32.Parse(parameters["toPrice"]);
            }
            int fromSize = 0;
            if (parameters.ContainsKey("fromSize") && !String.IsNullOrEmpty(parameters["fromSize"]))
            {
                fromSize = Int32.Parse(parameters["fromSize"]);
            }
            int toSize = 0;
            if (parameters.ContainsKey("toSize") && !String.IsNullOrEmpty(parameters["toSize"]))
            {
                toSize = Int32.Parse(parameters["toSize"]);
            }
            double fromRoomsNumber = 0.0;
            if (parameters.ContainsKey("fromRoomsNumber") && !String.IsNullOrEmpty(parameters["fromRoomsNumber"]))
            {
                fromRoomsNumber = Double.Parse(parameters["fromRoomsNumber"]);
            }
            double toRoomsNumber = 0.0;
            if (parameters.ContainsKey("toRoomsNumber") && !String.IsNullOrEmpty(parameters["toRoomsNumber"]))
            {
                toRoomsNumber = Double.Parse(parameters["toRoomsNumber"]);
            }
            int fromRoomatesNumber = 0;
            if (parameters.ContainsKey("fromRoomatesNumber") && !String.IsNullOrEmpty(parameters["fromRoomatesNumber"]))
            {
                fromRoomatesNumber = Int32.Parse(parameters["fromRoomatesNumber"]);
            }
            int toRoomatesNumber = 0;
            if (parameters.ContainsKey("toRoomatesNumber") && !String.IsNullOrEmpty(parameters["toRoomatesNumber"]))
            {
                toRoomatesNumber = Int32.Parse(parameters["toRoomatesNumber"]);
            }
            int furnitured = 0;
            if (parameters.ContainsKey("furnitured") && !String.IsNullOrEmpty(parameters["furnitured"]))
            {
                furnitured = Int32.Parse(parameters["furnitured"]);
            }
            int renovated = 0;
            if (parameters.ContainsKey("renovated") && !String.IsNullOrEmpty(parameters["renovated"]))
            {
                renovated = Int32.Parse(parameters["renovated"]);
            }
            int elevator = 0;
            if (parameters.ContainsKey("elevator") && !String.IsNullOrEmpty(parameters["elevator"]))
            {
                elevator = Int32.Parse(parameters["elevator"]);
            }
            int sublet = 0;
            if (parameters.ContainsKey("sublet") && !String.IsNullOrEmpty(parameters["sublet"]))
            {
                sublet = Int32.Parse(parameters["sublet"]);
            }
            int balcony = 0;
            if (parameters.ContainsKey("balcony") && !String.IsNullOrEmpty(parameters["balcony"]))
            {
                balcony = Int32.Parse(parameters["balcony"]);
            }
            int smoke = 0;
            if (parameters.ContainsKey("smoke") && !String.IsNullOrEmpty(parameters["smoke"]))
            {
                smoke = Int32.Parse(parameters["smoke"]);
            }
            int pets = 0;
            if (parameters.ContainsKey("pets") && !String.IsNullOrEmpty(parameters["pets"]))
            {
                pets = Int32.Parse(parameters["pets"]);
            }
            int parking = 0;
            if (parameters.ContainsKey("parking") && !String.IsNullOrEmpty(parameters["parking"]))
            {
                parking = Int32.Parse(parameters["parking"]);
            }
            int fromAgency = 0;
            if (parameters.ContainsKey("fromAgency") && !String.IsNullOrEmpty(parameters["fromAgency"]))
            {
                fromAgency = Int32.Parse(parameters["fromAgency"]);
            }
            int purpose = 0;
            if (parameters.ContainsKey("purpose") && !String.IsNullOrEmpty(parameters["purpose"]))
            {
                purpose = Int32.Parse(parameters["purpose"]);
            }

            UserSearch search = new UserSearch();
            //search.City = cityId;
            search.Balcony = balcony;
            search.FromAgency = fromAgency;
            search.FromPrice = fromPrice;
            search.FromRoomsNumber = fromRoomsNumber;
            search.FromSize = fromSize;
            search.FromTotalRoommatesNumber = fromRoomatesNumber;
            search.Furnitured = furnitured;
            search.Parking = parking;
            search.Pets = pets;
            search.Purpose = purpose;
            search.Renovated = renovated;
            search.Smoke = smoke;
            search.Sublet = sublet;
            search.ToPrice = toPrice;
            search.ToRoomsNumber = toRoomsNumber;
            search.ToSize = toSize;
            search.ToTotalRoommatesNumber = toRoomatesNumber;

            // Save Expressions in DB
            HunterBL bl = new HunterBL();

            List<HouseNames> currentSeachResult = bl.GetApartmentsExact(search, startDate, endDate);
            foreach (HouseNames currentResult in currentSeachResult)
            {
                if (!result.Contains(currentResult))
                {
                    result.Add(currentResult);
                }
            }

            /*if (result.Count > 0)
            {
                if (locationId > 0)
                {
                    search.Locations = new int[1];
                    search.Locations[0] = locationId;
                    search.AddressConclusionIds.AddRange(bl.GetAddressConclusionsByObjectIds(search.Locations, 2));
                }
                else if (subAreaId > 0)
                {
                    search.SubAreas = new int[1];
                    search.SubAreas[0] = subAreaId;
                    search.AddressConclusionIds.AddRange(bl.GetAddressConclusionsByObjectIds(search.SubAreas, 1));
                }
                else if (areaId > 0)
                {
                    List<String> subAreasIdsString = bl.GetSubAreasByAreaId(areaId, cityId);
                    foreach (string currentSubAreaIdString in subAreasIdsString)
                    {
                        search.SubAreasIds.Add(Int32.Parse(currentSubAreaIdString));
                    }
                    search.SubAreas = search.SubAreasIds.ToArray();
                    search.AddressConclusionIds.AddRange(bl.GetAddressConclusionsByObjectIds(search.SubAreas, 1));

                    List<String> locationIdsString = bl.GetLocationsByAreaId(areaId, cityId);
                    foreach (string currentLocationIdString in locationIdsString)
                    {
                        search.LocationsIds.Add(Int32.Parse(currentLocationIdString));
                    }
                    search.Locations = search.LocationsIds.ToArray();
                    search.AddressConclusionIds.AddRange(bl.GetAddressConclusionsByObjectIds(search.Locations, 2));
                }
                else // by city
                {
                    Dictionary<string, String> areaIds = bl.GetAreasByCityId(cityId);
                    foreach (string currentAreaIdString in areaIds.Keys)
                    {
                        int currentAreaId = Int32.Parse(currentAreaIdString);
                        List<String> subAreasIdsString = bl.GetSubAreasByAreaId(currentAreaId, cityId);
                        foreach (string currentSubAreaIdString in subAreasIdsString)
                        {
                            search.SubAreasIds.Add(Int32.Parse(currentSubAreaIdString));
                        }
                        search.SubAreas = search.SubAreasIds.ToArray();
                        search.AddressConclusionIds.AddRange(bl.GetAddressConclusionsByObjectIds(search.SubAreas, 1));

                        List<String> locationIdsString = bl.GetLocationsByAreaId(currentAreaId, cityId);
                        foreach (string currentLocationIdString in locationIdsString)
                        {
                            search.LocationsIds.Add(Int32.Parse(currentLocationIdString));
                        }
                        search.Locations = search.LocationsIds.ToArray();
                        search.AddressConclusionIds.AddRange(bl.GetAddressConclusionsByObjectIds(search.Locations, 2));
                
                    }
                }

                foreach (string currentAddress in search.AddressConclusionIds)
                {
                    IEnumerable<HouseNames> toAdd = result.Where(o => o.AddressesIds.Contains(currentAddress));
                    foreach (HouseNames currentHouse in toAdd)
                    {
                        if (!resultFiltered.Contains(currentHouse))
                        {
                            resultFiltered.Add(currentHouse);
                        }
                    }
                }

                resultFiltered.Sort();
            }*/

            return Json(resultFiltered, JsonRequestBehavior.AllowGet);
        }
    }
}
