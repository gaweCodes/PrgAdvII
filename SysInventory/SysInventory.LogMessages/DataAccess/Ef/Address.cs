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
    
    public partial class Address : IIdentifiable, IEquatable<Address>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            this.Customers = new HashSet<Customer>();
            this.ContactPersons = new HashSet<ContactPerson>();
        }
    
        public System.Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
        public System.Guid CityFk { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual City City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactPerson> ContactPersons { get; set; }
        public override string ToString() => $"{Street} {HouseNo}{Environment.NewLine}{City}";
        public override bool Equals(object obj) => Equals(obj as Address);
        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(ToString(), other.ToString());
        }
        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;
                var hash = hashingBase;
                return (hash * hashingMultiplier) ^ ToString().GetHashCode();
            }
        }
        public static bool operator ==(Address addressA, Address addressB)
        {
            if (ReferenceEquals(addressA, addressB)) return true;
            return addressA?.Equals(addressB) == true;
        }
        public static bool operator !=(Address addressA, Address addressB) => !(addressA == addressB);
    }
}
