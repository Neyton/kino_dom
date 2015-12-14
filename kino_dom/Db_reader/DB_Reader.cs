using kino_dom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace kino_dom.Db_reader
{
    public class DB_Reader
    {
        public ArticleModel GetArticle(int id)
        {
            CinemaModel model = null;
            ICollection<CinemaModel> coll = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from Kino
                                                     where article = @id order by date DESC
    "))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("id", id));
                    coll = new Collection<CinemaModel>();
                    using (var reader = comand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coll.Add(
                            model = new CinemaModel(
                            Int32.Parse(reader["cinema_id"].ToString()),
                            reader["name"].ToString(),
                            reader["body"].ToString(),
                            DateTime.Parse(reader["date"].ToString()),
                            reader["img"].ToString(),
                            reader["video"].ToString(),
                            reader["article"].ToString()));
                        }
                    }
                }
            }
            ArticleModel model1 = new ArticleModel(coll);
            return model1;
        }

        public ArticleModel GetFreshCinema()
        {
            CinemaModel model = null;
            ICollection<CinemaModel> coll = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from Kino
                                                     order by date Desc"))
                {
                    comand.Connection = connection;
                    coll = new Collection<CinemaModel>();
                    int k = 0;
                    using (var reader = comand.ExecuteReader())
                    {
                        while (reader.Read()&&(k!=6))
                        {
                            coll.Add(
                            model = new CinemaModel(
                            Int32.Parse(reader["cinema_id"].ToString()),
                            reader["name"].ToString(),
                            reader["body"].ToString(),
                            DateTime.Parse(reader["date"].ToString()),
                            reader["img"].ToString(),
                            reader["video"].ToString(),
                            reader["article"].ToString()));
                            k++;
                        }
                    }
                }
            }
            ArticleModel model1 = new ArticleModel(coll);
            return model1;
        }

        public CinemaModel GetMovie(int id)
        {
            CinemaModel model = null;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from Kino
                                                     where cinema_id = @id"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("id", id));
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new CinemaModel(
                            Int32.Parse(reader["cinema_id"].ToString()),
                            reader["name"].ToString(),
                            reader["body"].ToString(),
                            DateTime.Parse(reader["date"].ToString()),
                            reader["img"].ToString(),
                            reader["video"].ToString(),
                            reader["article"].ToString());
                        }
                    }
                }
            }
            return model;
        }

        public void Registration(RegistrationModel model)
        {
            string a = model.login;
            string b = model.password;
            string c = model.email;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var comand = new SqlCommand(@"insert into UserT 
                                                     values (@a,@b,@c)"))
                {
                    comand.Parameters.Add(new SqlParameter("a", a));
                    comand.Parameters.Add(new SqlParameter("b", b));
                    comand.Parameters.Add(new SqlParameter("c", c));
                    comand.Connection = connection;
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
            }    
        }

        public bool Autorization(AutorizationModel model)
        {
            bool a = false;
            string login = model.login;
            string pas = model.password;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from UserT
                                                     where UserT.login = @login AND UserT.password = @pas"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("login", login));
                    comand.Parameters.Add(new SqlParameter("pas", pas));
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            a = true;
                        }
                    }
                }
            }
            return a;
        }

        public void Contact(ContactModel model)
        {
            string a = model.name;
            string b = model.email;
            string c = model.massege;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var comand = new SqlCommand(@"insert into contact 
                                                     values (@a,@b,@c, GETDATE())"))
                {
                    comand.Parameters.Add(new SqlParameter("a", a));
                    comand.Parameters.Add(new SqlParameter("b", b));
                    comand.Parameters.Add(new SqlParameter("c", c));
                    comand.Connection = connection;
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
            }
        }

        public void Ocenka(string a, string b, string c, string d)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var comand = new SqlCommand(@"insert into oc
                                                     values (@a,@b,@c,@d)"))
                {
                    comand.Parameters.Add(new SqlParameter("a", a));
                    comand.Parameters.Add(new SqlParameter("b", b));
                    comand.Parameters.Add(new SqlParameter("c", c));
                    comand.Parameters.Add(new SqlParameter("d", d));
                    comand.Connection = connection;
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
            }
        }

        public string GetUserId(string name)
        {
            string a = "";
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from UserT
                                                     where UserT.login = @name"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("name", name));
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            a = reader["user_id"].ToString();
                        }
                    }
                }
            }
            return a;
        }

        public string GetArticleById(int id)
        {
            string a = "";
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from Kino
                                                    where Kino.cinema_id = @id"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("id", id));
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            a = reader["article"].ToString();
                        }
                    }
                }
            }
            return a;
        }

        public void PushStrAlg(string str, string user_id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var comand = new SqlCommand(@"insert into algoritm
                                                     values (@user_id,@str)"))
                {
                    comand.Parameters.Add(new SqlParameter("user_id", user_id));
                    comand.Parameters.Add(new SqlParameter("str", str));
                    comand.Connection = connection;
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
            }
        }

        public string GetStrAlg(string id)
        {
            string a = "";
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from algoritm
                                                     where algoritm.user_id = @id"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("id", id));
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            a = reader["alg"].ToString();
                        }
                    }
                }
            }
            return a;
        }

        public void GangeStrAlg(string str, string user_id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var comand = new SqlCommand(@"Update algoritm
                                                     set alg = @str
                                                     where user_id = @user_id"))
                {
                    comand.Parameters.Add(new SqlParameter("user_id", user_id));
                    comand.Parameters.Add(new SqlParameter("str", str));
                    comand.Connection = connection;
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
            }
        }

        public List<CinemaModel> GetMovieRec(int id, string a1, string[] b)
        {
            CinemaModel model = null;
            List<CinemaModel> coll = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from Kino
                                                     where article = @id"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("id", id));
                    coll = new List<CinemaModel>();
                    using (var reader = comand.ExecuteReader())
                    {
                        while (coll.Count!=4||reader.Read())
                        {
                            coll.Add(
                            model = new CinemaModel(
                            Int32.Parse(reader["cinema_id"].ToString()),
                            reader["name"].ToString(),
                            reader["body"].ToString(),
                            DateTime.Parse(reader["date"].ToString()),
                            reader["img"].ToString(),
                            reader["video"].ToString(),
                            reader["article"].ToString()));
                        }
                    }
                }
            }
            return coll;
        }

        public void AddMovie(string name, string body, string img, string video, string article)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var comand = new SqlCommand(@"insert into Kino 
                                                     values (@name,@body, GETDATE(),@img, @video,@article)"))
                {
                    comand.Parameters.Add(new SqlParameter("name", name));
                    comand.Parameters.Add(new SqlParameter("body", body));
                    comand.Parameters.Add(new SqlParameter("img", img));
                    comand.Parameters.Add(new SqlParameter("video", video));
                    comand.Parameters.Add(new SqlParameter("article", article));
                    comand.Connection = connection;
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
            }
        }

        public void Dell(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var comand = new SqlCommand(@"delete from Kino
                                                     where cinema_id = @id"))
                {
                    comand.Parameters.Add(new SqlParameter("id", id));
                    comand.Connection = connection;
                    connection.Open();
                    comand.ExecuteNonQuery();
                }
            }
        }

        public double R(int id)
        {
            double a = 0;
            int k = 0;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from oc
                                                     where cinema_id = @id"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("id", id));
                    using (var reader = comand.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            k++;
                            a += Double.Parse(reader["oc"].ToString());
                        }
                        if (k != 0)
                            a = a / k;
                        else
                            a = 0;
                    }
                }
            }
            return a;
        }

        public bool Reg(RegistrationModel model)
        {
            bool a = true;
            string login = model.login;
            string pas = model.password;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from UserT
                                                     where UserT.login = @login"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("login", login));
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            a = false;
                        }
                    }
                }
            }
            return a;
        }


        /*k++;
                            if (k == 4)
                            {
                                for (int i = 0; i < b.Length; i++)
                                    coll.RemoveAt(coll.FindIndex(x => x.id == Int32.Parse(b[i])));
                                k = 0;
                            }
        /*public bool GangeStr(int id)
        {
            bool a = false;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var comand = new SqlCommand(@"select* from algoritm
                                                     where algoritm.user_id = @id"))
                {
                    comand.Connection = connection;
                    comand.Parameters.Add(new SqlParameter("id", id));
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            a = true;
                        }
                    }
                }
            }
            return a;
        }*/

    }
}