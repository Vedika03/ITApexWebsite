using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ITApexWebsite.Models
{
    public class UserReg
    {
        public int u_Id { get; set; }

        [Required(ErrorMessage = " Name required")]
        public string u_name { get; set; }

        [Required(ErrorMessage = "Email required")]
        public string u_email { get; set; }

        [Required(ErrorMessage = "Contact required")]
        public string u_contact { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string u_pass { get; set; }

        [DisplayName("Confirm Password")]
        [Compare("u_pass", ErrorMessage ="Password and confirm password should match")]
        public string confirmPass { get; set; }

        public Nullable<System.DateTime> u_CreatedOn { get; set; }
        public Nullable<System.DateTime> u_ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}