using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    public class MakaleController : Controller
    {
        // GET: Makale
        BlogModel context = new BlogModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TariheGoreListele(int yil,int ay)
        {
            ViewBag.yil = yil;
            ViewBag.ay = ay;
            return View();
        }
        public ActionResult MakaleListele(int yil=0, int ay=0)
        {
            var data = context.Makale.Where(x => x.YayimTarihi.Value.Year == yil && x.YayimTarihi.Value.Month == ay);
            return View("MakaleListele", data);
        }

        public ActionResult Detay(int id)
        {

            //if(Session["Kullanici"]!=null)
            //{
                ViewBag.Kullanici = Session["Kullanici"];
            //}
            //else
            //{
            //    ViewBag.Kullanici = new Kullanici();
            //}

            Makale mk = context.Makale.FirstOrDefault(x => x.MakaleId == id);
            return View(mk);
        }

        [HttpPost]
        public ActionResult YorumYaz(Yorumlar yorum)
        {
            yorum.EklemeTarihi = DateTime.Now;          
            yorum.Baslik = "";
            yorum.Aktif = false;
            context.Yorumlar.Add(yorum);
            context.SaveChanges();
            return RedirectToAction("Detay", new { id = yorum.MakaleId });

        }

        public string Begen(int id)
        {
            Makale mak = context.Makale.FirstOrDefault(x => x.MakaleId == id);
            mak.Begeni++;
            context.SaveChanges();
            return mak.Begeni.ToString();
        }
    }
}