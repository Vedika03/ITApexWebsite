using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITApexWebsite.Models
{
    public class UserProductDisplay
    { 
        [Key]
            public int p_Id { get; set; }
        [Required]
            public string p_name { get; set; }
        [Required]
        public string p_img { get; set; }
        [Required]
        public Nullable<int> p_price { get; set; }
        [Required]
        public string p_desc { get; set; }
        [Required]
        public Nullable<int> p_quantity { get; set; }

            public int c_Id { get; set; }
        [Required]
        public string c_name { get; set; }

            public Nullable<int> p_fk_user { get; set; }
            public Nullable<int> p_fk_Category { get; set; }

            public string u_name { get; set; }
            public string u_contact { get; set; }      
    }
}