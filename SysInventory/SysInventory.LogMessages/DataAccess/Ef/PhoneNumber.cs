//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysInventory.LogMessages.DataAccess.Ef
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhoneNumber
    {
        public System.Guid Id { get; set; }
        public string Number { get; set; }
        public System.Guid PhoneTypeFk { get; set; }
        public System.Guid ContactPersonFk { get; set; }
    
        public virtual PhoneType PhoneType { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
    }
}