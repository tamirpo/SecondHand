using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HunterMVC.Models;
using System.Web.Script.Serialization;
using HunterMVC;

namespace HunterMVC.Controllers
{
    public class HouseController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index(string houseId)
        {
            Response.BufferOutput = true;

            HunterBL bl = new HunterBL();

            House house = bl.GetNewHouseFromHouseId(houseId);

            if (house != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                ViewBag.PostExpressionsVsTypes = serializer.Serialize((object)house.ExpressionsVsTypes);
                ViewBag.PostImplementation = house;
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetNeighborhoods(Dictionary<string, string> parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class

            //Dictionary<string, string> expressionsVsRootExpressions = serializer.Deserialize<Dictionary<string, string>>(parameters["expressionsVsRootExpressions"]);
            //Dictionary<string, string> rootExpressionIdsVsCategoryIds = serializer.Deserialize<Dictionary<string, string>>(parameters["rootExpressionIdsVsCategoryIds"]);
            int cityId = serializer.Deserialize<int>(parameters["cityId"]);
            //int houseId = 0;
            //if (rest.ContainsKey("houseId"))
            //{
            //    houseId = int.Parse(rest["houseId"]);
            //}

            
            // Save Expressions in DB
            HunterBL bl = new HunterBL();
            Dictionary<string,string> neighborhoods = bl.GetNeighborhoods(cityId);
            string resultJson = serializer.Serialize((object)neighborhoods);
            return Json(resultJson, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetLocations(Dictionary<string, string> parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class

            //Dictionary<string, string> expressionsVsRootExpressions = serializer.Deserialize<Dictionary<string, string>>(parameters["expressionsVsRootExpressions"]);
            //Dictionary<string, string> rootExpressionIdsVsCategoryIds = serializer.Deserialize<Dictionary<string, string>>(parameters["rootExpressionIdsVsCategoryIds"]);
            int cityId = serializer.Deserialize<int>(parameters["cityId"]);
            //int houseId = 0;
            //if (rest.ContainsKey("houseId"))
            //{
            //    houseId = int.Parse(rest["houseId"]);
            //}


            // Save Expressions in DB
            HunterBL bl = new HunterBL();
            Dictionary<string, string> locations = bl.GetLocations(cityId);
            string resultJson = serializer.Serialize((object)locations);
            return Json(resultJson, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateHouseAddress(Dictionary<string, string> parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
    
            int houseId = serializer.Deserialize<int>(parameters["houseId"]);
            List<int> selectedNeighborhoods = serializer.Deserialize<List<int>>(parameters["selectedNeighborhoods"]);
            List<int> selectedLocations = serializer.Deserialize<List<int>>(parameters["selectedLocations"]);

            HunterBL bl = new HunterBL();
            string result = bl.SaveHouseAddress(houseId, selectedNeighborhoods, selectedLocations);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
