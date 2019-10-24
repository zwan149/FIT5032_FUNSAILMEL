using FIT5032_FUNSAILMEL.Models;
using FIT5032_FUNSAILMEL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_FUNSAILMEL.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private FUNSAILMEL_Model1Container db = new FUNSAILMEL_Model1Container();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "What is FunSailMel";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us if you have any question";

            return View();
        }

        public ActionResult Check_Map()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send_Email(SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    List<String> toEmails = toEmail.Split(';').ToList<String>();

                    foreach (String emailaddress in toEmails)
                    {
                        EmailSender es = new EmailSender();
                        es.Send(emailaddress.Trim(), subject, contents, postedFile);
                    }

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Statistics()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GetChart()
        {
            return Json(db.Boats.Select(p => new { p.Colour, p.Capacity }),
                    JsonRequestBehavior.AllowGet);
        }
    }
}