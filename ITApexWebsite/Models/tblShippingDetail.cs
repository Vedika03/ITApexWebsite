//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITApexWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblShippingDetail
    {
        public int shipping_Id { get; set; }
        public Nullable<int> shipping_fk_user { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
        public Nullable<int> order_Id { get; set; }
        public Nullable<decimal> amount_paid { get; set; }
        public string payment_type { get; set; }
    
        public virtual tblUser tblUser { get; set; }
    }
}