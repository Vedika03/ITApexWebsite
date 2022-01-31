using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ITApexWebsite.Models
{
    public class ShippingDetails
    {
        public int shipping_Id { get; set; }
        [Required]
        public Nullable<int> shipping_fk_user { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string state { get; set; }

        [Required]
        public string country { get; set; }

        [Required]
        public string pincode { get; set; }

        public Nullable<int> order_Id { get; set; }
        public Nullable<decimal> amount_paid { get; set; }

        [Required]
        public string payment_type { get; set; }
    }
}