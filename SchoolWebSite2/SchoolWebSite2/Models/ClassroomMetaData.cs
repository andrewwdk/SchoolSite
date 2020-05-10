using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    [MetadataType(typeof(ClassroomMetaData))]
    public partial class Classroom
    {
    }

    public class ClassroomMetaData
    {
        [Required]
        [Range(1, 9999)]
        [Display(Name = "Номер")]
        public int Number { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Количество мест")]
        public int SitsCount { get; set; }

        [Display(Name = "Наличие компьютеров")]
        public bool HasComputers { get; set; }

        [Display(Name = "Наличие цифрового оборудования")]
        public bool HasDigitalEquipment { get; set; }
    }
}