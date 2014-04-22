using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.Models
{

    [PetaPoco.TableName("jexus_page")]
    [PetaPoco.PrimaryKey("pid")]
    public class Page
    {
        [PetaPoco.Column("pid")]
        public int PId { get; set; }
        [PetaPoco.Column("title")]
        public string Title { get; set; }
        [PetaPoco.Column("content")]
        public string Content { get; set; }
        [PetaPoco.Column("go_url")]
        public string Url { get; set; }
        [PetaPoco.Column("add_time")]
        public DateTime AddTime { get; set; }

        [PetaPoco.Column("is_hidden")]
        public int IsHidden { get; set; }

    }
}
