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
    
    public partial class PhoneNumbers
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public Nullable<int> PersonID { get; set; }
    
        public virtual People People { get; set; }
    }
}
