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
    public class AboutController : Controller
    {
        private IDatabaseModel _model;

        public AboutController()
        {
            this._model = new DatabaseModel();
        }

        // GET: About
        public ActionResult Index()
        {
            bool isLogged = false;
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                ViewBag.Username = username;
                isLogged = true;
            }

            return View(isLogged);
        }

        [HttpGet]
        public ActionResult GetInformation()
        {
            Result data = this._model.GetData("information");
            JsonResult result = Json(new
            {
                data = data.Data,
                status = data.Status
            }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpGet]
        public ActionResult GetFiles(string query)
        {
            string addQuery = !string.IsNullOrEmpty(query) ? "?query={\"usage\":\"" + query + "\"}" : "";

            Result data = this._model.GetFiles(addQuery);
            JsonResult result = Json(new
            {
                data = data.Data,
                status = data.Status
            }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpGet]
        public ActionResult GetConstants(string query)
        {
            string addQuery = !string.IsNullOrEmpty(query) ? "constants?query={\"action\":\"" + query + "\"}" : "constants";

            Result data = this._model.GetData(addQuery);
            JsonResult result = Json(new
            {
                data = data.Data,
                status = data.Status
            }, JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}