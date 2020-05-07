using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {
        public string FullName { get { return Person.FullName; } }
    }

    public class StudentMetaData
    {

    }
}