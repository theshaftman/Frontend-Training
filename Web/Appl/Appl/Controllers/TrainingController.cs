using Appl.Models.BusinessLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Appl.Controllers
{
    [AllowAnonymous]
    public class TrainingController : Controller
    {
        // GET: Training
        public ActionResult Index()
        {
            bool isLogged = false;

            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                ViewBag.Username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();

                isLogged = true;
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View(isLogged);
        }

        #region Get Data

        [HttpGet]
        public ActionResult GetData(string givenData, string query)
        {
            givenData = !string.IsNullOrEmpty(givenData) ? givenData : "subjects";
            query = !string.IsNullOrEmpty(query) ? query : "";

            var client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + givenData + query);
            var request = new RestRequest(Method.GET);
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

        #endregion

        #region Subjects

        [HttpPost]
        public JsonResult InsertSubjectData(FormCollection data)
        {
            string datatable = "subjects"; 
            string id = data["id"];
            string author = data["author"];
            string subject_title = data["subject_title"];
            string subject_body = data["subject_body"];

            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(subject_title) ||
                string.IsNullOrEmpty(subject_body))
            {
                return null;
            }

            subject_body = subject_body.Replace("&amp;", "&");
            subject_body = subject_body.Replace("&lt;", "<");
            subject_body = subject_body.Replace("&gt;", ">");

            if (subject_body.Contains("<script"))
            {
                return null;
            }

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + datatable);
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "c09c1448-cefb-9297-16ca-920e8a021437");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"id\"\r\n\r\n" + id + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\n" + author + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"subject_title\"\r\n\r\n" + subject_title + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"subject_body\"\r\n\r\n" + subject_body + "\r\n-----011000010111000001101001--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            JsonResult result = Json(new
            {
                status = "success"
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
        
        [HttpPost]
        public ActionResult DataDelete(FormCollection collection)
        {
            string givenData = collection["givenData"];
            string givenID = collection["id"];

            if (string.IsNullOrEmpty(givenData) ||
                string.IsNullOrEmpty(givenID))
            {
                return null;
            }

            var client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + givenData + "/" + givenID);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("postman-token", "c1111c9e-9a9a-28da-cd2a-2e03c1356bf8");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            // request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\nNan\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"subject_title\"\r\n\r\nNan\r\n-----011000010111000001101001--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            JsonResult result = Json(new
            {
                status = "success"
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        #endregion

        #region Comments

        [HttpPost]
        public JsonResult InsertCommentData(FormCollection data)
        {
            string datatable = "subjectComments";
            string id = data["id"];
            string subjectID = data["subject_id"];
            string author = data["author"];
            string comment = data["comment"];

            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(subjectID) ||
                string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(comment))
            {
                return null;
            }

            if (comment.Contains("<script"))
            {
                return null;
            }

            RestClient client = new RestClient("https://baas.kinvey.com/appdata/kid_rJ-gHb40/" + datatable);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk");
            request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"id\"\r\n\r\n" + id + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"subject_id\"\r\n\r\n" + subjectID + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\n" + author + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"comment\"\r\n\r\n" + comment + "\r\n-----011000010111000001101001--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            JsonResult result = Json(new
            {
                status = "success"
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        #endregion
    }
}