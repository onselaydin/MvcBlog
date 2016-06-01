using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        BlogModel context = new BlogModel();
        public ActionResult Index(Guid id)
        {
            return View(id);

        }

        public ActionResult MakaleListele(Guid id)
        {
            var data = context.Makale.Where(x => x.YazarId == id);
            return View("MakaleListele", data);

        }
    }
}