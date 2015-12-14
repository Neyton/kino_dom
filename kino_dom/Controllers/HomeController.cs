using kino_dom.Cookie;
using kino_dom.Db_reader;
using kino_dom.Models;
using kino_dom.Recomendaton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace kino_dom.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.ActiveI = "active";
            ViewBag.BodyP = "page1";
            CookieModel cook = new CookieModel();
            DB_Reader reader = new DB_Reader();
            MyClass r = new MyClass();
            ArticleModel model = null;
            if (cook.GetName() == null)
            {
                model = reader.GetFreshCinema();
            }
            else
            {
                if (reader.GetStrAlg(reader.GetUserId(cook.GetName())) == "")
                {
                    reader.PushStrAlg("0-0-0", reader.GetUserId(cook.GetName()));
                }
                model = r.GetRecomendation(r.Algoritm(reader.GetStrAlg(reader.GetUserId(cook.GetName()))));
                if (model.mas_c.Length==0)
                    model = reader.GetFreshCinema();
            }
            return View(model);
        }


        public ActionResult About_us()
        {
            ViewBag.ActiveA = "active";
            ViewBag.BodyP = "page2";
            return View();
        }

        public ActionResult Articles()
        {
            ViewBag.BodyP = "page3";
            ViewBag.ActiveAr = "active";
            return View();
        }

        [HttpGet]
        public ActionResult Autorization()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Autorization(AutorizationModel model)
        {
            DB_Reader reader = new DB_Reader();
            if (reader.Autorization(model))
            {
                string str = "";
                if (model.login == "admin")
                    str = "Admin";
                else
                    str = "User";
                CookieModel cook = new CookieModel();
                MyClass r = new MyClass();
                ArticleModel model1 = null;
                if (cook.GetName() == null)
                {
                    model1 = reader.GetFreshCinema();
                }
                else
                {
                    model1 = r.GetRecomendation(r.Algoritm(reader.GetStrAlg(reader.GetUserId(cook.GetName()))));
                    if (model1.mas_c.Length == 0)
                        model1 = reader.GetFreshCinema();
                }
                var ticet = new FormsAuthenticationTicket(2, model.login, DateTime.Now, DateTime.Now.AddMinutes(10), true, str);
                var encTicket = FormsAuthentication.Encrypt(ticet);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(cookie);
                return View("Index", model1);
            }
            else 
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Registration(RegistrationModel model)
        {

            DB_Reader reader = new DB_Reader();
            if (reader.Reg(model))
            {
                reader.Registration(model);
                var ticet = new FormsAuthenticationTicket(2, model.login, DateTime.Now, DateTime.Now.AddMinutes(10), true, "User");
                var encTicket = FormsAuthentication.Encrypt(ticet);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(cookie);
                CookieModel cook = new CookieModel();               
                ArticleModel model1 = reader.GetFreshCinema();             
                return View("Index", model1);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Article(int art)
        {
            DB_Reader reader = new DB_Reader();
            ArticleModel model = reader.GetArticle(art);
            return View(model);
        }

        [HttpGet]
        public ActionResult Item(int id_n)
        {
            DB_Reader reader = new DB_Reader();
            CinemaModel model = reader.GetMovie(id_n);
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Item(int id_n, CinemaModel model1)
        {
            DB_Reader reader = new DB_Reader();
            CookieModel cook = new CookieModel();
            string name = cook.GetName();
            string a1 = reader.GetUserId(name);
            string a2 = reader.GetArticleById(id_n);
            reader.Ocenka(id_n.ToString(), a1, a2, model1.oc.ToString());
            string str1 = reader.GetStrAlg(a1);
            if (str1 == "")
            {
                reader.PushStrAlg("0-0-0", a1);
            }
            //id article/id_movie/ocenka
            str1 += "|" + a2 + "-" + id_n + "-" + model1.oc;
            reader.GangeStrAlg(str1, a1);
            CinemaModel model = reader.GetMovie(id_n);
            return View(model);
        }

        public ActionResult Site_Map()
        {
            ViewBag.BodyP = "page5";
            ViewBag.ActiveS = "active";
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.BodyP = "page4";
            ViewBag.ActiveC = "active";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            ViewBag.BodyP = "page4";
            ViewBag.ActiveC = "active";
            DB_Reader reader = new DB_Reader();
            reader.Contact(model);
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        [ValidateInput(false)]
        public ActionResult Add(AddModel model)
        {
            DB_Reader reader = new DB_Reader();
            string str = "";
            var b = new byte[model.img.ContentLength];
            model.img.InputStream.Read(b, 0, model.img.ContentLength);
            str = Convert.ToBase64String(b);
            reader.AddMovie(model.name,model.boddy,str,model.video,model.article);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Dell(int id_n)
        {
            DB_Reader reader = new DB_Reader();
            reader.Dell(id_n);
            return View("Articles");
        }

    }
}
