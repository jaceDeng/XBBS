using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.Models
{
    [TableName("jexus_user_groups")]
    [PrimaryKey("gid", autoIncrement = false)]
    public class UserGroup
    {

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "gid")]
        public int GId
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "group_type")]
        public short GroupType
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "group_name")]
        public string GroupName
        {
            get;
            set;
        }

        /// <summary> 
        /// 
        /// </summary>
        [ColumnAttribute(Name = "usernum")]
        public int UserNum
        {
            get;
            set;
        }
    }
}
