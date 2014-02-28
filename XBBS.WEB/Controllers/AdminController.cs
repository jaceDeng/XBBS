using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XBBS.WEB.Controllers
{
    public class AdminController : RootController
    {
        //
        // GET: /Admin/

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
