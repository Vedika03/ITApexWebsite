using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITApexWebsite.Models
{
    public class UserProductDetail
    {
        public int p_Id { get; set; }

        [Required(ErrorMessage = "Product Name required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charachters are allowed ", MinimumLength = 3)]
        public string p_name { get; set; }

        [Required(ErrorMessage = "Product Image required")]
        public string p_img { get; set; }

        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> p_price { get; set; }

        [Required(ErrorMessage = "description is required")]
        public string p_desc { get; set; }

        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> p_quantity { get; set; }

        public Nullable<int> p_fk_user { get; set; }

        [Required]
        [Range(1, 50)]
        public Nullable<int> p_fk_Category { get; set; }

        public Nullable<bool> isActive { get; set; }

        public Nullable<bool> isDelete { get; set; }

        public Nullable<System.DateTime> p_createdDate { get; set; }

        public Nullable<System.DateTime> p_modifiedDate { get; set; }

        public SelectList Categories { get; set; }
    }
}