//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocationEf : ILocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocationEf()
        {
            this.Location1 = new HashSet<LocationEf>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid PoDId { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocationEf> Location1 { get; set; }
        public virtual LocationEf Location2 { get; set; }
        public override string ToString() => Name;
    }
}