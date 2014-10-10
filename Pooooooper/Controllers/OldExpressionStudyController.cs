using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HunterMVC.Models;
using System.Web.Script.Serialization;

namespace HunterMVC.Controllers
{
    public class OldExpressionStudyController : Controller
    {
        //
        // GET: /OldExpressionStudy/

        public ActionResult Index()
        {
            Response.BufferOutput = true;
            if (Session["username"] == null)
            {
                //return null;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Response.BufferOutput = true;

                HunterBL bl = new HunterBL();

                House house = bl.GetNewHouse(1);
                if (house != null)
                {
                    Dictionary<string, string> rootExpressionIdsVsCategoryIds = bl.GetRootExpressionsIdsVsCategoriesIds(house.CityId);
                    Dictionary<string, List<string>> categoriesVsAddresses = bl.GetCategoriesIdsVsAddressesIds(house.CityId);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    ViewBag.RootExpressionIdsVsCategoryIds = serializer.Serialize((object)rootExpressionIdsVsCategoryIds);
                    ViewBag.CategoriesVsAddresses = serializer.Serialize((object)categoriesVsAddresses);
                    if (house != null)
                    {
                        ViewBag.PostExpressionsVsTypes = serializer.Serialize((object)house.ExpressionsVsTypes);
                        ViewBag.PostImplementation = house;

                        ViewBag.House = serializer.Serialize(house);
                    }
                }
                
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveExpressionsAndGetNewPost(Dictionary<string, string> parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class
            Dictionary<string, string> expressionsVsRootExpressions = new Dictionary<string, string>();
            Dictionary<string, string> rootExpressionIdsVsCategoryIds = new Dictionary<string, string>();
            Dictionary<string, string> rest = new Dictionary<string, string>();
            if (parameters.ContainsKey("expressionsVsRootExpressions"))
            {
               expressionsVsRootExpressions = serializer.Deserialize<Dictionary<string, string>>(parameters["expressionsVsRootExpressions"]);
           
            }
            if (parameters.ContainsKey("rootExpressionIdsVsCategoryIds"))
            {
               rootExpressionIdsVsCategoryIds = serializer.Deserialize<Dictionary<string, string>>(parameters["rootExpressionIdsVsCategoryIds"]);
            
            }
            if (parameters.ContainsKey("rest"))
            {
                 rest = serializer.Deserialize<Dictionary<string, string>>(parameters["rest"]);
           
            }
            
            int houseId = 0;
            if (rest.ContainsKey("houseId"))
            {
                houseId = int.Parse(rest["houseId"]);
            }

            int cityId = 0;
            if (rest.ContainsKey("cityId"))
            {
                cityId = int.Parse(rest["cityId"]);
            }
            // Save Expressions in DB
            HunterBL bl = new HunterBL();
            if (expressionsVsRootExpressions.Count > 0)
            {
                bl.SaveExpressions(expressionsVsRootExpressions);
                //Dictionary<int, int> expressionsIdVsCategoryId = bl.GetExpressionsIds(expressionsVsRootExpressions.Keys);

                // Get New Post from DB
                if (bl.VerifyHouse(houseId))
                {
                    // Get New Post from DB
                    House newHouse = bl.GetNewHouse(cityId);
                    if (newHouse != null)
                    {
                        //    newHouse.ExpressionsVsRootExpressionsJson = serializer.Serialize((object)newHouse.ExpressionsVsRootExpressions);
                        newHouse.ExpressionsVsTypesJson = serializer.Serialize((object)newHouse.ExpressionsVsTypes);
                        return Json(newHouse, JsonRequestBehavior.AllowGet);
                    }
                }
                /*
                if (bl.UpdateHouseFields(expressionsIdVsCategoryId, houseId))
                {
                    
                }*/
            }
            else
            {
                if ((houseId == 0) || (houseId > 0 && bl.VerifyHouse(houseId)))
                {

                    // Get New Post from DB
                    House newHouse = bl.GetNewHouse(cityId);
                    if (newHouse != null)
                    {
                        //  newHouse.ExpressionsVsRootExpressionsJson = serializer.Serialize((object)newHouse.ExpressionsVsRootExpressions);
                        newHouse.ExpressionsVsTypesJson = serializer.Serialize((object)newHouse.ExpressionsVsTypes);
                        return Json(newHouse, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult AddBugAndGetNewPost(Dictionary<string, string> parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class

            Dictionary<string, string> expressionsVsRootExpressions = serializer.Deserialize<Dictionary<string, string>>(parameters["expressionsVsRootExpressions"]);
            Dictionary<string, string> rootExpressionIdsVsCategoryIds = serializer.Deserialize<Dictionary<string, string>>(parameters["rootExpressionIdsVsCategoryIds"]);
            Dictionary<string, string> rest = serializer.Deserialize<Dictionary<string, string>>(parameters["houseId"]);

            int houseId = 0;
            if (rest.ContainsKey("houseId"))
            {
                houseId = int.Parse(rest["houseId"]);
            }

            string comment = String.Empty;
            if (rest.ContainsKey("comment"))
            {
                comment = rest["comment"];
            }

            int cityId = 0;
            if (rest.ContainsKey("cityId"))
            {
                cityId = int.Parse(rest["cityId"]);
            }
            // Save Expressions in DB
            HunterBL bl = new HunterBL();

            bl.AddBug(houseId, comment);

            // Get New Post from DB
            if (bl.VerifyHouse(houseId))
            {
                // Get New Post from DB
                House newHouse = bl.GetNewHouse(cityId);
                if (newHouse != null)
                {
                    newHouse.ExpressionsVsRootExpressionsJson = serializer.Serialize((object)newHouse.ExpressionsVsRootExpressions);
                    newHouse.ExpressionsVsTypesJson = serializer.Serialize((object)newHouse.ExpressionsVsTypes);
                    return Json(newHouse, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult GetAllCategories()
        {
            // Save Expressions in DB
            HunterBL bl = new HunterBL();
            Dictionary<string, string> categoryIdsVsNames = bl.GetAllCategories();

            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class

            //first way
            string json = serializer.Serialize((object)categoryIdsVsNames);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllRootExpressions()
        {
            // Save Expressions in DB
            HunterBL bl = new HunterBL();
            Dictionary<string, string> rootIdsVsNames = bl.GetAllRootExpressions();

            JavaScriptSerializer serializer = new JavaScriptSerializer(); //creating serializer instance of JavaScriptSerializer class

            //first way
            string json = serializer.Serialize((object)rootIdsVsNames);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}
