using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    [MetadataType(typeof(ClassMetaData))]
    public partial class Class
    {
        public string FullName { get { return Number.ToString() + " " + Code; } }
        public string ClassTeacherName { get; set; }
    }

    public class ClassMetaData
    {
        [Required]
        [Range(1, 11)]
        [Display(Name = "Номер")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Display(Name = "Классный руководитель")]
        public Nullable<int> ClassTeacher { get; set; }

        [Display(Name = "Информация")]
        public string Info { get; set; }
    }
}