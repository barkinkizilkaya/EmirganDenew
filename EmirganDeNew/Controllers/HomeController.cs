using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmirganDeNew.Models;
using System.Net.Mail;
using System.Net;

namespace EmirganDeNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MailModel model = new MailModel();
            return View(model);
        }


        public IActionResult Datenschutz()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] MailModel model)
        {
            if (ModelState.IsValid)
            {
                using (MailMessage mm = new MailMessage("website@emirgan.de", "Vertrieb@emirgan.de"))
                {
                    mm.Subject = model.Topic;
                    mm.Body = "<!DOCTYPE html><html><head><style>h1 {text-align: center;}p {text-align: center;}div {text-align: center;}</style></head><body><h1>Mesajınız Var!</h1><p>" + model.Message + "</p><div>" + model.Email + "</div><div>"+model.TelephoneNumber+"</div></body></html>";

                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {

                        smtp.Host = "smtp.ionos.de";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("website@emirgan.de", "fgrewAt5wzgtH$%");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        ViewBag.Message = "Nachricht gesendet.";
                    }
                }


                ModelState.Clear();
            }

            return View();
        }

    }
}
