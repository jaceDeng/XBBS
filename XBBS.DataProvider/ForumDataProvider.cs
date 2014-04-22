using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.DataProvider
{
    public class ForumDataProvider
    {
        public static int TotalForum()
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.ExecuteScalar<int>("SELECT COUNT(1) FROM jexus_forums ");
            }

        }


        public static List<Models.Comment> GetUserComment(int uid, int top)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Fetch<Models.Comment>(1, top, "SELECT \"id\",  fid  ,uid   ,   \"content\"      ,replytime FROM jexus_comments WHERE uid=@0  ORDER BY replytime DESC ", uid).ToList();
            }
        }


        public static List<Models.Forums> GetUserForums(int uid, int top)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Fetch<Models.Forums>(1, top, "SELECT \"fid\",cid,uid,ruid,title,keywords,'' as \"content\",addtime,updatetime,lastreply,views,comments,favorites,closecomment,is_top,is_hidden,ord  FROM   jexus_forums WHERE uid=@0 ORDER BY addtime DESC ", uid).ToList();
            } 
        }

        public static void DeleteForum(int id)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                db.Delete<Models.Forums>("WHERE fid=@0", id);

            }
        }

        public static List<Models.Forums> GetForumsTags(string title)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Forums>("WHERE fid IN (SELECT jexus_tags_relation.fid FROM jexus_tags JOIN jexus_tags_relation ON jexus_tags.tag_id = jexus_tags_relation.tag_id WHERE tag_title=@0)", title).ToList();
            }
        }

        /// <summary>
        /// 插入标签
        /// </summary>
        /// <param name="targTitle"></param>
        /// <param name="fid"></param>
        public static void AddTarg(string targTitle, int fid)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                Models.Targs tag = db.SingleOrDefault<Models.Targs>("WHERE tag_title=@0", targTitle);
                if (tag == null)
                {
                    tag = new Models.Targs();
                    tag.Forums = fid;
                    tag.Title = targTitle;
                    db.Save(tag);
                }

                Models.TargsRelation tagr = new Models.TargsRelation();
                tagr.TagID = tag.TagID;
                tagr.ForumsId = fid;
                db.Insert(tagr);
            }

        }
        /// <summary>
        /// 删除掉帖子的所有标签
        /// </summary>
        /// <param name="fid"></param>
        public static void DeleteTarg(int fid)
        {

            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                db.Execute("DELETE FROM  jexus_tags_relation WHERE fid=@0", fid);
            }
        }

    
        public static List<Models.Category> GetAllCategory()
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Category>("").ToList();
            }
        }

        public static List<Models.Targs> GetAllTarg()
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Targs>(@"SELECT  jexus_tags.tag_id,tag_title,COUNT(1)
FROM    jexus_tags
        JOIN jexus_tags_relation ON jexus_tags.tag_id = jexus_tags_relation.tag_id
        GROUP BY jexus_tags.tag_id,tag_title").ToList();
            }

        }
        public static int AddForum(Models.Forums form)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                int id = Convert.ToInt32(db.Insert(form));
                return id;
            }

        }

        public static List<Models.Forums> GetForums(int cid)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Query<Models.Forums>("WHERE cid=@0", cid).ToList();
            }
        }


        public static List<Models.Forums> GetLastForums(int top, int skip = 0)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.SkipTake<Models.Forums>(skip, top, "  ORDER BY addtime DESC").ToList();
            }
        }
        public static Models.Forums GetForum(int id)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                var f = db.SingleOrDefault<Models.Forums>("WHERE fid=@0", id);
                if (f != null)
                {
                    f.Views++;
                    db.Update(f, new string[] { "views" });
                }
                return f;
            }
        }
        /// <summary>
        /// 获取最新的帖子
        /// </summary>
        /// <returns></returns>
        public static List<Models.Forums> GetLastForum(int top)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.Fetch<Models.Forums>(1, top, " SELECT  * FROM jexus_forums ORDER BY  addtime DESC").ToList();
            }
        }

        public static Models.Category GetCategory(int id)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.SingleOrDefault<Models.Category>("WHERE cid=@0", id);
            }
        }

        public static bool UpdateForum(Models.Forums forum)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                forum.UpdateTime = DateTime.Now;
                db.Save(forum);
                return true;
            }
        }

        public static bool AddComment(Models.Comment comment)
        {
            var f = GetForum(comment.FId);
            if (f == null) return false;
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                f.LastReply = DateTime.Now;
                f.Comments++;
                f.Ruid = comment.UId;
                db.Update(f, new string[] { "lastreply", "comments","ruid" });

                return db.Insert(comment) != null;
            }
        }

        public static int TotalComment(int fid)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                return db.ExecuteScalar<int>("SELECT COUNT(1) FROM jexus_comments  WHERE fid=@0", fid);
            }

        }

        public static List<Models.Comment> GetComments(int fid, int pageSize, int pageIndex, ref int total)
        {
            using (PetaPoco.Database db = new PetaPoco.Database("sqlconnection"))
            {
                var pg = db.Page<Models.Comment>(pageIndex, pageSize, "WHERE fid =@0 ", fid);
                total = (int)pg.TotalPages;
                return pg.Items;
            }
        }
    }
}
