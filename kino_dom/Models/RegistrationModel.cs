using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kino_dom.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Ваедите имя")]
        [MaxLength(50, ErrorMessage = "максимальная длинна поля 50")]
        public string login { get; set; }

        [Required(ErrorMessage = "Ваедите пароль")]
        [MaxLength(50, ErrorMessage = "максимальная длинна поля 50")]
        public string password { get; set; }

        [Required(ErrorMessage = "Ваедите email")]
        [MaxLength(256, ErrorMessage = "максимальная длинна поля 256")]
        public string email { get; set; }
    }
}