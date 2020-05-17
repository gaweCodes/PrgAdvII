using System;
using System.Collections.Generic;

namespace Iterators
{
    internal class RecursiveList<T>
    {
        private Node _root;
        private Node _tail;
        internal class Node
        {
            public Node Next { get; set; }
            public T Value { get; private set; }
            public Node(T val) => Value = val;
        }
        public RecursiveList()
        {
            _root = new Node(default);
            _tail = _root;
        }
        public void Append(T val)
        {
            _tail.Next = new Node(val);
            _tail = _tail.Next;
        }
        private IEnumerable<T> TraverseInternal(Node node)
        {
            if (node == null) yield break;
            yield return node.Value;
            foreach (var t in TraverseInternal(node.Next)) yield return t;
        }
        private IEnumerable<T> InverseInternal(Node node)
        {
            if (node == null) yield break;
            foreach (var t in InverseInternal(node.Next)) yield return t;
            yield return node.Value;
        }
        public IEnumerable<T> Traverse => TraverseInternal(_root.Next);
        public IEnumerable<T> Inverse => InverseInternal(_root.Next);
        public void ForEach(Action<T> action)
        {
            if (action == null) return;
            foreach (T elem in Traverse) action(elem);
        }
    }
}
