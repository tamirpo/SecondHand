using HunterMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HunterMVC.Controllers
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string id)
        {
            return "ok";
        }


        // POST api/<controller>/login
        [TokenValidation]
        public string Login([NakedBody] string imei)
        {
            HunterBL bl = new HunterBL();
            string accessToken = bl.LoginUser(imei);
            return accessToken;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}