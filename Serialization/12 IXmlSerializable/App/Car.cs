using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace App {

    public class Car : IXmlSerializable {
        #region Members

        public bool IsHatchback;

        public string Type;

        public int Model;

        public int Price;

        public List<Wheel> Wheels;

        #endregion

        #region IXmlSerializable

        public void WriteXml(XmlWriter writer) {
            //<Car IsHatchback="False">
            writer.WriteAttributeString("IsHatchback", IsHatchback.ToString());

            // Nicht möglich ohne IXmlSerializble
            writer.WriteStartElement("Info"); //<Info>

            //<Info Model="2015">
            writer.WriteAttributeString("Model", Model.ToString());

            //<Type>Chev</Type>
            writer.WriteElementString("Type", Type);

            //<Price>20000</Price>
            writer.WriteElementString("Price", Price.ToString());

            //</Info>
            writer.WriteEndElement();

            writer.WriteStartElement("Wheels"); //<Wheels>
            foreach (var wheel in Wheels) {
                writer.WriteStartElement("Wheel"); //<Wheel>

                //<Wheel Id="1">
                writer.WriteAttributeString("Id", wheel.Id.ToString());
                writer.WriteString(wheel.Type); //<Wheel Id="1">Bridgestone
                writer.WriteEndElement(); //</Wheel>
            }

            writer.WriteEndElement(); //</Wheels>
        }

        public void ReadXml(XmlReader reader) {
            //initialize values
            Wheels = new List<Wheel>();
            int wheelId;
            string wheelType;

            IsHatchback = bool.Parse(reader.GetAttribute("IsHatchback"));
            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element) {
                    switch (reader.Name) {
                        case "Info":
                            Model = int.Parse(reader.GetAttribute("Model"));
                            break;
                        case "Type":
                            //reads the next node, which is a Text node
                            reader.Read();

                            //reads the value of the Text node
                            Type = reader.Value;
                            break;
                        case "Price":
                            //reads the next node, which is a Text node
                            reader.Read();

                            //reads the value of the Text node
                            Price = int.Parse(reader.Value);
                            break;
                        case "Wheel":
                            wheelId = int.Parse(reader.GetAttribute("Id"));
                            reader.Read();
                            wheelType = reader.Value;

                            Wheels.Add(new Wheel {
                                Id = wheelId,
                                Type = wheelType
                            });
                            break;

                    }
                }
            }
        }

        public XmlSchema GetSchema() {
            return null;
        }

        #endregion

    }

    public class Wheel {
        public int Id;
        public string Type;
    }


}