using System;

namespace Generics.Ueberblick {
    public class Buffer {
        object[] items;

        public void Push(object item) {
            /* ... */
        }

        public object Pop() {
            /* ... */
            return null;
        }
    }

    public class Buffer<T> {
        T[] items;

        public void Push(T item) {
            /* ... */
        }

        public T Pop() {
            /* ... */
            return default(T);
        }
    }

    public class Buffer<TElement, TPriority> {
        TElement[] items;
        TPriority[] priorities;

        public void Push(TElement item, TPriority prio) {
            /* ... */
        }
    }

    class Examples {
        public void Test() {
            Buffer buffer = new Buffer();
            buffer.Push(1); // Boxing: Performance-Verlust
            buffer.Push(2);
            int i = (int) buffer.Pop(); // Casting: Performance-Verlust

            buffer.Push(3);
            buffer.Push("Hello");
        }

        public void TestGeneric() {
            Buffer<int> buffer = new Buffer<int>();
            buffer.Push(1); // Kein Boxing
            buffer.Push(2);
            int i = buffer.Pop(); // Kein Casting

            buffer.Push(3);
            //Buffer.Push("Hello"); // Compilerfehler
        }

        public void TestGenericMultiple() {
            Buffer<int, int> b1 = new Buffer<int, int>();
            b1.Push(1, 3);
            b1.Push(2, 1);

            Buffer<string, float> b2 = new Buffer<string, float>();
            b2.Push("Hello", 0.3f);
            b2.Push("World", 0.2f);
        }
    }
}