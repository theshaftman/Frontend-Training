﻿using Appl.Models.BusinessLayer;
using Appl.Models.BusinessLayer.Account;
using Appl.Models.BusinessLayer.Data;
using Appl.Models.BusinessLayer.Structure;
using Appl.Models.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Appl.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        IData _data;

        public AccountController()
        {
            this._data = new Data();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View(false);
        }

        /// <summary>
        /// Log in form.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserLogin(FormCollection model)
        {
            string username = model["Username"];
            string password = model["Password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Json(new
                {
                    status = "fail"
                }, JsonRequestBehavior.AllowGet);
            }

            Result response = this._data.GetData("users", "/_count?query=%7B%22%24and%22%3A%5B%7B%22username%22%3A%22" + 
                username + "%22%2C%20%22password%22%3A%22" + password + "%22%7D%5D%7D");

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            object isFoundObject = jsonSerializer.DeserializeObject(response.Data);
            dynamic duff = JsonConvert.DeserializeObject(response.Data);
            string str = duff["count"].ToString();
            int number = int.Parse(str);

            bool isFound = number == 1 ? true : false;

            if (isFound)
            {
                System.Web.HttpCookie myCookie = new System.Web.HttpCookie(Constant.COOKIE_NAME);
                myCookie["Username"] = model["Username"].ToString();
                myCookie.Expires = DateTime.Now.AddDays(5d);
                Response.Cookies.Add(myCookie);
            }

            JsonResult result = Json(new
            {
                isFound = isFound
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        /// <summary>
        /// Log off
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserLogoff()
        {
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                System.Web.HttpCookie myCookie = new System.Web.HttpCookie(Constant.COOKIE_NAME);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}