using NewAppl.Models.BusinessLayer.Structure;
using NewAppl.Models.Data;
using NewAppl.Models.Interfaces;
using Newtonsoft.Json;
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

            dynamic duff = JsonConvert.DeserializeObject(result.Data);
            string username = duff["username"];
            string status = "issue";

            if (!string.IsNullOrEmpty(username))
            {
                status = result.Status;
                System.Web.HttpCookie myCookie = new System.Web.HttpCookie(Constant.COOKIE_NAME);
                myCookie["Username"] = username;
                myCookie.Expires = DateTime.Now.AddDays(5d);
                Response.Cookies.Add(myCookie);
            }

            return Json(new
            {
                data = result.Data,
                status = status
            }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult Logoff()
        {
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                System.Web.HttpCookie myCookie = new System.Web.HttpCookie(Constant.COOKIE_NAME);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Register(FormCollection model)
        {
            Result result = this._model.Register(model);

            return null;
        }
    }
}