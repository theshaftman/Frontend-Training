using Appl.Models.BusinessLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appl.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            bool isLogged = false;

            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                ViewBag.Username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();

                isLogged = true;
            }

            return View(isLogged);
        }
        
        [HttpGet]
        public ActionResult GetData()
        {
            List<int> list = new List<int>();
            list.Add(4);

            return Json(new
            {
                data = list
            }, JsonRequestBehavior.AllowGet);
        }
    }
}