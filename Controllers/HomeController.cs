using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            string[] peod = new string[12]{"Jaanuari pidu","Veebruari pidu", "Märts pidu", "Aprill pidu", "Mai pidu", "Juuni pidu", "Juuli pidu", "August pidu", "September pidu", "Oktoober pidu", "November pidu", "Detsember pidu" };
            int hour = DateTime.Now.Hour;
            int month = DateTime.Now.Month;

            /*string prazdnik = "";
            if (DateTime.Now.Month==1){prazdnik = "Jaanuari pidu";}
            else if (DateTime.Now.Month == 2){prazdnik = "Veebruari pidu";}
            else if (DateTime.Now.Month == 3){prazdnik = "Märts pidu";}
            else if (DateTime.Now.Month == 4){prazdnik = "Aprill pidu";}
            else if (DateTime.Now.Month == 5){prazdnik = "Mai pidu";}
            else if (DateTime.Now.Month == 6){prazdnik = "Juuni pidu";}
            else if (DateTime.Now.Month == 7){prazdnik = "Juuli pidu";}
            else if (DateTime.Now.Month == 8){prazdnik = "August pidu";}
            else if (DateTime.Now.Month == 9){prazdnik = "September pidu";}
            else if (DateTime.Now.Month == 10){ prazdnik = "Oktoober pidu";}
            else if (DateTime.Now.Month == 11){prazdnik = "November pidu";}
            else if (DateTime.Now.Month == 12){prazdnik = "Detsember pidu";}*/
   

            ViewBag.Message = "Ootan sind oma peole! " + peod[month-1]  + ". Palun tule kindlasti!";

            if (hour <= 16)
            {
                ViewBag.Greeting = hour < 10 ? "Tere hommikust!" : "Tere päevast";
            }
            else if(hour > 16)
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
            E_mailGuest(guest);
            if (ModelState.IsValid)
            {
                ViewBag.Greeting = guest.Email;
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

        MailMessage message = new MailMessage();
        public void E_mailGuest(Guest guest)
        {
            string komu = guest.Email;
            message.To.Add(new MailAddress(komu));
            message.From = new MailAddress(komu);
            message.Subject = "Ostetud piletid";
            message.Body = guest.Name + "Ne zabud pridit v 01.05.2022";
            string email = "programmeeriminetthk2@gmail.com";
            string password = "2.kuursus tarpv20";
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true,
            };
            /*try
            {
                client.Send(message);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {}*/
        }

        GuestContext db = new GuestContext();
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }

    }
}