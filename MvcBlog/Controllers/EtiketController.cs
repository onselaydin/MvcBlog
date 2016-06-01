using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcBlog.Controllers
{
    public class EtiketController : Controller
    {
        // GET: Etiket
        BlogModel context = new BlogModel();
        public ActionResult Index( int id)
        {
            return View();
        }

        public ActionResult MakaleListele(int id)
        {
            var data = context.Makale.Where(x => x.Etiket.Any(me => me.EtiketId == id));
            return View("MakaleListele",data);

            //yukarıdakinin sql karşılığı gibi.
            //select * from makale where Id in (select makaleid from MakaleEtiket where EtiketId=3)

        }
    }
}