using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace XBBS.Models
{
    [PetaPoco.TableName("stb_users")]
    [PetaPoco.PrimaryKey("uid", autoIncrement = true)]
    public class User
    {
        [PetaPoco.Column("username")]
        [Required(ErrorMessage = "用户名必须填写")]
        public string UserName { get; set; }

        [PetaPoco.Column("email")]
        [Required(ErrorMessage = "电子邮件必填")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "请输入正确的电子邮件")]
        public string Email { get; set; }

        [PetaPoco.Column("password")]
        [Required(ErrorMessage = "密码必须填写")]
        public string Password { get; set; }


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
        [PetaPoco.Column("openid")]
        public string Openid
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("avatar")]
        public string Avatar
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("homepage")]
        public string Homepage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("money")]
        public Nullable<int> Money
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("signature")]
        public string Signature
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("forums")]
        public Nullable<int> Forums
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("replies")]
        public Nullable<int> Replies
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("notices")]
        public Nullable<short> Notices
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("follows")]
        public int Follows
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("regtime")]
        public Nullable<DateTime> Regtime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("lastlogin")]
        public Nullable<DateTime> Lastlogin
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("lastpost")]
        public Nullable<DateTime> Lastpost
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("qq")]
        public string QQ
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("group_type")]
        public short GroupType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("gid")]
        public short Gid
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("ip")]
        public string IP
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("location")]
        public string Location
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("token")]
        public string Token
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("introduction")]
        public string Introduction
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [PetaPoco.Column("is_active")]
        public short IsActive
        {
            get;
            set;
        }
    }
}
