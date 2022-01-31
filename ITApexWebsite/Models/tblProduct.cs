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
    
    public partial class tblProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProduct()
        {
            this.tblCarts = new HashSet<tblCart>();
        }
    
        public int p_Id { get; set; }
        public string p_name { get; set; }
        public string p_image { get; set; }
        public string p_description { get; set; }
        public Nullable<int> p_quantity { get; set; }
        public Nullable<int> p_price { get; set; }
        public Nullable<System.DateTime> p_createdDate { get; set; }
        public Nullable<System.DateTime> p_modifiedDate { get; set; }
        public Nullable<bool> p_isFeatured { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public int p_fk_c { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCart> tblCarts { get; set; }
        public virtual tblCategory tblCategory { get; set; }
    }
}
