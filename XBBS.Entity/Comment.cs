using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.Models
{
    [PetaPoco.TableName("jexus_comments")]
    [PetaPoco.PrimaryKey("id", autoIncrement = true)]
    public class Comment
    {
        [PetaPoco.Column("id")]
        public int ID { get; set; }
        [PetaPoco.Column("fid")]
        public int FId { get; set; }
        [PetaPoco.Column("uid")]
        public int UId { get; set; }
        [PetaPoco.Column("content")]
        public string Content { get; set; }
        [PetaPoco.Column("replytime")]
        public DateTime Replytime { get; set; }
    }
}
