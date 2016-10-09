using Appl.Models.BusinessLayer;
using Appl.Models.BusinessLayer.Account;
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
            var client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/questions/_count");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "c35818ee-840d-ed36-d282-b2cc1283fc2f");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            IRestResponse response = client.Execute(request);

            JsonResult result = Json(new
            {
                status = "success",
                data = response.Content
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public JsonResult SendQuestion(FormCollection data)
        {
            string id = data["id"];
            string author = data["author"];
            string question = data["question"];

            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(question))
            {
                return null;
            }

            if (question.Contains("<script"))
            {
                return null;
            }

            var client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/questions");
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "0b5fdfae-d6f3-b28e-be6c-d22de0916c21");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"id\"\r\n\r\n"
                + id + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\n" + author +
                "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"question\"\r\n\r\n" + question + 
                "\r\n-----011000010111000001101001--", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            JsonResult result = Json(new
            {
                status = "success"
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}