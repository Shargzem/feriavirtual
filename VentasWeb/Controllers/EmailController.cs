using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VentasWeb.Models;

namespace VentasWeb.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(EmailModel model)
        {
            using (MailMessage mm = new MailMessage(model.Email, model.To))
            {
                mm.Subject = model.Subjet;
                mm.Body = model.Body;

                if (model.Attachment.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(model.Attachment.FileName);
                    mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
                }

                mm.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    NetworkCredential cred = new NetworkCredential(model.Email, model.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = cred;
                    smtp.Port = 587;

                    smtp.Send(mm);

                    ViewBag.message = "Correo enviado correctamente";

                }

            }
            return View();
        }

    }
}