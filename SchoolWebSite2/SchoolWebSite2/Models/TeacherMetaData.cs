using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    [MetadataType(typeof(TeacherMetaData))]
    public partial class Teacher
    {
        public string FullName { get { return Person.FullName; } }
    }

    public class TeacherMetaData
    {
        [Required]
        [Range(1, 100)]
        [Display(Name = "Нагрузка")]
        public Nullable<int> TeacherLoad { get; set; }
    }
}