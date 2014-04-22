using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.Models
{
    [PetaPoco.TableName("jexus_forums")]
    [PetaPoco.PrimaryKey("fid")]
    /// <summary>
    /// 帖子
    /// </summary>
    public class Forums
    {
        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("fid")]
        public int FId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("cid")]
        public int Cid
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("uid")]
        public int Uid
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("ruid")]
        public Nullable<int> Ruid
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("title")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("keywords")]
        public string Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("addtime")]
        public Nullable<DateTime> AddTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("updatetime")]
        public Nullable<DateTime> UpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("lastreply")]
        public Nullable<DateTime> LastReply
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("views")]
        public int Views
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("comments")]
        public Nullable<short> Comments
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("favorites")]
        public Nullable<long> Favorites
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("closecomment")]
        public Nullable<short> CloseComment
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("is_top")]
        public short IsTop
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("is_hidden")]
        public short IsHidden
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("ord")]
        public long Ord
        {
            get;
            set;
        }

    }
}
