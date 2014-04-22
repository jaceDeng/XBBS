using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XBBS.WEB.Controllers
{
    public class RootController : Controller
    {


        public RootController()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in XBBS.DataProvider.CommonDataProvider.GetAllSetting())
            {
                if (!dic.ContainsKey(item.Title))
                {
                    dic.Add(item.Title, item.Value);
                }
            }


            if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.Session["User"] != null && System.Web.HttpContext.Current.Session["User"] is Models.User)
                {
                    ViewBag.User = System.Web.HttpContext.Current.Session["User"] as Models.User;
                    ViewData["GroupName"] = XBBS.DataProvider.AccountDataProvider.GetUserGroup(ViewBag.User.Gid).GroupName;
                }
                else
                {

                    System.Web.HttpContext.Current.Session["User"] = XBBS.DataProvider.AccountDataProvider.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
                    if (System.Web.HttpContext.Current.Session["User"] == null)
                    {
                        System.Web.Security.FormsAuthentication.SignOut();
                    }
                    else
                    {
                        ViewBag.User = System.Web.HttpContext.Current.Session["User"] as Models.User;
                        ViewData["GroupName"] = XBBS.DataProvider.AccountDataProvider.GetUserGroup(ViewBag.User.Gid).GroupName;
                         
                    } 
                }
            }
            ViewBag.Pages = XBBS.DataProvider.CommonDataProvider.GetAllPages();
            ViewBag.Setting = dic;
            ViewBag.Links = XBBS.DataProvider.CommonDataProvider.GetAllLinks();

        }
    }
}
