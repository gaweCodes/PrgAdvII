#define tracing

namespace Attributes.Attribute1 
{
    internal class Stack
    {
        private readonly int[] _data = new int[16];
        public int Size { get; private set; }
        private void Dump()
        {
            for (var i = 0; i < Size; i++)
                Trace.Write(_data[i] + " ");
            Trace.WriteLine();
        }
        public void Push(int x)
        {
            Trace.Write("Push(" + x + "): ");
            if (Size < 16)
            {
                _data[Size] = x;
                Size++;
            }
            else
                throw new OverflowException();
            Dump();
        }
        public int Pop()
        {
            Trace.Write("Pop(): ");
            if (Size == 0) throw new UnderflowException();
            Size--;
            Dump();
            return _data[Size];
        }
    }
}
