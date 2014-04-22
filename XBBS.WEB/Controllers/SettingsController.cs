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

            if (Request.Files.Count > 0)
            { 
           
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public new ActionResult Profile()
        {

            return View(System.Web.HttpContext.Current.Session["User"] as Models.User);
        }

        [Authorize]
        [HttpPost]
        public new ActionResult Profile(string email,string homepage,string location,string qq,string signature,string introduction)
        {
            Models.User user = System.Web.HttpContext.Current.Session["User"] as Models.User;
            user.Email = email;
            user.Homepage = homepage;
            user.Location = location;
            user.QQ = qq;
            user.Signature = signature;
            user.Introduction = introduction;
            XBBS.DataProvider.AccountDataProvider.UpdateUser(user);
            System.Web.HttpContext.Current.Session["User"] = user;
            return View(System.Web.HttpContext.Current.Session["User"] as Models.User);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Password()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Password(string password, string newpassword, string newpassword2)
        {
            //ViewData["password"] = password;
            //ViewData["newpassword"] = newpassword;
            //ViewData["newpassword2"] = newpassword2;
            if (string.IsNullOrEmpty(newpassword))
            {
                ViewData.ModelState.AddModelError("error", "新密码不能为空!");
                return View();
            }
            if (newpassword != newpassword2)
            {
                ViewData.ModelState.AddModelError("error", "两次输入新密码不匹配");
                return View();
            }
            var account = XBBS.DataProvider.AccountDataProvider.GetUser(User.Identity.Name);
            if (account.Password != XBBS.Core.Utils.MD5(password))
            {
                ViewData.ModelState.AddModelError("error", "原始密码不对");
            }
            account.Password = XBBS.Core.Utils.MD5(newpassword);
            XBBS.DataProvider.AccountDataProvider.UpdateUser(account);
            ViewData["success"] = true;
            return View();
        }
    }
}
