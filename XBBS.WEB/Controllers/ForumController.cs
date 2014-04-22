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
        [Authorize]
        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            ViewData["Title"] = "创建";
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
            ViewBag.NodeId = id;
            return View();
        }


        [Authorize]
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
                int b = DataProvider.ForumDataProvider.AddForum(forum);
                if (b > 0)
                {
                    DataProvider.ForumDataProvider.DeleteTarg(b);
                    string[] strsKey = null;
                    if (string.IsNullOrEmpty(Request.Form["keywords"]))
                    {
                        List<string> list = new List<string>();
                        foreach (var item in XBBS.DataProvider.CommonDataProvider.GetAllTag())
                        {
                            if (forum.Content.IndexOf(item) >= 0)
                            {
                                list.Add(item);
                            }
                        }
                        strsKey = list.ToArray();
                    }
                    else
                    {
                        strsKey = Request.Form["keywords"].Split(',', ' ');
                    }
                    foreach (var item in strsKey)
                    {
                        DataProvider.ForumDataProvider.AddTarg(item.Trim(), b);
                    }
                    return Redirect("/");
                }
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            XBBS.Models.Forums forum = XBBS.DataProvider.ForumDataProvider.GetForum(id);
            if (forum == null)
            {
                return new RedirectResult("/");
            }
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
            ViewData["Title"] = forum.Title;
            return View(forum);
        }

        [ValidateInput(false)]
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, string title, string content, int cid)
        {
            XBBS.Models.Forums forum = XBBS.DataProvider.ForumDataProvider.GetForum(id);
            if (forum == null)
            {
                return new RedirectResult("/");
            }
            forum.Cid = cid;
            forum.Title = title;
            forum.Content = content;
            bool b = XBBS.DataProvider.ForumDataProvider.UpdateForum(forum);
            if (!b)
            {
                ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetAllCategory();
                ViewData["Title"] = forum.Title;
                ModelState.AddModelError("状态", "添加失败");
                return View(forum);
            }
            else
            {
                DataProvider.ForumDataProvider.DeleteTarg(id);
                string[] strsKey = null;
                if (string.IsNullOrEmpty(Request.Form["keywords"]))
                {
                    List<string> list = new List<string>();
                    foreach (var item in XBBS.DataProvider.CommonDataProvider.GetAllTag())
                    {
                        if (forum.Content.IndexOf(item) >= 0)
                        {
                            list.Add(item);
                        }
                    }
                    strsKey = list.ToArray();
                }
                else
                {
                    strsKey = Request.Form["keywords"].Split(',', ' ');
                }
                foreach (var item in strsKey)
                {
                    DataProvider.ForumDataProvider.AddTarg(item.Trim(), id);
                }
                return Redirect("/topic/" + id.ToString());
            }
        }

        public ActionResult Topic(int id, int page = 1)
        {
            XBBS.Models.Forums forum = XBBS.DataProvider.ForumDataProvider.GetForum(id);
            if (forum == null)
            {
                return new RedirectResult("/");
            }
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetCategory(forum.Cid);
            int total = 0;
            ViewBag.Comments = XBBS.DataProvider.ForumDataProvider.GetComments(id, 10, page, ref total);// new List<XBBS.Models.Comment>();
            ViewBag.PageIndex = page;
            ViewBag.PageCount = total;
            ViewData["Title"] = forum.Title;
            //shengcheng fenye 
            ViewData["cname"] = ViewBag.Category.CName;
            ViewData["content"] = ViewBag.Category.Content;
            ViewData["cid"] = forum.Cid;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 1; i <= total; i++)
            {
                if (i > 1)
                {
                    sb.Append("<a href=\"/topic/");
                    sb.Append(id);
                    sb.Append("/1\">←</a>");
                }

                if (i == page)
                {
                    sb.Append(" <li class=\"active\"><span>");
                    sb.Append(i);
                    sb.Append("</span></li>");
                }
                else
                {
                    sb.Append("<li><a href=\"/topic/");
                    sb.Append(id);
                    sb.Append("/");
                    sb.Append(page);
                    sb.Append(">");
                    sb.Append(i);
                    sb.Append("</a></li>");

                }

                if (i < total)
                {
                    sb.Append("<a href=\"/topic/");
                    sb.Append(id);
                    sb.Append("/");
                    sb.Append(i);
                    sb.Append("\">→</a>");

                }
                ViewData["Pagination"] = sb.ToString();
            }

            ViewBag.ForumUser = XBBS.DataProvider.AccountDataProvider.GetUser(forum.Uid);
            return View(forum);

        }


        public ActionResult Tag(string title)
        {
            ViewBag.ForumList = XBBS.DataProvider.ForumDataProvider.GetForumsTags(title);
            ViewData["TagTitle"] = title;
            return View();

        }

        public ActionResult Node(int id)
        {
            ViewBag.Category = XBBS.DataProvider.ForumDataProvider.GetCategory(id);
            ViewBag.ForumList = XBBS.DataProvider.ForumDataProvider.GetForums(id);
            return View();
        }

        [Authorize]
        public ActionResult Del(int id, int category, int uid)
        {
            var d = XBBS.DataProvider.ForumDataProvider.GetForum(id);
            if (d != null)
            {
                Models.User user = ViewBag.User as Models.User;
                if (d.Uid == user.Uid)
                {
                    XBBS.DataProvider.ForumDataProvider.DeleteForum(d.FId);
                    ViewData["success"] = true;
                    return Redirect("/node/" + d.Cid.ToString());
                }
            }
            return JavaScript("alert('无权删除!') ; window.location.href='/'; ");
        }



        [Authorize]
        public ActionResult CommentAdd(string comment, int fid, int is_top, string username, string avatar)
        {
            Models.Comment com = new Models.Comment();
            com.Content = comment;
            com.UId = ViewBag.User.Uid;
            com.FId = fid;
            com.Replytime = DateTime.Now;
            XBBS.DataProvider.ForumDataProvider.AddComment(com);
            var js = new
            {
                content = comment,
                fid = fid,
                uid = com.UId,
                replytime = DateTime.Now,
                username = username,
                avatar = avatar,
                layer = XBBS.DataProvider.ForumDataProvider.TotalComment(fid)
            };
            return Json(js);
        }
    }
}
