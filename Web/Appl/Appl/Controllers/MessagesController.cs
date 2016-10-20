using Appl.Models.BusinessLayer;
using Appl.Models.BusinessLayer.Account;
using Appl.Models.BusinessLayer.Data;
using Appl.Models.BusinessLayer.Structure;
using Appl.Models.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appl.Controllers
{
    [AllowAnonymous]
    public class MessagesController : Controller
    {
        IData _data;

        public MessagesController()
        {
            this._data = new Data();
        }
        
        // GET: Messages
        public ActionResult Index()
        {
            bool isLogged = false;

            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                username = InputEncoding.DecodePassword(username);
                ViewBag.Username = username;

                isLogged = true;
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
            return View(isLogged);
        }

        [HttpGet]
        public ActionResult GetQuestionsCount()
        {
            Result response = this._data.GetData("questions", "/_count");
            
            JsonResult result = Json(new
            {
                status = response.Status,
                data = response.Data
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public JsonResult SendQuestion(FormCollection data)
        {
            string question = data["question"];

            if (question.Contains("<script"))
            {
                return null;
            }

            IRestResponse response = this._data.SendQuestion(data);
            string currentStatus = "success";

            if (response == null)
            {
                currentStatus = "fail";
            }

            JsonResult result = Json(new
            {
                status = currentStatus
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}