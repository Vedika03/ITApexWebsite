using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITApexWebsite.Models.Home
{

    public class Item
    {
        public tblProduct Product { get; set; }

        public int Quantity { get; set; }

        public string p_image { get; set; }

    }
}