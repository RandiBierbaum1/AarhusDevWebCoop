using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using System.Web.Mvc;
using System.Net.Mail;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: Default
        public ActionResult Index ()
        {
            return PartialView("ContactForm", new ContactForm());
        }
        
        // GET: ContactFormSurface
        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {

          if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            TempData["success"] = true;

            MailMessage message = new MailMessage();
            message.To.Add("randibierbaum@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;


            
            //ensure there is no errors, otherwise display errors if there is

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("randibierbaum@gmail.com", "nxjhkegvwxunkvmo");

                // send mail
                smtp.Send(message);
                               
            }
            return RedirectToCurrentUmbracoPage();
        }
    }
}