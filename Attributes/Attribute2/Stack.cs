namespace Attributes.Attribute2
{
    internal class Stack
    {
        private readonly int[] _data = new int[10];
        private int _top;
        [Abbreviation("+ Push")]
        public void Push(int value) => _data[_top++] = value;
        [Abbreviation("+ Pop")]
        public int Pop() => _data[--_top];
    }
}
