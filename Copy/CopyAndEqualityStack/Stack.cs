using System;

namespace CopyAndEqualityStack
{
    internal sealed class Stack : ICloneable, IEquatable<Stack>
    {
        private object[] _items;
        public int Count { get; private set; }
        public Stack(int length = 0) => _items = new object[length == 0 ? 10 : length];
        public void Push(object item)
        {
            Grow();
            _items[Count] = item;
            Count++;
        }
        public object Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");
            return _items[Count - 1];
        }
        public object Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");
            var item = _items[Count - 1];
            _items[Count - 1] = default;
            Count--;
            return item;
        }
        public void Clear()
        {
            _items = new object[10];
            Count = 0;
        }
        private void Grow()
        {
            if (_items.Length >= Count + 1)
                return;
            var newLength = _items.Length * 2;
            Array.Resize(ref _items, newLength);
        }
        public override bool Equals(object obj) => Equals(obj as Stack);
        public bool Equals(Stack other)
        {
            if (ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            if (Count == 0 && other.Count == 0) return true;
            if (Count != other.Count) return false;
            if (other._items.Equals(_items)) return true;
            for (var i = 0; i < this.Count; i++)
            {
                if (!Equals(_items[i], other._items[i])) return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;
                const int hash = hashingBase;
                return (hash * hashingMultiplier) ^ (_items?.GetHashCode() ?? 0);
            }
        }
        public static bool operator ==(Stack stackA, Stack stackB)
        {
            if (ReferenceEquals(stackA, stackB))
                return true;
            return stackA?.Equals(stackB) == true;
        }
        public static bool operator !=(Stack stackA, Stack stackB) => !(stackA == stackB);
        public object Clone()
        {
            var ret = new Stack(_items.Length);
            for (var i = 0; i < Count; i++)
                ret.Push(this._items[i]);
            return ret;
        }
    }
}
