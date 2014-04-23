using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

        public ActionResult QQLogin()
        {
            //应用的APPID--申请的APPID
            string app_id = "1010000"; //修改为您的申请的  APP ID  
            //成功授权后的回调地址--授权成功后的回调地址
            string callbackurl = "http://bbs.jexus.org/Home/qqcallback";//修改为您的回调地址
            Session["requeststate"] = Guid.NewGuid().ToString().Replace("-", ""); //state参数用于防止CSRF攻击，成功授权后回调时会原样带回
            string scope = "get_simple_userinfo,get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
            //拼接URL     
            string dialog_url = "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id="
               + app_id + "&scope=" + scope + "&redirect_uri=" + Server.UrlEncode(callbackurl) + "&state="
               + Session["requeststate"];
            return Redirect(dialog_url);
        }



        private string file_get_contents(string url, System.Text.Encoding encode)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            WebResponse response = request.GetResponse();
            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    int readc;
                    byte[] buffer = encode.GetBytes(url);// new byte[1024];
                    while ((readc = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, readc);
                    }
                }
                return encode.GetString(ms.ToArray());
            }
        }

        private NameValueCollection ParseJson(string json_code)
        {
            NameValueCollection mc = new NameValueCollection();
            Regex regex = new Regex(@"(\s*\""?([^""]*)\""?\s*\:\s*\""?([^""]*)\""?\,?)");
            json_code = json_code.Trim();
            if (json_code.StartsWith("{"))
            {
                json_code = json_code.Substring(1, json_code.Length - 2);
            }
            foreach (Match m in regex.Matches(json_code))
            {
                mc.Add(m.Groups[2].Value, m.Groups[3].Value);
                //Response.Write(m.Groups[2].Value + "=" + m.Groups[3].Value + "<br/>");
            }
            return mc;
        }

        private NameValueCollection ParseUrlParameters(string str_params)
        {
            NameValueCollection nc = new NameValueCollection();
            foreach (string p in str_params.Split('&'))
            {
                string[] p_s = p.Split('=');
                nc.Add(p_s[0], p_s[1]);
            }
            return nc;
        }

        public ActionResult qqcallback()
        {
            //应用的APPID--申请的APPID
            string app_id = "10100000"; //修改为您的申请的  APP ID
            //应用的APPKEY--申请的APPKEY
            string app_key = "123121231312312312312312";//申请的APPKEY
            //成功授权后的回调地址--授权成功后的回调地址
            string callbackurl = "http://bbs.jexus.org/Home/qqcallback";//修改为您的回调地址
            if (Request.Params["code"] != null)
            {
                string code = Request["code"].ToString();
                if (Session["requeststate"] == null)
                {
                    return QQLogin();
                }
                string state = Session["requeststate"].ToString();
                if (Request["state"].ToString().Equals(state))
                {
                    //通过Authorization Code获取Access Token
                    //拼接URL   
                    string token_url = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&"
                    + "client_id=" + app_id + "&redirect_uri=" + Server.UrlEncode(callbackurl)
                    + "&client_secret=" + app_key + "&code=" + code + "&state=" + state;
                    string response = file_get_contents(token_url, Encoding.UTF8);
                   
                    NameValueCollection msg;
                    if (response.IndexOf("callback") != -1)
                    {
                        int lpos = response.IndexOf("(");
                        int rpos = response.IndexOf(")");
                        response = response.Substring(lpos + 1, rpos - lpos - 1);
                        msg = ParseJson(response);

                        if (!string.IsNullOrEmpty(msg["error"]))
                        {
                            return Content("<h3>error:</h3>" + msg["error"].ToString() + "<h3>msg  :</h3>" + msg["error_description"]);
                        }
                    }
                    //使用Access Token来获取用户的OpenID
                    NameValueCollection ps = ParseUrlParameters(response);
                    string graph_url = "https://graph.qq.com/oauth2.0/me?access_token=" + ps["access_token"];
                    string str = file_get_contents(graph_url, Encoding.UTF8);
                    if (str.IndexOf("callback") != -1)
                    {
                        int lpos = str.IndexOf("(");
                        int rpos = str.IndexOf(")");
                        str = str.Substring(lpos + 1, rpos - lpos - 1);
                    }
                    NameValueCollection user = ParseJson(str);
                    if (!string.IsNullOrEmpty(user["error"]))
                    {
                        return Content("<h3>error:</h3>" + user["error"] + "<h3>msg  :</h3>" + user["error_description"]);
                    }
                    // user["openid"];//QQ接口唯一性
                    //加上自己需要用的处理逻辑
                    //思路提示：
                    //通过在会员登录表建立一个qqOpenId 字段为判断 QQ登录
                    //判断您的用户是不是第一次使用QQ登录，是就为 这个用户注册
                    //使用Access Token、penid、app_id来获取用户的信息,具体请看QQ开放平台数据http://wiki.connect.qq.com/api%e5%88%97%e8%a1%a8
                    string getuserinfo_url = "https://graph.qq.com/user/get_user_info?access_token=" + ps["access_token"] + "&oauth_consumer_key=" + app_id + "&openid=" + user["openid"];
                    string ret_info = file_get_contents(getuserinfo_url, Encoding.UTF8);
                    NameValueCollection userinfo = ParseJson(ret_info);
                    string username = userinfo["nickname"].ToString();//QQ昵称
                    string UserFace = userinfo["figureurl_1"].ToString();//QQ空间头像
                    string gender = userinfo["gender"].ToString();//返回性别 男 女

                    //如果通过qqOpenId判断已经存在 就结合自己的网站登录程序 给用户登录处理,不存在就给用户绑定注册处理
                    string openid = userinfo["openid"];
                    var u = DataProvider.AccountDataProvider.GetUserByOpenID(openid);
                    if (u == null)
                    {
                        return Redirect("~/Login?openid=" + openid);
                    }
                    else
                    {
                        //登录成功
                        System.Web.Security.FormsAuthentication.SetAuthCookie(u.UserName, false);
                        string returnUrl = Request["ReturnUrl"];
                        if (string.IsNullOrEmpty(returnUrl))
                            return Redirect("/");
                        else
                            return Redirect(returnUrl);
                    }

                }
                else
                {
                    return Content("The state does not match. You may be a victim of CSRF.request=" +
                        Request["state"] + ",session=" + state);
                }


            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}
