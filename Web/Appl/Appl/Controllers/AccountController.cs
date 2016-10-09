using Appl.Models.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Appl.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            ViewBag.GetUserCredentials = "oukASeASeasrlaajkl112112asSaGsaGslaa77612dporqpg1rs12s9937761as3yhmat112aGsasrasrasdaGsASeaGs11212dporasd345xclOIP111.asLKLasS1pl//1112pkalaa12sxcllaahhh098pka12sxcl993GGG";
            ViewBag.ReturnUrl = returnUrl;

            return View(false);
        }
        
        // POST: /Account/Login
        [HttpPost]
        public ActionResult UserLogin(LoginViewModel model, string url)
        {
            if (model.Username == null || model.Password == null)
            {
                return Json(new 
                {
                    status = "fail"
                }, JsonRequestBehavior.AllowGet);
            }

            HttpCookie myCookie = new HttpCookie(Constant.COOKIE_NAME);
            myCookie["Username"] = model.Username;
            myCookie.Expires = DateTime.Now.AddDays(5d);
            Response.Cookies.Add(myCookie);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult UserLogoff()
        {
            if (Request.Cookies[Constant.COOKIE_NAME] != null)
            {
                HttpCookie myCookie = new HttpCookie(Constant.COOKIE_NAME);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}