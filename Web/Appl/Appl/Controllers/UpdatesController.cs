using Appl.Models.BusinessLayer;
using Appl.Models.BusinessLayer.Account;
using Appl.Models.BusinessLayer.Data;
using Appl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Appl.Controllers
{
    public class UpdatesController : Controller
    {
        private IData _data;

        public UpdatesController()
        {
            this._data = new Data();
        }

        [HttpGet]
        public ActionResult GetUpdate(FormCollection data)
        {
            string currentStatus = "fail";
            string modificationID = null;

            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                username = InputEncoding.DecodePassword(username);
                Result currentData = this._data.GetData("userLastModificationID", "?query={\"userID\": \"" + username + "\"}");
                
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                dynamic routesList = jsonSerializer.DeserializeObject(currentData.Data);
                modificationID = routesList[0]["modificationID"].ToString();
                currentStatus = currentData.Status;
            }

            JsonResult result = Json(new
            {
                status = currentStatus,
                modificationID = modificationID
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public ActionResult Update(FormCollection data)
        {
            string currentStatus = "fail";

            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                currentStatus = "success";
            }

            JsonResult result = Json(new
            {
                status = currentStatus
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}