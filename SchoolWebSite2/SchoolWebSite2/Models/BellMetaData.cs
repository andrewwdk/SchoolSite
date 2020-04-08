using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    [MetadataType(typeof(BellMetaData))]
    public partial class Bell
    {
    }

    public class BellMetaData
    {
        [Display(Name = "Номер")]
        public int OrderNumber { get; set; }
        [Required]
        [Display(Name = "Начало урока")]
        public System.TimeSpan StartTime { get; set; }
        [Required]
        [Display(Name = "Конец урока")]
        public System.TimeSpan EndTime { get; set; }
    }
}