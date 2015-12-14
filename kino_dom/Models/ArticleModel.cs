using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kino_dom.Models
{
    public class ArticleModel
    {
        public ArticleModel(ICollection<CinemaModel> model)
        {
            CinemaModel[] cinema = new CinemaModel[model.Count];
            model.CopyTo(cinema, 0);
            this.mas_c = cinema;
        }
        public CinemaModel[] mas_c { get; set; }
    }
}