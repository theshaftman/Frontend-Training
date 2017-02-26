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
        public ActionResult Index(string query, string category)
        {
            bool isLogged = false;
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                ViewBag.Username = username;
                isLogged = true;
            }

            this.ViewBag.Query = query;
            this.ViewData["Query"] = query;
            this.ViewBag.Category = category;
            this.ViewData["Category"] = category;

            return View(isLogged);
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
            bool isLogged = false;
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                ViewBag.Username = username;
                isLogged = true;
            }

            ViewBag.Id = id;
            ViewData["Id"] = id;

            return View(isLogged);
        }

        [HttpGet]
        public ActionResult GetComments(string recipeId)
        {
            string data = "?query={\"recipeId\":\"" + recipeId + "\"}&sort{\"_kmd\": { \"ect\": -1}}";
            Result comments = this._model.GetData("comments" + data);

            JsonResult result = Json(new
            {
                status = comments.Status,
                data = comments.Data
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public ActionResult PostComment(FormCollection model)
        {
            Result postComment = this._model.PostComment(model);
            string status = "issuePostComment";
            string data = string.Empty;

            if (postComment != null)
            {
                status = postComment.Status;
                data = postComment.Data;
            }

            JsonResult result = Json(new
            {
                status = status,
                data = data
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}