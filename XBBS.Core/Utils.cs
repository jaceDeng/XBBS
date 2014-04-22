using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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


        public static string SubString(string txt, int len)
        {
            if (string.IsNullOrEmpty(txt))
                return "";
            return txt.Length > len ? txt.Substring(0, len) : txt;
        }


        /// <summary>
        /// 头像的缩放
        /// </summary>
        /// <param name="filestrm">图像文件流</param>
        /// <param name="path">保存目录etg:/uploads/avatar/12</param>
        /// <returns></returns>
        public static bool ThumImage(System.IO.Stream filestrm, string path)
        {
            try
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(filestrm);
                if (bmp.Width < 100 || bmp.Height < 100)
                {
                    bmp.Dispose();
                    return false;
                }

                System.Drawing.Bitmap[] bmps = new System.Drawing.Bitmap[3];
                bmps[0] = new Bitmap(100, 100);
                bmps[1] = new Bitmap(60, 60);
                bmps[2] = new Bitmap(24, 24);

                //100X100
                System.Drawing.Graphics gf = System.Drawing.Graphics.FromImage(bmps[0]);
                gf.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gf.SmoothingMode = SmoothingMode.HighQuality;
                gf.DrawImage(bmp, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                gf.Dispose();

                //60X60
                gf = System.Drawing.Graphics.FromImage(bmps[1]);
                gf.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gf.SmoothingMode = SmoothingMode.HighQuality;
                gf.DrawImage(bmp, new Rectangle(0, 0, 60, 60), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                gf.Dispose();

                //24x24
                gf = System.Drawing.Graphics.FromImage(bmps[2]);
                gf.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gf.SmoothingMode = SmoothingMode.HighQuality;
                gf.DrawImage(bmp, new Rectangle(0, 0, 24, 24), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                gf.Dispose();


                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                string path2 = System.IO.Path.Combine(path, "avatar_large.jpg");
                bmps[0].Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg);
                bmps[1].Save(System.IO.Path.Combine(path, "default.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                bmps[2].Save(System.IO.Path.Combine(path, "avatar_small.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);


                bmp.Dispose();
                bmps[0].Dispose();
                bmps[1].Dispose();
                bmps[2].Dispose();
                return true;
            }
            catch
            {
                return false;
            }
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
                    return ((int)ts.TotalDays).ToString() + " 天前";

            }

        }

    }
}
