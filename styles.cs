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
    using System.ComponentModel.DataAnnotations;

    public partial class styles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public styles()
        {
            this.fighters = new HashSet<fighters>();
        }
    
        public int OID { get; set; }
        public int StylesID { get; set; }
        public string about { get; set; }
        [DisplayName("Upload File...")]
        public string image { get; set; }
        public string style { get; set; }
    
        [DisplayName("Video eg. 6jaHyyi3m30")]
        public string video { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        //after the above, go to view, using razer, add the <input type="file" name="ImageFile" required>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fighters> fighters { get; set; }
        public virtual Origin Origin { get; set; }
    }
}
