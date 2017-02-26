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
    public class AccountController : Controller
    {
        private IAccountModel _model;

        public AccountController()
        {
            this._model = new AccountModel();
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection model)
        {
            Result result = this._model.LogIn(model);
            return Json(new
            {
                data = result.Data,
                status = result.Status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}