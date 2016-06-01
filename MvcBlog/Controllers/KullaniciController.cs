using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcBlog.Controllers
{
    public class KullaniciController : Controller
    {
        BlogModel context = new BlogModel();
        // GET: Kullanici
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(string kullaniciAdi,string Parola)
        {
            if(Membership.ValidateUser(kullaniciAdi,Parola))
            {
                FormsAuthentication.RedirectFromLoginPage(kullaniciAdi, true);

                Guid id = (Guid)Membership.GetUser(kullaniciAdi).ProviderUserKey;
                Session["Kullanici"] = context.Kullanici.FirstOrDefault(x => x.YazarId == id);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı veya Parola Yanlış...";
            }
            return View();

        }
        public ActionResult KayitOl()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult KayitOl(Kullanici kullanici,HttpPostedFileBase Resim,string Parola)
        {
            MembershipUser user = Membership.CreateUser(kullanici.Nick, Parola,kullanici.Mail);
            kullanici.YazarId = (Guid)user.ProviderUserKey;
            Session["Kullanici"] = kullanici;
            kullanici.ResimId = YonetimController.ResimKaydet(Resim,HttpContext);
            kullanici.KayitTarihi = DateTime.Now;
            context.Kullanici.Add(kullanici);
            context.SaveChanges();
            FormsAuthentication.RedirectFromLoginPage(kullanici.Nick, true);
            Session["Kullanici"] = kullanici;
            return RedirectToAction("Index", "Home");
        }
    }
}