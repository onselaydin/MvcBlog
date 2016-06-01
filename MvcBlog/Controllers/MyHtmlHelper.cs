using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    public static class MyHtmlHelper
    {
        public static string HarfleriBuyut(this HtmlHelper htmlHelper, string text)
        {
            return text.ToUpper();
        }
        public static string HarfleriKucult(this HtmlHelper htmlHelper, string text)
        {
            return text.ToLower();
        }
        public static string TextBox(this HtmlHelper htmlHelper, string name, bool buyultKucult)
        {
            if (buyultKucult == true)
                name = name.ToUpper();
            else
                name = name.ToLower();
            return name;
        }

        public static string Kisalt(this HtmlHelper htmlHelper, string text)
        {
            if (text.Length > 100)
            {
                text.Substring(0, Math.Min(text.Length, 100));
              
            }
            else
            {
                text.ToString();
            }
            return text;
        }

    }
}