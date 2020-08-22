using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SerializationTasks
{
    internal static class Program
    {
        private static void Main()
        {
            var x = new BinaryTree();
            var rnd = new Random(0);
            for (var i = 0; i < 1000; i++)
            {
                x.Insert(rnd.Next());
            }
            
            SerializeBinary(x);
            SerializeXml(x);
            SerializeJson(x);

            x = DeserializeBinary();
            x = DeserializeXml();
            x = DeserializeJson<BinaryTree>();

            var list = new List();
            list.Add(new DateTime(2018, 4, 25, 10, 30, 0));
            list.Add(new DateTime(2018, 3, 5, 12, 24, 30));
            list.Add(new DateTime(2018, 1, 20, 11, 11, 11));
            IFormatter f = new BinaryFormatter();

            using (var s = new FileStream("myfile", FileMode.Create))
            {
                f.Serialize(s, list);
                s.Close();
            }

            using (var s = new FileStream("myfile", FileMode.Open))
            {
                var newList = f.Deserialize(s) as List;
                s.Close();
            }
        }

        private static void SerializeBinary(BinaryTree x)
        {
            using (Stream stream = new FileStream("binary.dat", FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, x);
                stream.Close();
            }
        }

        private static BinaryTree DeserializeBinary()
        {
            using (Stream stream = new FileStream("binary.dat", FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (BinaryTree)formatter.Deserialize(stream);
            }
        }

        private static void SerializeXml(BinaryTree x)
        {
            using (Stream stream = new FileStream("xml.xml", FileMode.OpenOrCreate))
            {
                var ser = new XmlSerializer(typeof(BinaryTree));
                ser.Serialize(stream, x);
                stream.Close();
            }
        }

        private static BinaryTree DeserializeXml()
        {
            using (Stream stream = new FileStream("xml.xml", FileMode.Open))
            {
                var ser = new XmlSerializer(typeof(BinaryTree));
                return (BinaryTree)ser.Deserialize(stream);
            }
        }

        private static void SerializeJson<T>(T x)
        {
            File.WriteAllText("json.json", JsonConvert.SerializeObject(x, Formatting.Indented,
                new JsonSerializerSettings {PreserveReferencesHandling = PreserveReferencesHandling.All}));
        }

        private static T DeserializeJson<T>()
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText("json.json"));
        }
    }

    [Serializable]
    public class Node
    {
        public int Val;
        public Node Left, Right;
    }

    [Serializable]
    public class BinaryTree
    {
        public Node Root;
        public void Insert(int x)
        {
            Node p = Root, father = null;
            while (p != null)
            {
                father = p;
                p = x < p.Val ? p.Left : p.Right;
            }

            p = new Node { Val = x };
            if (Root == null)
            {
                Root = p;
            }
            else if (x < father.Val)
            {
                father.Left = p;
            }
            else
            {
                father.Right = p;
            }
        }

        public bool Contains(int x)
        {
            var p = Root;
            while (p != null)
            {
                if (p.Val == x)
                {
                    return true;
                }

                p = x < p.Val ? p.Left : p.Right;
            }

            return false;
        }

        private void P(Node p)
        {
            if (p != null)
            {
                P(p.Left);
                Console.WriteLine(p.Val);
                P(p.Right);
            }
        }

        public void Print()
        {
            P(Root);
        }
    }
}