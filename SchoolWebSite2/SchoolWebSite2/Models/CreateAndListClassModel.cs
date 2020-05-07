using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    public class CreateAndListClassModel
    {
        public List<Class> Classes { get; set; }
        public Class NewClass { get; set; }
    }
}