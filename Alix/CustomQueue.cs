using System;
using System.Collections.Generic;
using System.Text;

namespace Alix
{
    public class CustomQueue<T>
    {

        public CustomQueue()
        {
            first = null;
            length = 0;
        }
        public uint Length() => length;

        public void Enqueue(T value)
        {
            length += 1;
            Node<T> node = new Node<T>(value);
            node.Next = first;
            first = node;
        }

        public void Dequeue()
        {
            if (isEmpty())
            {
                return;
            }
            first = first.Next;
            length -= 1;
        }

        public bool isEmpty()
        {
            return length == 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = first;

            while (current != null)
            {
                yield return current.Content;
                current = current.Next;
            }
        }

        private Node<T> first;
        private uint length;
    }
}
