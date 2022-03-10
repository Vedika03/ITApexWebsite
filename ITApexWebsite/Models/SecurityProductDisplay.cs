using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITApexWebsite.Models
{
    public class SecurityProductDisplay
    {
        public int s_Id { get; set; }
        public string s_name { get; set; }
        public string s_img { get; set; }
        public string s_desc { get; set; }
        public Nullable<int> s_price { get; set; }
        public Nullable<int> s_quan { get; set; }
    }
}