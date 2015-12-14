using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kino_dom.Models
{
    public class AutorizationModel
    {
        [Required(ErrorMessage = "Ваедите логин")]
        [MaxLength(50, ErrorMessage = "максимальная длинна поля 50")]
        public string login { get; set; }

        [Required(ErrorMessage = "Ваедите пароль")]
        [MaxLength(50, ErrorMessage = "максимальная длинна поля 50")]
        public string password { get; set; }
    }
}