using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace HunterMVC.Models
{
    public class PostImplementation
    {

        public String Message { get; set; }

        public String[] MessageWords { get; set; }
        public String Type { get; set; }
        [ScriptIgnore]
        public Dictionary<String, String> ExpressionsVsRootExpressions { get; set; }
        [ScriptIgnore]
        public Dictionary<String, String> ExpressionsVsTypes { get; set; }

        public string ExpressionsVsRootExpressionsJson { get; set; }
        public string ExpressionsVsTypesJson { get; set; }

        public List<string> ImageUrls { get; set; }

        public PostImplementation(String message)
        {
            this.MessageWords = message.Split(' ');
            this.ExpressionsVsRootExpressions = new Dictionary<string, string>();
            this.ExpressionsVsTypes = new Dictionary<string, string>();
            this.Message = message;
            this.ImageUrls = new List<string>();
        }

        public PostImplementation()
        {
            this.ExpressionsVsRootExpressions = new Dictionary<string, string>();
            this.ExpressionsVsTypes = new Dictionary<string, string>();
            this.ImageUrls = new List<string>();
            /*this.Message = String.Empty;
            this.MessageWords = Message.Split(' ');*/
        }
    }
}