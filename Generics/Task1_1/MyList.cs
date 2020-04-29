using System.Collections;
using System.Collections.Generic;

namespace Task1_1
{
    internal class MyList<T> : IEnumerable<T>
    {
        protected Node Head;
        protected Node Current = null;
        protected sealed class Node
        {
            public Node Next { get; set; }
            public T Data { get; set; }
            public Node(T t)
            {
                Next = null;
                Data = t;
            }
        }
        public MyList() => Head = null;
        public void Add(T t)
        {
            var n = new Node(t) {Next = Head};
            Head = n;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var curr = Head;
            while (curr != null)
            {
                yield return curr.Data;
                curr = curr.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
