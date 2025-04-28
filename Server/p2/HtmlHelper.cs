using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace p2
{
    public class HtmlHelper
    {
        public string[] HtmlTags { get; set; }
        public string[] HtmlVoidTags { get; set; }
        private static readonly HtmlHelper instance = new HtmlHelper();
        public static HtmlHelper Instance=>instance;
        // private static readonly HtmlHelper instance = new HtmlHelper();
        //public static HtmlHelper 
        private HtmlHelper()
        {
          
            var cleanHtml = new Regex("\\s").Replace(File.ReadAllText("HtmlTags.json"), "");
            HtmlTags = new Regex("'(.*?)'").Split(cleanHtml).Where(s=>s.Length>0 &&!s.Equals("[")&& !s.Equals("]") && !s.Equals(",")).ToArray();
             cleanHtml = new Regex("\\s").Replace(File.ReadAllText("HtmlVoidTags.json"), "");
            HtmlVoidTags = new Regex("'(.*?)'").Split(cleanHtml).Where(s => s.Length > 0 && !s.Equals("[") && !s.Equals("]") && !s.Equals(",")).ToArray();
             
        }
        public bool IsExsiseInHtmlTags(string s)
        {
            for (int i = 0; i < HtmlTags.Length; i++)
            {
                string f = HtmlTags[i];
                if (f.Equals(s))
                    return true;
            }
            return false;
        }
        public bool IsExsiseInHtmlVoidTags(string s)
        {
            for (int i = 0; i < HtmlVoidTags.Length; i++)
            {
                if (HtmlVoidTags[i].Equals(s))
                    return true;
            }
            return false;
        }
        public override string ToString()
        {
            string s = "HtmlTags:\n\n";
            foreach (var item in HtmlTags)
            {
                s += item +"\n";
            }
            s += "\nHtmlVoidTags:\n\n\n";
            foreach (var item in HtmlVoidTags)
            {
                s += item + "\n";
            }
            return s;
        }

    }
}
