using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    [MetadataType(typeof(PersonMetaData))]
    public partial class Person
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
    public class PersonMetaData
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string Patronimic { get; set; }

        [Display(Name = "Домашний телефон")]
        public string HomeNumber { get; set; }

        [Display(Name = "Мобильный телефон")]
        public string MobileNumber { get; set; }

        [Display(Name = "Дата рождения")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }

        [Display(Name = "Информация")]
        public string Info { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Логин")]
        [MinLength(8, ErrorMessage = "Длина логина не должна быть меньше 8 символов!")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [MinLength(8, ErrorMessage ="Длина пароля не должна быть меньше 8 символов!")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}