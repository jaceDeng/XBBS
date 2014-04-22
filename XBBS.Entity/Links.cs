using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBBS.Models
{

    [PetaPoco.TableName("jexus_links")]
    [PetaPoco.PrimaryKey("id")]
    public class Links
    {
        [PetaPoco.Column("id")]
        public int Id { get; set; }
        [PetaPoco.Column("name")]
        public string Name
        { get; set; }
        [PetaPoco.Column("url")]
        public string Url { get; set; }
        [PetaPoco.Column("logo")]
        public string Logo { get; set; }
        [PetaPoco.Column("is_hidden")]
        public int IsHidden { get; set; }
    }
}
