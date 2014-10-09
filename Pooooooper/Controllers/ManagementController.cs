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
        public ActionResult GenerateBusinessReport(Dictionary<string, string> parameters)
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
    }
}
