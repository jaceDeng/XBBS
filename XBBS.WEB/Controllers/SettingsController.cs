using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XBBS.WEB.Controllers
{
    public class SettingsController : RootController
    {
        //
        // GET: /Settings/

        [Authorize]
        public ActionResult Avatar()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {

            return View(System.Web.HttpContext.Current.Session["User"] as Models.User);
        }

    }
}
