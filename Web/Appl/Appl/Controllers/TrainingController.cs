using Appl.Models.BusinessLayer;
using Appl.Models.BusinessLayer.Account;
using Appl.Models.BusinessLayer.Data;
using Appl.Models.BusinessLayer.Structure;
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
        private IUpdates _updates;

        public TrainingController()
        {
            this._currentData = new Data();
            this._updates = new Updates();
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
            string author = data["author"];
            string subjectTitle = data["subject_title"];
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

            string method = editID != null ? "Modified" : "Created";
            string modification = string.Format("{0} \"{1}\"", method, subjectTitle);

            bool modify = this._updates.UpdateInsertData(author, modification, subjectTitle);

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

            // Update the modifications.
            string author = collection["author"];
            string subjectTitle = collection["deleteHeader"];
            string modification = string.Format("Deleted \"{0}\"", subjectTitle);

            if (string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(subjectTitle) ||
                string.IsNullOrEmpty(modification))
            {
                return Json(new
                {
                    status = "fail"
                }, JsonRequestBehavior.AllowGet);
            }

            bool modify = this._updates.UpdateInsertData(author, modification, subjectTitle);

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
            string comment = data["comment"];

            if (comment.Contains("<script") ||
                comment.Contains("&lt;script"))
            {
                return null;
            }

            IRestResponse response = this._currentData.ModifyComment(null, data);
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

        #endregion
    }
}