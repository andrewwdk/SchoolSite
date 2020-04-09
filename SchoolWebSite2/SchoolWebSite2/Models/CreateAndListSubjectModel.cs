using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    public class CreateAndListSubjectModel
    {
        public List<Subject> Subjects { get; set; }
        public Subject NewSubject { get; set; }
    }
}