using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.Models
{
    [TableName("stb_categories")]
    [PrimaryKey("cid", autoIncrement = false)]
    public class Category
    {

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "cid")]
        public short CId
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "pid")]
        public short PId
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "cname")]
        public string CName
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "keywords")]
        public string Keywords
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "ico")]
        public string Icon
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "master")]
        public string Master
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "permit")]
        public string Permit
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "listnum")]
        public Nullable<int> ListNum
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "clevel")]
        public string CLevel
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "cord")]
        public Nullable<short> COrd
        {
            get;
            set;
        }

        public static bool IsParent(List<XBBS.Models.Category> listCategory, int cid)
        {
            foreach (var item in listCategory)
            {
                if (item.PId == cid)
                    return true;
            }
            return false;
        }

        public static List<XBBS.Models.Category> GetCatList(List<XBBS.Models.Category> listCategory, int pid)
        {
            List<XBBS.Models.Category> list = new List<XBBS.Models.Category>();
            foreach (var item in listCategory)
            {
                if (item.PId == pid)
                {
                    list.Add(item);
                }
            }
            return list;

        }
    }
}
