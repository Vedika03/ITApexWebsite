using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ITApexWebsite.Models
{
    public class CategoryDetail
    {
        public int c_Id { get; set; }
        [Required(ErrorMessage ="Category Name required")]
        [StringLength(100,ErrorMessage ="Minimum 3 and minimum 5 and maximum 100 charachters are allowed ",MinimumLength =3)]
        public string c_name { get; set; }
        public int c_fk_ad { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<bool> isdelete { get; set; }
    }


    public class ProductDetails
    {
        

        public int p_Id { get; set; }

        [Required(ErrorMessage = "Product Name required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charachters are allowed ", MinimumLength = 3)]
        public string p_name { get; set; }

        [Required(ErrorMessage = "Product Image required")]
        public string p_image { get; set; }

        [Required(ErrorMessage ="description is required")]
        public string p_description { get; set; }

        [Required]
        [Range(typeof(int),"1","500",ErrorMessage ="Invalid Quantity")]
        public Nullable<int> p_quantity { get; set; }

        [Required]
        [Range(typeof(int),"1","20000000",ErrorMessage ="Invalid Price")]
        public Nullable<int> p_price { get; set; }

        public Nullable<System.DateTime> p_createdDate { get; set; }

        public Nullable<System.DateTime> p_modifiedDate { get; set; }

        public Nullable<bool> p_isFeatured { get; set; }

        public Nullable<bool> isActive { get; set; }

        public Nullable<bool> isDelete { get; set; }
        [Required]
        [Range(1,50)]
        public Nullable<int> p_fk_c { get; set; }
        public SelectList Categories { get; set; }
    }
}