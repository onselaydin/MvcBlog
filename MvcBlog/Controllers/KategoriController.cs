using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        BlogModel context = new BlogModel();
        public ActionResult Index(int id)
        {
            //var data = context.Makale.Where(x => x.KategoriId == id);

            return View(id);
        }

        public ActionResult MakaleListele(int id)
        {
            var data = context.Makale.Where(x => x.KategoriId == id);
            return View("MakaleListele", data);
        }
    }
}