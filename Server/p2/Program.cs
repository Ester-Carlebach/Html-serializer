
using System.Runtime.Intrinsics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using p2;

//קריאת הקוד
//var html = await Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/#_3");
var html = await Load("https://malkabruk.co.il/learn");

//ביטוי רגולרי המחליף את הרווחים בדולר

var cleanHtml = new Regex("\\s").Replace(html, "$");

//var cleanHtml1 = new Regex("\"").Replace(cleanHtml, "'");

int countD(string s)
{
    int count = 0;
    for (int i = 0; i < s.Length; i++)
    {
        if (s[i] == '$')
            count++;
    }
    return count;
}
//מערך של שורות הקוד

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0 && s.Count() != countD(s));

var htmlElement = "<div id=\"my-id\" class=\"my-class-1 my-class-2\" width=\"100%\">text</div>";

Console.WriteLine();
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
Console.WriteLine();
HtmlElement root = null;
HtmlElement element = new HtmlElement();
List<HtmlElement> ll = new List<HtmlElement>();
List<string> l1 = new List<string>();
List<string> l2 = new List<string>();
foreach (var item in htmlLines)
{
    //התעלמות מהשורה הראשונה
    if (item != "!doctype$html")
    {
        // '/' - אם התגית בתחילה ב
        //סימן שמדובר בתגית סוגרת שהיא האבא של האלמנט הנוכחי
        if (item[0] == '/')
        {
            element = element.Parent;
        }
        //יצירת אוביקט חדש והוספתו לילדים של האוביקט הנוכחי HTML אחרת אם היא שם של תגיות ה-
        else
        {
            //משתנים לבדיקה אם המילה הראשונה היא תגית
            bool t = false, vt = false;
            //משתנה המכיל את שם התגית
            string e = item;
            if (item.IndexOf("$") >= 0)
                e = item.Substring(0, item.IndexOf("$"));
            //מעבר על התגיות 
            foreach (var j in HtmlHelper.Instance.HtmlTags)
            {
                if (j.Equals(e))
                {
                    t = true;
                    break;

                }

            }
            if (!t)
            {
                foreach (var j in HtmlHelper.Instance.HtmlVoidTags)
                {
                    if (j.Equals(e))
                    {
                        vt = true;
                        break;
                    }

                }
            }
            //האוביקט החדש
            HtmlElement h1 = new HtmlElement(e);
            //אם המילה הראשונה היא תגית
            if (t || vt)
            {

                var r1 = new Regex("(.*?)=\"(.*?)\"").Matches(item).ToList();
                List<string> ls = new List<string>();
                string word = "";
                Dictionary<string, string> d = new Dictionary<string, string>();
                for (int x = 0; x < r1.Count; x++)
                {

                    word = r1[x].ToString().Replace("$", " ");

                    if (word.Substring(0, word.IndexOf("=")).Contains(" "))
                        word = word.Substring(word.IndexOf(" ") + 1);

                    string key = word.Substring(0, word.IndexOf("=")), val = word.Substring(word.IndexOf("=") +2);
                    //if (key != "id")
                    val = val.Substring(0, val.Length - 1);
                    if (key.Equals("class"))
                    {
                        if (val.Contains(" "))
                            foreach (string o in val.Split(" "))
                            {
                                h1.Classes.Add(o);
                            }
                        else
                            h1.Classes.Add(val);
                    }
                    else if (key.Equals("id"))
                        h1.Id = val;
                    else
                        d[key] = val;
                    ls.Add(word);
                }
                h1.Attributes = d;
                if (root == null)
                {
                    root = h1;
                    element = h1;
                }
                else
                {
                    h1.Parent = element;
                    element.Children.Add(h1);
                    if (t)
                    {
                        element = h1;
                    }
                }
                ll.Add(element);

            }
            else
            {
                element.InnerHtml = item;
            }
        }
    }
}

//Console.WriteLine(root);

HashSet<HtmlElement> list = new HashSet<HtmlElement>();
//var g = Selector.convertToSelector("div h1 span");
var g = Selector.convertToSelector("div div div div");
list = root.findBySelector(root, null,g
    );
//var d1 = HtmlElement.Search(root, g,list.ToList());
//onsole.WriteLine(d1.Count());
list.ToList().ForEach(x => Console.WriteLine(x));
Console.WriteLine(list.Count);
