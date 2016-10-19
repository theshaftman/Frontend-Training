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
        IUpdates _updates;

        public UpdatesController()
        {
            this._updates = new Updates();
        }

        [HttpGet]
        public ActionResult GetUpdates()
        {
            string currentStatus = "fail";
            IList<Update> modificationData = null;

            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                IList<Update> currentData = this._updates.GetCurrentUpdates(username);
                
                modificationData = currentData;
                currentStatus = "success";
            }

            JsonResult result = Json(new
            {
                status = currentStatus,
                modificationsData = modificationData
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public ActionResult Update(FormCollection data)
        {
            bool isUpdated = false;

            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();

                isUpdated = this._updates.UpdateCurrentData(data, username);
            }

            string currentStatus = isUpdated ? "success" : "fail";

            JsonResult result = Json(new
            {
                status = currentStatus
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}