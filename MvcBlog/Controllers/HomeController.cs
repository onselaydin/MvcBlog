using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    public class HomeController : Controller
    {


        BlogModel context = new BlogModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult KategoriGetir()
        {
            return View(context.Kategori);
        }

        public ActionResult PostGetir() //yeni eklenen makaleler.
        {
            ViewBag.Fresh = context.Makale.OrderByDescending(x => x.YayimTarihi).Take(5);
            //ViewBag.Populer = context.Makale.OrderByDescending(x => x.Goruntulenme).Take(5);
            return View();
        }

        public ActionResult EtiketGetir()
        {
            var tags = context.Etiket.ToList();
            return View(tags);
        }


        public ActionResult TumMakaleleriGetir()
        {
            var makaleler = context.Makale.ToList();
            return View("MakaleListele", makaleler);
        }

        public JsonResult getSaying()
        {

            List<Saying> ListOfSay = context.Saying.SqlQuery("exec getsay", "").ToList();
            return Json(ListOfSay, JsonRequestBehavior.AllowGet);
        }
    }
}