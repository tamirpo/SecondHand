using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using HunterMVC;

namespace HunterMVC
{
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string encryptedToken;

            try
            {
                encryptedToken = actionContext.Request.Headers.GetValues("Authorization").First();
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Missing Authorization-Token")
                };
                return;
            }

            try
            {
                //RSAClass.Decrypt(token);
                String token = encryptedToken;
                String imei = "ss32e";

                //AuthorizedUserRepository.GetUsers().First(x => x.Name == RSAClass.Decrypt(token));
                HunterBL bl = new HunterBL();
                //bool isUserExists = bl.VerifyUserLogin(token, imei);
                bool isUserExists = true; 
                base.OnActionExecuting(actionContext);
            }
            catch (Exception ex)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Unauthorized User")
                };
                return;
            }
        }
    }
}