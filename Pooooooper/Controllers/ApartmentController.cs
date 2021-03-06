﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HunterMVC.Models;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;
using HunterMVC;
using Newtonsoft.Json.Converters;


namespace HunterMVC.Controllers
{
    public class ApartmentController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        /*
        // GET api/<controller>/GetApartmentsForUser
        [TokenValidation]
        public string GetApartmentsForUser([FromUri] int userId)
        {
            List<House> result = new List<House>();
            SecondHandBL bl = new SecondHandBL();

            List<UserSearch> userSearches = bl.GetUserSearchesByIds(userId);
            foreach (UserSearch userSearch in userSearches)
            {
                result.AddRange(bl.GetApartments(userSearch));
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            String resultJson = serializer.Serialize(result);

            return resultJson;
        }
         */

        // GET api/<controller>/GetApartmentsForUser
        [TokenValidation]
        [HttpGet]
        public HttpResponseMessage SearchApartmentsBySearchIds(string searchId)
        {
            DateTime lastGrabDate = DateTime.Now.AddDays(-10);

            List<House> result = new List<House>();
            List<House> resultFiltered = new List<House>();

            HunterBL bl = new HunterBL();

            UserSearch userSearch = bl.GetUserSearchById(searchId);
            List<House> currentSeachResult = bl.GetApartments(userSearch, lastGrabDate);
            foreach (House currentResult in currentSeachResult)
            {
                if (!result.Contains(currentResult))
                {
                    result.Add(currentResult);
                }
            }

            if (result.Count > 0)
            {
                resultFiltered = result;

                resultFiltered.Sort();
                resultFiltered.Reverse();
                //resultFiltered = result.GetRange(0, limit);

                bl.AttachPostsImages(ref resultFiltered);
            }

            string resultJson = JsonConvert.SerializeObject(resultFiltered, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new SkipEmptyContractResolver() , DefaultValueHandling =  DefaultValueHandling.Ignore});
            return new HttpResponseMessage()
            {
                Content = new StringContent(resultJson, System.Text.Encoding.UTF8, "application/json")
            };
        }

        // POST api/<controller>/login
        [TokenValidation]
        public string Login([NakedBody] string imei)
        {
            HunterBL bl = new HunterBL();
            string accessToken = bl.LoginUser(imei);
            return accessToken;
        }

        // POST api/<controller>/search
        [TokenValidation]
        public string SaveSearch([NakedBody] string searchCriteriaJson)
        {
            /*{
              "ToPrice": 5500,
              "ToRoomsNumber": 2.5,
              "Addresses": [
                {
                  "City": 1,
                  "Areas": [
                    2,
                    4
                  ],
                  "Locations": []
                }
              ],
              "Purpose": 19,
              "FromPrice": 4000,
              "UserId": "357506057604808",
              "FromRoomsNumber": 2
            }*/
            String result = String.Empty;
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                UserSearch searchCriteria = serializer.Deserialize<UserSearch>(searchCriteriaJson);

                HunterBL bl = new HunterBL();
                //List<House> houses = bl.GetApartments(searchCriteria);
                result = bl.SaveUserSearch(searchCriteria);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }



        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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