//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projekt_zaliczenie
{
    using System;
    using System.Collections.Generic;
    
    public partial class Owners
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Owners()
        {
            this.PhoneBooks = new HashSet<PhoneBooks>();
        }
    
        public int ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhoneBooks> PhoneBooks { get; set; }
    }
}
