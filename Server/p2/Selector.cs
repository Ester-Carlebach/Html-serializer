using p2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace p2
{
    public class Selector
    {
        //מאפינים
        public string TagName { get; set; } = null;
        public string Id { get; set; } = null;
        public List<string> Classes { get; set; } = null;
        public Selector Father { get; set; }
        public Selector Child { get; set; }

        //בנאי
        public Selector()
        {
            Classes = new List<string>();
        }
        //ToString
        public override string ToString()
        {
            string s = "name:" + TagName + "\nid:" + Id + "\nclasses:";
            foreach (var item in Classes)
            {
                s += item + ",";
            }
            return s;

        }
        //המרה ממחרוזת לעץ סלקטורים
        public static Selector convertToSelector(string query)
        {
            
            Selector selector = new Selector(), s = new Selector();
            string[] q = query.Split(" ");
            Selector root = null;
            for (int i = 0; i < q.Length; i++)
            {   
                //האוביקט שנוסף בכל איטרציה
                s = new Selector();
                //מערך הכלסים
                string[] cl;
                //ID מתחיל ב
                if (q[i].StartsWith("#"))
                {
                    //only id
                    if (!q[i].Contains("."))
                        s.Id = q[i].Substring(1);
                    //with class
                    else
                    {
                        s.Id = q[i].Substring(0, q[i].IndexOf("."));
                        q[i] = q[i].Substring(q[i].IndexOf(".") + 1);
                        cl = q[i].Split(".");
                        s.Classes.AddRange(cl);
                    }
                }
                //only classes
                else if (q[i].StartsWith("."))
                {
                    cl = q[i].Split(".");
                    s.Classes.AddRange(cl);
                }
                //start with tagName
                else
                {
                    //tag+id
                    if (q[i].IndexOf("#") != -1)
                    {
                        if (HtmlHelper.Instance.HtmlTags.Contains(q[i].Substring(0, q[i].IndexOf("#"))) ||
                            HtmlHelper.Instance.HtmlVoidTags.Contains(q[i].Substring(0, q[i].IndexOf("#"))))
                            s.TagName = q[i].Substring(0, q[i].IndexOf("#"));
                        q[i] = q[i].Substring(q[i].IndexOf("#") + 1);
                        //tag+id+classes
                        if (q[i].Contains("."))
                        {
                            s.Id = q[i].Substring(0, q[i].IndexOf("."));
                            q[i] = q[i].Substring(q[i].IndexOf(".") + 1);
                            cl = q[i].Split(".");
                            s.Classes.AddRange(cl);
                        }
                        else
                        {
                            s.Id = q[i];
                        }
                    }
                    else
                    {
                        //tag+classes
                        if (q[i].Contains("."))
                        {
                            if (HtmlHelper.Instance.HtmlTags.Contains(q[i].Substring(0, q[i].IndexOf("."))) ||
                           HtmlHelper.Instance.HtmlVoidTags.Contains(q[i].Substring(0, q[i].IndexOf("."))))
                                s.TagName = q[i].Substring(0, q[i].IndexOf("."));
                            q[i] = q[i].Substring(q[i].IndexOf(".") + 1);
                            cl = q[i].Split(".");
                            s.Classes.AddRange(cl);
                        }
                        //tag
                        else
                        {
                            if (HtmlHelper.Instance.HtmlTags.Contains(q[i]) ||
                          HtmlHelper.Instance.HtmlVoidTags.Contains(q[i]))
                                s.TagName = q[i];
                        }
                    }


                }
                if (root == null)
                {
                    root = s;
                    selector = root;

                }
                else
                {
                    s.Father = selector;
                    selector.Child = s;
                    selector = selector.Child;
                }
            }

            return root;
        }
    }
}
