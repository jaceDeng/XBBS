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
        const int PER = 17;


        public ActionResult Index()
        {
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
            ViewBag.ForumList = XBBS.DataProvider.ForumDataProvider.GetLastForums(PER);
            ViewBag.Targs = XBBS.DataProvider.ForumDataProvider.GetAllTarg();
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
            ViewData["TotalFourms"] = XBBS.DataProvider.ForumDataProvider.TotalForum();
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
            ViewBag.ForumList = XBBS.DataProvider.ForumDataProvider.GetLastForums(PER);
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



        public ActionResult GetMore(int id)
        {
            ViewBag.ForumList = XBBS.DataProvider.ForumDataProvider.GetLastForums(PER, PER * (id - 1));
            return View();
        }
    }
}
