using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Compare {
    [DataContract]
    public class Automobile {
        [DataMember] public int Model;
        [DataMember] public string Type;
    }

    [DataContract]
    public class Car : Automobile {
        [DataMember(EmitDefaultValue = false)] public bool IsHatchback;

        [DataMember(Name = "WheelsList")] public List<Wheel> Wheels;

        [DataMember] public DateTime ManufacturingDate;

        [DataMember] public Hashtable tbl;

        [DataMember] public Address ManufacturingAdress;

        [DataMember] public Address AssemblingAdress;

        // JsonConverter für Json.NET
        [DataMember(Name = "CarCondition"), JsonConverter(typeof(StringEnumConverter))]
        public CarConditionEnum En;
    }

    [DataContract]
    public class Wheel {
        [DataMember] public int id;
        [DataMember] public string Type;
    }

    [DataContract]
    public class Address {
        [DataMember] public int PostalCode;
        [DataMember] public string Country;
    }

    [DataContract]
    public enum CarConditionEnum {
        [EnumMember(Value = "New")] New,
        [EnumMember] Used,
        [EnumMember] Rental
    }
}