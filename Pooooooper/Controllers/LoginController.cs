using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHandMVCnew.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(Dictionary<string,string> data)
        {
            string usernameString = data["username"];
            string passwordString = data["password"];

            if (usernameString.Equals("test") && passwordString.Equals("4shallwe"))
            {
                Session["username"] = usernameString;
                //return RedirectToAction("Index", "Index");
            }
            return Json("");
        }
    }
}
