﻿using System;
using System.Data.Linq.Mapping;

namespace SysInventory.LogMessages.Models
{
    [Table(Name = "Location")]
    public sealed class Location : ILocation
    {
        [Column(Name = "LocationId", IsPrimaryKey = true)]
        public Guid Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "PodFk")]
        public Guid PoDId { get; set; }
        [Column(Name = "ParentId")]
        public Guid? ParentId { get; set; }
        public int? Level { get; set; }
        public override string ToString() => Level != null ? Name + " Level " + Level : Name;
    }
}
