using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITApexWebsite.Models
{
    public class SecurityProductDisplay
    {
        public int s_Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string s_name { get; set; }
        [Required]
        public string s_img { get; set; }
        [Required]
        public string s_desc { get; set; }
        [Required]
        public Nullable<int> s_price { get; set; }
        [Required]
        public Nullable<int> s_quan { get; set; }
    }
}