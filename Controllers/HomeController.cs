using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Ootan sind oma peole! Palun tule kindlasti!";
            ViewBag.Tittle = "Kutse kõik";
            int hour = DateTime.Now.Hour;
            /*int month = DateTime.Now.Month;
            if (month <=6)
            {
                ViewBag.Greeting = month < 10 ? "Tere hommikust!" : "Tere päevast";
            }
            else if (month >6)
            {

            }*/
            if (hour <= 15)
            {
                ViewBag.Greeting = hour < 10 ? "Tere hommikust!" : "Tere päevast";
            }
            else if(hour > 15)
            {
                ViewBag.Greeting = hour < 20 ? "Tere õhtu!" : "Tere öö";
            }
            //ViewBag.Greeting = hour < 10 ? "Tere hommikust!" : "Tere päevast";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Tittle = "Kutse kõik";

            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }

        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "programmeeriminetthk2@gmail.com";
                WebMail.Password = "2.kuursus tarpv20";
                WebMail.From = "programmeeriminetthk2@gmail.com";
                WebMail.Send("programmeeriminetthk2@gmail.com", "Vastus kutsele",guest.Name + " vastas " + ((guest.WillAttend ?? false) ?
                    "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahjul! Ei saa kirja saada!!!";
            }
        }


    }
}