using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kino_dom.Models;
using System.Collections;
using kino_dom.Db_reader;

namespace kino_dom.Recomendaton
{
    public class MyClass
    {
        public string [,] Algoritm(string str)
        {
            string []b = str.Split('|');
            string[,] a = new string[b.Length,3];
            string[] c = new string[2];
            for (int i = 0; i < b.Length; i++)
            {
                c = b[i].Split('-');
                for (int j = 0; j < 3; j++)
                {
                    a[i,j] = c[j];
                }
            }
            return a;
        }
    
        public ArticleModel GetRecomendation(string [,]a)
        {
            double fun=0, com = 0, drum=0, act=0;
            int k=1;
            //фантастика 1
            for (int i= 0; i <a.Length/3; i++)
            {
                if (a[i,0] == "1")
                {
                    fun+=Int32.Parse(a[i,2]);
                    k++;
                }
            }
            fun = fun/k;
            k=1;
            //сомедии 2
            for (int i= 0; i <a.Length/3; i++)
            {
                if (a[i,0] == "2")
                {
                    com+=Int32.Parse(a[i,2]);
                    k++;
                }
            }
            com = com/k;
            k=1;
            //драмы 3
            for (int i= 0; i <a.Length/3; i++)
            {
                if (a[i, 0] == "3")
                {
                    drum+=Int32.Parse(a[i,2]);
                    k++;
                }
            }
            drum = drum/k;
            k=1;
            //боевики 4
            for (int i= 0; i <a.Length/3; i++)
            {
                if (a[i, 0] == "4")
                {
                    act+=Int32.Parse(a[i,2]);
                    k++;
                }
            }
            act = act/k;
            k=1;

            string [,] b = new string [4, 2];
            b[0,0] = "1";
            b[0,1] += fun;
            b[1,0] = "2";
            b[1,1] += com;
            b[2,0] = "3";
            b[2,1] += drum;
            b[3,0] = "4";
            b[3,1] += act;

            string e = "", c = "";
            for (int i = 0; i < 4; ++i)
                for (int j = i+1; j < 4; ++j)
                {
                    if (Double.Parse(b[i,1])<Double.Parse(b[j,1]))
                    {
                        e = b[i,0];
                        c = b[i,1];
                        b[i,0] = b[j,0];
                        b[i,1] = b[j,1];
                        b[j, 0] = e;
                        b[j, 1] = c;
                    }
                }
            string s = "";
            for (int i = 0; i<a.Length/3; i++)
                s+=a[i,1]+',';
            DB_Reader reader = new DB_Reader();
            string[] w = s.Split(',');
            int q = 0;
            ArticleModel model = reader.GetArticle(Int32.Parse(b[0, 0]));
            List<CinemaModel> li1 = model.mas_c.ToList<CinemaModel>();
            for (int i = 0; i < w.Length-1; i++)
            {
                q = li1.FindIndex(x => x.id == Int32.Parse(w[i]));
                if(q!=-1)
                    li1.RemoveAt(q);
            }
            ArticleModel model1 = reader.GetArticle(Int32.Parse(b[1, 0]));
            List<CinemaModel> li = model1.mas_c.ToList<CinemaModel>();
            for (int i = 0; i < w.Length - 1; i++)
            {
                q = li.FindIndex(x => x.id == Int32.Parse(w[i]));
                if (q != -1)
                    li.RemoveAt(q);

            }
            List<CinemaModel> li3 = new List<CinemaModel>();
            if (li1.Count >= 4)
                for (int i = 0; i < 4;i++ )
                    li3.Add(li1[i]);
            else
                for (int i = 0; i<li1.Count;i++)
                    li3.Add(li1[i]);
            if (li.Count >= 2)
                for (int i = 0; i < 2; i++)
                    li3.Add(li[i]);
            else
                for (int i = 0; i < li.Count; i++)
                    li3.Add(li[i]);
            ArticleModel mod = new ArticleModel(li3);
            return mod;
        }        
    }
}