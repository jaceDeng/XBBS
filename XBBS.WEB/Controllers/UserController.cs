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
    public class UserController : RootController
    {
        //
        // GET: /User/
        [HttpGet]
        public ActionResult Reg()
        {

            ViewData["Title"] = "注册新用户";
            return View();
        }

        [HttpPost]
        public ActionResult Reg(XBBS.Models.User user)
        {
            if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            ViewData["UserName"] = user.UserName;
            ViewData["Password"] = user.Password;
            if (ViewBag.Setting["show_captcha"] == "on" && (string.IsNullOrEmpty(Request.Form["captcha_code"]) || Session["ValidateCode"] == null || Request.Form["captcha_code"].ToString().ToUpper() != Session["ValidateCode"].ToString().ToUpper()))
            {
                ModelState.AddModelError("captcha_code", "验证码错误");
                return View();
            }
            if (ModelState.IsValid)
            {
                user.Openid = Request["openid"] == null ? "" : Request["openid"];
                user.IP = Request.UserHostAddress.ToString();
                user.GroupType = 2;
                user.Gid = 3;
                user.Regtime = DateTime.Now;
                user.IsActive = 1;
                bool b = XBBS.DataProvider.AccountDataProvider.CreatUser(user);
                if (!b)
                {
                    ModelState.AddModelError("", "创建失败!");
                    return View();
                }
                else
                {
                    //登录成功
                    System.Web.Security.FormsAuthentication.SetAuthCookie(user.UserName, false);
                    string returnUrl = Request["ReturnUrl"];
                    if (string.IsNullOrEmpty(returnUrl))
                        return Redirect("/");
                    else
                        return Redirect(returnUrl);
                }
            }
            ViewData["Title"] = "注册新用户";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            ViewData["UserName"] = UserName;
            ViewData["Password"] = Password;
            if (ViewBag.Setting["show_captcha"] == "on" && (string.IsNullOrEmpty(Request.Form["captcha_code"]) || Session["ValidateCode"] == null || Request.Form["captcha_code"].ToString().ToUpper() != Session["ValidateCode"].ToString().ToUpper()))
            {
                ModelState.AddModelError("captcha_code", "验证码错误");
                return View();
            }
            if (ModelState.IsValid)
            {
                var user = XBBS.DataProvider.AccountDataProvider.GetUser(UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", "不存在的用户");
                    return View();
                }
                else
                {
                    //登录成功
                    if (XBBS.Core.Utils.MD5(Password) == user.Password)
                    {
                        System.Web.HttpContext.Current.Session["User"] = user;
                        System.Web.Security.FormsAuthentication.SetAuthCookie(UserName, false);
                        string returnUrl = Request["ReturnUrl"];
                        if (string.IsNullOrEmpty(returnUrl))
                            return Redirect("/");
                        else
                            return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("LoginResult", "用户名或密码错误");
                    }
                }
            }
            return View();
        }

        public ActionResult LoginOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        public ActionResult Info(int id)
        {
            return View();

        }


        public ActionResult CaptchaCode(int w, int h)
        {
            int codeW = 100;
            int codeH = 40;

            if (!(h > 300 || h <= 0 || w > 600 || w <= 0))
            {
                codeW = w;
                codeH = h;
            }


            int fontSize = 26;
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            //写入Session
            Session["ValidateCode"] = chkCode;
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Color clFont = color[rnd.Next(color.Length)];
            ////画噪线 
            //for (int i = 0; i < 2; i++)
            //{
            //    int x1 = rnd.Next(codeW);
            //    int y1 = rnd.Next(codeH);
            //    int x2 = rnd.Next(codeW);
            //    int y2 = rnd.Next(codeH);
            //    g.DrawLine(new Pen(clFont, 2f), x1, y1, x2, y2);
            //}

            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clFont), (float)i * 18 + 2, (float)0);
            }

            //清除该页输出缓存，设置该页无缓存 
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);

                return File(ms.ToArray(), @"image/Png");

            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }


        }
    }
}
