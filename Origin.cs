//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    //this is required for imageupload file property
    using System.Web;

    public partial class Origin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Origin()
        {
            this.styles = new HashSet<styles>();
        }
    
        public int OID { get; set; }
        public string about { get; set; }
        [DisplayName("Upload File...")]
        public string image { get; set; }
        public string country { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        //after the above, go to view, using razer, add the <input type="file" name="ImageFile" required>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<styles> styles { get; set; }

        
    }
}
