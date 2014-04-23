using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.Models
{
    [PetaPoco.TableName("jexus_tags")]
    [PetaPoco.PrimaryKey("tag_id", autoIncrement = true)]
    public class Targs
    {
        [PetaPoco.Column("tag_id")]
        public int TagID { get; set; }

        [PetaPoco.Column("tag_title")]
        public string Title { get; set; }

        [PetaPoco.Column("forums")]
        public int Forums { get; set; }

        [PetaPoco.Ignore()]
        public int Total { get; set; }
    }


    [PetaPoco.TableName("stb_tags_relation")]
    public class TargsRelation
    {
        [PetaPoco.Column("tag_id")]
        public int TagID { get; set; }

        [PetaPoco.Column("fid")]
        public int ForumsId { get; set; }
    }
}
