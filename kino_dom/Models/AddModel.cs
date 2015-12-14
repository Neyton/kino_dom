using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kino_dom.Models
{
    public class AddModel
    {
        [Required(ErrorMessage = "Ваедите имя")]
        [MaxLength(100, ErrorMessage = "максимальная длинна поля 100")]
        public string name { get; set; }
        
        [Required(ErrorMessage = "Ваедите описание")]
        public string boddy { get; set; }
        
        [Required(ErrorMessage = "Добавьте картинку")]
        public HttpPostedFileWrapper img { get; set; }
        
        [Required(ErrorMessage = "Добавьте трейлер")]
        public string video { get; set; }
        
        [Required(ErrorMessage = "Ваедите жанр")]
        [MaxLength(50, ErrorMessage = "максимальная длинна поля 50")]
        public string article { get; set; }

    }
}