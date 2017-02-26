using NewAppl.Models.BusinessLayer.Structure;
using NewAppl.Models.Data;
using NewAppl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAppl.Controllers
{
    public class ContactController : Controller
    {
        IEmailModel _model;

        public ContactController()
        {
            this._model = new EmailModel();
        }

        // GET: Contact
        public ActionResult Index(string status)
        {
            bool isLogged = false;
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                ViewBag.Username = username;
                isLogged = true;
            }
            this.ViewBag.Status = status;

            return View(isLogged);
        }

        [HttpPost]
        public ActionResult SendMail(FormCollection model)
        {
            Result sendMail = this._model.SendMail(model);

            return RedirectToAction("Index", "Contact", new { status = sendMail.Status });
        }
    }
}