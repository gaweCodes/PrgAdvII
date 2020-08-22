using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;

namespace Service
{
    [XmlSerializerFormat]
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        //[XmlSerializerFormat]
        Car GetCar();

    }

    [XmlRoot(ElementName = "CarRoot", Namespace = "http://serialization")]
    public class Car
    {
        [XmlElement(ElementName = "CarType", DataType = "string")]
        public string Type;

        [XmlAttribute]
        public int Model;

        [XmlArray(ElementName = "WheelItems", Namespace = "http://serialization.array")]
        public List<Wheel> Wheels;

        [XmlIgnore]
        public int Price;

        [XmlElement("Radio")]
        public Radio _Radio;
    }

    public class Wheel
    {
        public string Type;
    }

    public class Radio
    {
        [XmlAttribute]
        public int Model;

        [XmlText()]
        public string Type;
    }

}
