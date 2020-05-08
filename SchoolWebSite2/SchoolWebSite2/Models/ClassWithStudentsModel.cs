using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    public class ClassWithStudentsModel
    {
        public Class Class { get; set; }

        [DisplayName("Новый ученик")]
        public Nullable<int> NewStudentId { get; set; }

        public List<Student> Students { get; set; }
    }
}