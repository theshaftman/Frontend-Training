using Appl.Models.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            ViewBag.GetSubjectsData = "oukASeASeasrlaajkl112112asSaGsaGslaa77612dporqpg1rs12s9937761as3yhmat112aGsasrasrasdaGsASeaGs11212dporasd345xclOIP111.asLKLasS1pl//1112laapkaasSpek12s1asASelaahhh";
            ViewBag.SendSubject = "oukASeASeasrlaajkl112112asSaGsaGslaa77612dporqpg1rs12s9937761as3yhmat112aGsasrasrasdaGsASeaGs11212dporasd345xclOIP111.asLKLasS1pl//1112laapkaasSpek12s1asASelaa";

            return View(isLogged);
        }
    }
}