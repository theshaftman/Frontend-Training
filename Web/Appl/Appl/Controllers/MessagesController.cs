using Appl.Models.BusinessLayer;
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
                ViewBag.Username = Request.Cookies[Constant.COOKIE_NAME]["Username"].ToString();

                isLogged = true;
            }
            else
            {
                return RedirectToAction("Login", "Account");                
            }

            ViewBag.GetQuestionsNumber = "oukASeASeasrlaajkl112112asSaGsaGslaa77612dporqpg1rs12s9937761as3yhmat112aGsasrasrasdaGsASeaGs11212dporasd345xclOIP111.asLKLasS1pl//1112098pka12slaaASepor3yhqpglaa1123451as3yhpkaqpgASe";
            ViewBag.PostQuestion = "oukASeASeasrlaajkl112112asSaGsaGslaa77612dporqpg1rs12s9937761as3yhmat112aGsasrasrasdaGsASeaGs11212dporasd345xclOIP111.asLKLasS1pl//1112098pka12slaaASepor3yhqpglaa";


            return View(isLogged);
        }
    }
}