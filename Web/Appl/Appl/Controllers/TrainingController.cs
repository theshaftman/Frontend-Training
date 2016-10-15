using Appl.Models.BusinessLayer;
using Appl.Models.BusinessLayer.Account;
using Appl.Models.BusinessLayer.Data;
using Appl.Models.Interfaces;
using Newtonsoft.Json.Linq;
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
        private IData _currentData;

        public TrainingController()
        {
            this._currentData = new Data();
        }

        // GET: Training
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

        #region Get Data

        [HttpGet]
        public ActionResult GetData(string givenData, string query)
        {
            Result getData = this._currentData.GetData(givenData, query);
            JsonResult result = Json(new
            {
                status = getData.Status,
                data = getData.Data
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        #endregion

        #region Subjects
        
        [HttpPost]
        public ActionResult ModifySubjectData(FormCollection data)
        {
            string editID = data["editID"];
            string subjectBody = data["subject_body"];

            if (subjectBody.Contains("<script") ||
                subjectBody.Contains("&lt;script"))
            {
                return Json(new
                {
                    status = "fail"
                }, JsonRequestBehavior.AllowGet);
            }

            IRestResponse response = this._currentData.ModifySubject(editID, data);

            string currentStatus = "success";
            if (response == null)
            {
                currentStatus = "fail";
            }

            return Json(new 
            {
                status = currentStatus
            }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult DataDelete(FormCollection collection)
        {
            string givenData = collection["givenData"];
            string givenID = collection["id"];
            string currentID = collection["currentId"];

            if (string.IsNullOrEmpty(givenData) ||
                string.IsNullOrEmpty(givenID))
            {
                return null;
            }

            IRestResponse response = this._currentData.Delete(givenData, givenID);

            Result subjectComments = this._currentData.GetData("subjectComments", 
                "?query={\"subject_id\": \"" + currentID + "\"}");

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic routesList = jsonSerializer.DeserializeObject(subjectComments.Data);

            List<string> list = new List<string>();

            // Delete the comments
            for (int i = 0; i < routesList.Length; i++)
            {
                list.Add(routesList[i]["_id"].ToString());
            }

            for (int i = 0; i < list.Count; i++)
            {
                var item = this._currentData.Delete("subjectComments", list[i]);
            }

            JsonResult result = Json(new
            {
                status = subjectComments.Status,
                data = subjectComments.Data
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
            request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"id\"\r\n\r\n" + 
                id + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"subject_id\"\r\n\r\n" + 
                subjectID + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"author\"\r\n\r\n" + 
                author + "\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"comment\"\r\n\r\n" + 
                comment + "\r\n-----011000010111000001101001--", ParameterType.RequestBody);

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