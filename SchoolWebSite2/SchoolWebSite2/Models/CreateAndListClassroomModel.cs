using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    public class CreateAndListClassroomModel
    {
        public List<Classroom> Classrooms { get; set; }
        public Classroom NewClassroom { get; set; }
    }
}