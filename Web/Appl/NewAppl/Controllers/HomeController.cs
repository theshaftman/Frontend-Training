using NewAppl.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAppl.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            bool isLogged = false;
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                string username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();
                ViewBag.Username = username;
                isLogged = true;
            }

            return View(isLogged);
        }
    }
}