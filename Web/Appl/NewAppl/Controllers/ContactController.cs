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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(FormCollection model)
        {
            Result sendMail = this._model.SendMail(model);
            JsonResult result = Json(new 
            {
                data = sendMail.Data,
                status = sendMail.Status
            }, JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}