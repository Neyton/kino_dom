using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kino_dom.Models
{
    public class CinemaModel
    {
        public CinemaModel()
        { }
        public CinemaModel(int id, string name, string body, DateTime date, string img, string video, string article) 
        {
            this.id = id;
            this.name = name;
            this.body = body;
            this.date = date;
            this.img = img;
            this.video = video;
            this.article = article;
            this.oc = 0;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string body { get; set; }
        public DateTime date { get; set; }
        public string img { get; set; }
        public string video { get; set; }
        public string article { get; set; }
        public int oc { get; set; }
    }
}