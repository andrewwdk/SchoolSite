using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    [MetadataType(typeof(SubjectMetaData))]
    public partial class Subject
    {
    }

    public class SubjectMetaData
    {
        [Required]
        [Display(Name = "Полное название")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Сокращённое название")]
        public string ShortName { get; set; }
    }
}