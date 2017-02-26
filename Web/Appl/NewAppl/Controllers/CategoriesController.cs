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
    public class CategoriesController : Controller
    {
        private IDatabaseModel _model;

        public CategoriesController()
        {
            this._model = new DatabaseModel();
        }

        // GET: Categories
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
        public ActionResult GetCategoryGroups()
        {
            Result currentData = this._model.GetData("categoryGroupName?sort{\"groupId\": 1}");

            return Json(new
            {
                data = currentData.Data,
                status = currentData.Status
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            Result currentData = this._model.GetData("categories?sort{\"categoryId\": 1}");

            return Json(new
            {
                data = currentData.Data,
                status = currentData.Status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}