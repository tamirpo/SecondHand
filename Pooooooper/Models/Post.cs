using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterMVC.Models
{
    public class Post
    {
        public String Message { get; set; }
        public PostImplementation Implementation { get; set; }

        public Post()
        {

        }

        public Post(String message, PostImplementation implementation)
        {
            this.Message = message;
            this.Implementation = implementation;
        }
    }
}