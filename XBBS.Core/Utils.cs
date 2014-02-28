using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace XBBS.Core
{
    public class Utils
    {
        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret.ToUpper();
        }



        /// <summary>
        /// 格式化日期友好
        /// </summary>
        /// <param name="dt"></param>
        public static string FriendlyDate(DateTime dt)
        {
            TimeSpan ts = DateTime.Now - dt;
            if (ts.TotalSeconds < 10)
            {
                return "刚刚";
            }
            if ((ts.TotalSeconds >= 0) && (ts.TotalSeconds <= 60))
            {
                return ((int)ts.TotalSeconds).ToString() + "秒前";
            }
            if ((ts.TotalMinutes >= 0) && (ts.TotalMinutes <= 60))
            {
                return ((int)ts.TotalMinutes).ToString() + "分钟前";
            }
            if ((ts.TotalHours >= 0) && (ts.TotalHours <= 24))
            {
                return ((int)ts.TotalHours).ToString() + "小时前";
            }
            if (ts.TotalDays > 60)
            {
                return dt.ToString("yyyy-MM-dd");
            }

            switch ((int)ts.TotalDays)
            {
                case 0:
                    return dt.ToString("今天HH:mm");

                case 1:
                    return dt.ToString("昨天HH:mm");

                default:
                    //$day += 1;
                    return ((int)ts.TotalDays).ToString() + " 天前";

            }

        }

    }
}
