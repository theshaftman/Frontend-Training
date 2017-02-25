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
        public ActionResult Index(string query)
        {
            this.ViewBag.Query = query;
            this.ViewData["Query"] = query;

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
        public ActionResult GetRecipes(string query, string sort, string limit)
        {
            string addQuery = !string.IsNullOrEmpty(query) ? string.Format("?query={0}", query) : "";
            addQuery += !string.IsNullOrEmpty(sort) ? string.Format("{0}sort={1}", 
                (!string.IsNullOrEmpty(addQuery) ? "&" : "?"), sort) : "";
            addQuery += !string.IsNullOrEmpty(limit) ? string.Format("{0}limit={1}", 
                (!string.IsNullOrEmpty(addQuery) ? "&" : "?"), limit) : "";

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