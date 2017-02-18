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
            return View();
        }

        [HttpGet]
        public ActionResult GetInformation()
        {
            Result data = this._model.GetData("myInformation");
            JsonResult result = Json(new
            {
                data = data.Data,
                status = data.Status
            }, JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}