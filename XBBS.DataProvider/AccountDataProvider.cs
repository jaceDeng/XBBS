using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBBS.Models;

namespace XBBS.DataProvider
{
    public static class AccountDataProvider
    {

        /// <summary>
        /// 根据登录名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User GetUser(string userName)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.SingleOrDefault<Models.User>("WHERE username=@0 ", userName);
            }
        }
        public static User GetUser(int uid)
        {
            User user = null;
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                user = db.SingleOrDefault<Models.User>("WHERE uid=@0 ", uid);
            }
            if (user == null)
                user = new User();
            return user;
        }
        public static bool CreatUser(User user)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                user.Password = XBBS.Core.Utils.MD5(user.Password);
                return db.Insert(user) != null;
            }

        }

        public static Models.UserGroup GetUserGroup(int id)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.SingleOrDefault<Models.UserGroup>("WHERE gid=@0 ", id);
            }


        }
    }
}
