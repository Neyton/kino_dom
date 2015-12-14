using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kino_dom.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Ваедите имя")]
        [MaxLength(50, ErrorMessage = "максимальная длинна поля 50")]
        public string name { get; set; }

        [Required(ErrorMessage = "Ваедите email")]
        [MaxLength(256, ErrorMessage = "максимальная длинна поля 256")]
        public string email { get; set; }

        [Required(ErrorMessage = "Ваедите сообщение")]
        public string massege { get; set; }
    }
}