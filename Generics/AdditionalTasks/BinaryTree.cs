using System;

namespace AdditionalTasks
{
    [Serializable]
    internal class Node<T> where T : IComparable<T>
    {
        public static bool operator >(Node<T> lhs, Node<T> rhs) => lhs.Item.CompareTo(rhs.Item) > 0;
        public static bool operator <(Node<T> lhs, Node<T> rhs) => lhs.Item.CompareTo(rhs.Item) < 0;
        public Node()
        {
            RightNode = LeftNode = null;
            Item = default;
        }
        public Node(T item)
        {
            RightNode = LeftNode = null;
            Item = item;
        }
        public Node(T item, Node<T> right, Node<T> left)
        {
            RightNode = right;
            LeftNode = left;
            Item = item;
        }
        public Node<T> LeftNode;
        public Node<T> RightNode;
        public T Item;
    }
    public class BinaryTre<T> where T : IComparable<T>
    {
        private readonly Node<T> _root;
        public BinaryTre() => _root = new Node<T>();
        public void Add(params T[] items)
        {
            foreach (T t in items) 
                Add(t);
        }
        public void Add(T item) => Add(new Node<T>(item), _root);
        private static void Add(Node<T> newNode, Node<T> root)
        {
            if (newNode > root)
            {
                if (root.RightNode == null)
                {
                    root.RightNode = newNode;
                    return;
                }
                Add(newNode, root.RightNode);
            }

            if (newNode < root)
            {
                if (root.LeftNode == null)
                {
                    root.LeftNode = newNode;
                    return;
                }
                Add(newNode, root.LeftNode);
            }
        }
        public void TraceTree()
        {
            Console.WriteLine("Trace In-Order");
            TraceInOrder(_root.RightNode);
            Console.WriteLine("Trace Post-Order");
            TracePostOrder(_root.RightNode);
        }

        private static void TraceInOrder(Node<T> root)
        {
            if (root.LeftNode != null) TraceInOrder(root.LeftNode);
            Console.WriteLine(root.Item.ToString());
            if (root.RightNode != null) TraceInOrder(root.RightNode);
        }
        private static void TracePostOrder(Node<T> root)
        {
            Console.WriteLine(root.Item.ToString());
            if (root.LeftNode != null) TracePostOrder(root.LeftNode);
            if (root.RightNode != null) TracePostOrder(root.RightNode);
        }
    }
}
