using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App {
    [XmlRoot(ElementName = "CarRoot", Namespace = "http://serialization")]
    public class Car {
        [XmlElement(ElementName = "CarType", DataType = "string")]
        public string Type;

        [XmlAttribute] public int Model;

        [XmlArray(ElementName = "WheelItems", Namespace = "http://serialization.array")]
        public List<Wheel> Wheels;

        [XmlIgnore] public int Price;

        [XmlElement("Radio")] public Radio _Radio;
    }

    public class Wheel {
        public string Type;
    }

    public class Radio {
        [XmlAttribute] public int Model;

        [XmlText()] public string Type;
    }
}