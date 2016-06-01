using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    using Models;
    using System.Data.Entity;

    [Authorize(Roles ="Admin")]
    [Authorize(Roles ="Yazar")]
    public class YonetimController : Controller
    {
        // GET: Yonetim
        BlogModel context = new BlogModel();
        public ActionResult Index()
        {
            ViewBag.Tip = 1;
            return View(context.Makale.ToList());
        }

        public ActionResult MakaleYaz()
        {
            ViewBag.Tip = 1;
            ViewBag.Kategoriler = context.Kategori.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult MakaleYaz(Makale makale,HttpPostedFileBase resim,string etiketler)
        {
            if(makale!=null)
            {
                Kullanici aktif = Session["Kullanici"] as Kullanici;
                makale.YayimTarihi = DateTime.Now;
                makale.MakaleTipiId = 1;
                makale.YazarId = aktif.YazarId;
                makale.KapakResimId = ResimKaydet(resim,HttpContext);
                context.Makale.Add(makale);
                context.SaveChanges();

                string[] etikets = etiketler.Split(',');
                foreach(string etiket in etikets)
                {
                    Etiket etk = context.Etiket.FirstOrDefault(x => x.Adi.ToLower() == etiket.ToLower().Trim());
                    if(etk==null)
                      {
                        //Etiket Yoksa
                        //Çoktan çoğa tablonun modeli oluşmamış (bkz makeleetiketleri tablosu)

                        etk = new Etiket();
                        etk.Adi = etiket;
                        context.Etiket.Add(etk);
                        context.SaveChanges();
                        makale.Etiket.Add(etk);
                        context.SaveChanges();
                    }
                    makale.Etiket.Add(etk);
                    context.SaveChanges();
                }

            }
            return RedirectToAction("Index"); //indexin içine git.
        }

        public static int? ResimKaydet(HttpPostedFileBase resim, HttpContextBase ctx)
        {
            BlogModel context = new BlogModel();
            

            int KucukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["kw"]);
            int KucukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["kh"]);
            int OrtaWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
            int OrtaHeight = Convert.ToInt32(ConfigurationManager.AppSettings["oh"]);
            int BuyukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
            int BuyukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["bh"]);

            string newName = Path.GetFileNameWithoutExtension(resim.FileName) +"_"+ Guid.NewGuid() + Path.GetExtension(resim.FileName);

            Image orjRes = Image.FromStream(resim.InputStream);
            Bitmap kucukRes = new Bitmap(orjRes, KucukWidth, KucukHeight);
            Bitmap ortaRes = new Bitmap(orjRes, OrtaWidth, OrtaHeight);
            Bitmap buyukRes = new Bitmap(orjRes);
            kucukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Kucuk/" + newName));
            ortaRes.Save(ctx.Server.MapPath("~/Content/Resimler/Orta/" + newName));
            buyukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Buyuk/" + newName));

            Kullanici k = (Kullanici)ctx.Session["Kullanici"];

            Resimler dbres = new Resimler();
            dbres.Adi = resim.FileName;
            dbres.BuyukResimYol = "/Content/Resimler/Buyuk/" + newName;
            dbres.OrtaResimYol = "/Content/Resimler/Orta/" + newName;
            dbres.KucukResimYol = "/Content/Resimler/Kucuk/" + newName;
            dbres.EklemeTarihi = DateTime.Now;
            dbres.EkleyenId = k.YazarId;
            context.Resimler.Add(dbres);
            context.SaveChanges();
            return dbres.Id;
        }

        public ActionResult Kategori()
        {
            ViewBag.Tip = 1;
            return View(context.Kategori.ToList());
        }
        public ActionResult KategoriEkle()
        {
            ViewBag.Tip = 1;
            return View();
        }
        [HttpPost]    
        public ActionResult KategoriEkle(Kategori kat)
        {
            context.Kategori.Add(kat);
            context.SaveChanges();
            return RedirectToAction("Kategori");
        }
        public ActionResult KategoriDuzelt(int id)
        {
            ViewBag.Tip = 1;
            return View(context.Kategori.FirstOrDefault(x => x.KategoriId==id));
        }

        [HttpPost]
        public ActionResult KategoriDuzelt(Kategori kat)
        {
            //if (ModelState.IsValid)
            //{
            context.Entry(kat).State = EntityState.Modified;
            context.SaveChanges();
            //}
            //context.Kategori.Attach(kat);
            //context.SaveChanges();

            return RedirectToAction("Kategori");
        }
       public ActionResult KategoriSil(int id)
        {
            context.Kategori.Remove(context.Kategori.FirstOrDefault(x => x.KategoriId == id));
            context.SaveChanges();
            return RedirectToAction("Kategori");
                
        }
    }
}