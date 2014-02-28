using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.DataProvider
{
    public class ForumDataProvider
    {

        public static List<Models.Category> GetAllCategory()
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Category>("").ToList();
            }
        }


        public static bool AddForum(Models.Forums form)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Insert(form) != null;
            }

        }

        public static List<Models.Forums> GetForums(int cid)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Forums>("WHERE cid=@0", cid).ToList();
            }
        }
    }
}
