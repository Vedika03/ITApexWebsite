using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITApexWebsite.Models
{
    public class Cart
    {
        public int s_Id { get; set; }

        public decimal Quantity { get; set; }

        public decimal unitPrice { get; set; }

        public decimal Total { get; set; }

        public string ImagePath { get; set; }

        public string ImageName { get; set; }
    }
}