using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XBBS.WEB.Controllers
{
    public class HomeController : RootController
    {



        public ActionResult Index()
        { 
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
            ViewBag.ForumList = XBBS.DataProvider.ForumDataProvider.GetForums(1);
            return View();
        }


        /// <summary>
        /// RSS订阅
        /// </summary>
        /// <returns></returns>
        public ActionResult Feed()
        {

            return View();
        }


        /// <summary>
        /// 节点列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Section()
        {
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
            ViewBag.ForumList = XBBS.DataProvider.ForumDataProvider.GetForums(1);        
            return View();
        }


        /// <summary>
        /// 会员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult User()
        {
            ViewData["Title"] = "用户";
            return View();
        }



        public ActionResult GetMore(int page)
        {
            var m = new
            {
                a = "123",
                b
                    = "1111"
            };

            return Json(m);

        }
    }
}
