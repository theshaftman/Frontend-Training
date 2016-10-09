using Appl.Models.BusinessLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (model["Username"] == null || model["Password"] == null)
            {
                return Json(new
                {
                    status = "fail"
                }, JsonRequestBehavior.AllowGet);
            }

            var client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/users/_count?query=%7B%22%24and%22%3A%5B%7B%22username%22%3A%22"
                + model["Username"].ToString() + "%22%2C%20%22password%22%3A%22" + model["Password"].ToString() + "%22%7D%5D%7D");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "620ae36d-3492-1aad-e4c7-7ea71dd53bdb");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            IRestResponse response = client.Execute(request);

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            object isFoundObject = jsonSerializer.DeserializeObject(response.Content);
            dynamic duff = JsonConvert.DeserializeObject(response.Content);
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