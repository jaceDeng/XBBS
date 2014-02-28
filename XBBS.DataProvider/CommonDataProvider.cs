using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBBS.Models;

namespace XBBS.DataProvider
{
    public static class CommonDataProvider
    {


        /// <summary>
        /// 获取所有的
        /// </summary>
        /// <returns></returns>
        public static List<Settings> GetAllSetting()
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Settings>("").ToList();
            }

        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static Settings GetSetting(string title)
        {

            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.SingleOrDefault<Models.Settings>("WHERE title=@0", title);
            }
        }

        /// <summary>
        /// 获取所有友情链接
        /// </summary>
        /// <returns></returns>
        public static  List<Models.Links> GetAllLinks()
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Links>("").ToList();
            }

        }

        public static List<Models.Page> GetAllPages()
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Page>("").ToList();
            }
        }
    }
}
