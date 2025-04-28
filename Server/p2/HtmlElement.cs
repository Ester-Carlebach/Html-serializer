using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement()
        {
            //Parent = new HtmlElement();
            Attributes = new Dictionary<string, string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }
        public HtmlElement(string name) : this()
        {

            Name = name;
        }
        public override string ToString()
        {
            string s = "HtmlElement:[";
            s += "id: " + Id + " name: " + Name;
            if (Attributes != null)
                s += " Atributes:";

            foreach (var item in Attributes)
            {
                s += item + ",";
            }
            //if (Classes != null)
            //    s += " Classes:";
            //foreach (var item in Classes)
            //{
            //    s += item + ",";
            //}
            //if (Parent != null)
            //    s += "   parent:" + Parent.Name;
           // if (Children.Count > 0)
             //   s += " children: \n";
            //for (int item = 0; item < Children.Count; item++)
           // {
            //    s += (item + 1) + ":" + Children[item] + "\n";
           // }
            //if (InnerHtml != null)
            //    s += "InnerHtml:" + InnerHtml;
        //    s += "]\n";
            return s;
        }
        //-----------------------------------פונקציות ב-HtmlElement-----------------------------------
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                if (queue != null)
                    if (queue.Peek().Children != null)
                        foreach (var item in queue.Peek().Children)
                        {
                            queue.Enqueue(item);
                        }
                yield return queue.Peek();
                queue.Dequeue();
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement element = this;
            while (element != null)
            {
                yield return element;
                element = element.Parent;
            }


        }

        public HashSet<HtmlElement> findBySelector(HtmlElement element, HashSet<HtmlElement> list, Selector selector)
        {
            if (selector == null)
                return list;
            List<HtmlElement> ll = new List<HtmlElement>();

            if (list == null)
                ll = element.Descendants().ToList();
            
            else
            {
                for (int i = 0; i < list.ToList().Count; i++)
                {

                    ll.AddRange(list.ToList()[i].Descendants());
                }
            }
            Console.WriteLine(ll.Count);
           HashSet<HtmlElement> temp = new HashSet<HtmlElement>();
            foreach (HtmlElement item in ll)
            {
                bool f = false;

                if (((selector.Id != null && item.Id == selector.Id) || selector.Id == null) && (selector.TagName == null || item.Name == selector.TagName))
                {
                    f = true;
                }
               
                if (f)
                {

                    int count = 0;
                    foreach (var item1 in selector.Classes)
                    {
                        foreach (var item2 in item.Classes)
                        {
                            if (item2.Contains(item1))
                                count++;
                        }
                    }
                    if (count != selector.Classes.Count)
                        f = false;
                    else if (count > 0)
                        f = true;

                }

              
                if (f)
                {
                    temp.Add(item);

                }


            }
           
            return findBySelector(element,temp, selector.Child);
        }
        public static List<HtmlElement> Search(HtmlElement element, Selector selector, List<HtmlElement> l2)
        {
            //if (selector == null)
            //    l2.Add(element);
            var list5 = element.Descendants();

            foreach (var item in list5)
            {
                Boolean fleg = false;
                if ((selector.Id != null && item.Id == selector.Id))
                {
                    fleg = true;
                }
                if (selector.TagName != null && item.Name == selector.TagName)
                {
                    fleg = true;
                }

                if (item.Classes != null)
                {
                    foreach (var i in selector.Classes)
                    {
                        if (selector.Classes.Contains(i))
                        {
                            fleg = true;
                        }
                        else
                        {
                            fleg = false;
                            break;
                        }
                    }
                }
                if (fleg)
                {
                    if (selector.Child == null)
                        l2.Add(item);
                    else
                        Search(item, selector.Child, l2);
                }

            }
            return l2;
        }

    }




}

