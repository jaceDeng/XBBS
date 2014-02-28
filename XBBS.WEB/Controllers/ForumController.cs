using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XBBS.WEB.Controllers
{
    public class ForumController : RootController
    {
        //
        // GET: /Forum/
        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            ViewData["Title"] = "创建";
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
            ViewBag.NodeId = id;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Add(string title, int cid, string content)
        {
            ViewData["Title"] = "创建";
            ViewBag.Category = DataProvider.ForumDataProvider.GetAllCategory();
            ViewBag.NodeId = cid;
            if (ModelState.IsValid)
            {
                Models.User user = Session["User"] as Models.User;
                XBBS.Models.Forums forum = new Models.Forums();
                forum.AddTime = DateTime.Now;
                forum.Cid = cid;
                forum.Uid = user.Uid;
                forum.Content = content;
                forum.Title = title;
                forum.Comments = 0;
                forum.Views = 0;
                forum.UpdateTime = DateTime.Now;
                bool b = DataProvider.ForumDataProvider.AddForum(forum);
                if (b)
                {
                    return Redirect("/");
                }
            }

            return View();
        }


        public ActionResult Topic(int id)
        {
            ViewData["Title"] = "查看标题";
            return View();
        
        }
    }
}
