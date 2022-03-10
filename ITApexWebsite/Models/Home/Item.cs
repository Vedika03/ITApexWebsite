using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITApexWebsite.Models.Home
{

    public class Item
    {
        public tblProduct Product { get; set; }



        public securityProduct SProduct { get; set; }

        public tblUser User { get; set; }

        public int Quantity { get; set; }

        public string p_image { get; set; }

        public string CustomerName { get; set; }

        public string CustomerNo { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerAddress { get; set; }

    }
}