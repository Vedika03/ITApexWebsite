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
    
    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            this.tblProduct_u = new HashSet<tblProduct_u>();
        }
    
        public int u_Id { get; set; }
        public string u_name { get; set; }
        public string u_email { get; set; }
        public string u_contact { get; set; }
        public string u_pass { get; set; }
        public Nullable<System.DateTime> u_CreatedOn { get; set; }
        public Nullable<System.DateTime> u_ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct_u> tblProduct_u { get; set; }
    }
}
