using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterMVC.Models
{
    public class BusinessReportResult
    {
        public string Sender { get; set; }
        public string PostText { get; set; }
        public DateTime DateCreated { get; set; }

        public string City { get; set; }
    }
}