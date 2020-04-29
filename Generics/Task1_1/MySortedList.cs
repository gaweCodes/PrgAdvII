using System;

namespace Task1_1
{
    internal class MySortedList<T> : MyList<T> where T : IComparable<T>
    {
        public void BubbleSort()
        {
            if (Head?.Next == null)
                return;
            bool swapped;
            do
            {
                Node previous = null;
                var curr = Head;
                swapped = false;
                while (curr.Next != null)
                {
                    if (!(curr.Data is IComparable<T> comp))
                        throw new Exception("data-object must implement ICOmparable");

                    if (comp.CompareTo(curr.Next.Data) > 0)
                    {
                        var tmp = curr.Next;
                        curr.Next = curr.Next.Next;
                        tmp.Next = curr;
                        if (previous == null)
                            Head = tmp;
                        else
                            previous.Next = tmp;
                        previous = tmp;
                        swapped = true;
                    }
                    else
                    {
                        previous = curr;
                        curr = curr.Next;
                    }
                }
            } while (swapped);
        }
    }
}
