using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    public class TeacherWithSubjectsModel
    {
        public Teacher Teacher { get; set; }

        public List<Subject> Subjects { get; set; }

        [Display(Name = "Новый предмет")]
        public Nullable<int> NewSubjectId { get; set; }
    }
}