using System;
using System.Runtime.Serialization;

namespace SerializationTasks
{
    [Serializable]
    public class NodeZbW : ISerializable
    {
        public DateTime Date;
        public NodeZbW Next;
        public NodeZbW(DateTime d)
        {
            Date = d;
        }
        protected NodeZbW(SerializationInfo info, StreamingContext context)
        {
            var year = (int)info.GetValue("year", typeof(int));
            var month = (int)info.GetValue("month", typeof(int));
            var day = (int)info.GetValue("day", typeof(int));
            Date = new DateTime(year, month, day);
            Next = (NodeZbW)info.GetValue("next", typeof(NodeZbW));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("year", Date.Year);
            info.AddValue("month", Date.Month);
            info.AddValue("day", Date.Day);
            info.AddValue("next", Next);
        }
    }

    [Serializable]
    public class List
    {
        private NodeZbW _head;
        public void Add(DateTime date)
        {
            var p = new NodeZbW(date) {Next = _head};
            _head = p;
        }

        public bool Contains(DateTime date)
        {
            var p = _head;
            while (p != null && date.CompareTo(p.Date) != 0)
            {
                p = p.Next;
            }

            return p != null;
        }
    }
}
