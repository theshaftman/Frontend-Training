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
    public class RecipesController : Controller
    {
        private IDatabaseModel _model;

        public RecipesController()
        {
            this._model = new DatabaseModel();
        }

        // GET: Recipes
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            Result currentData = this._model.GetData("categories");

            JsonResult result = Json(new 
            {
                status = currentData.Status,
                data = currentData.Data
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpGet]
        public ActionResult GetRecipes(string query)
        {
            string addQuery = !string.IsNullOrEmpty(query) ? "?" + query : "";
            Result currentData = this._model.GetData("recipes" + addQuery);

            JsonResult result = Json(new
            {
                status = currentData.Status,
                data = currentData.Data
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        public ActionResult Recipe(string id)
        {
            ViewBag.Id = id;
            ViewData["Id"] = id;

            return View();
        }
    }
}