using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class CustomQueue<T> : IEquatable<T>
    {
        private Node<T> start;
        private Node<T> end;

        public CustomQueue()
        {
            this.start = null;
            this.end = null;
        }
        public CustomQueue(Node<T> Start, Node<T> End)
        {
            this.start = Start;
            this.end = End;
        }
        public Node<T> Start
        {
            get { return this.start; }
            set { this.start = value; }
        }
        public Node<T> End
        {
            get { return this.end; }
            set { this.end = value; }
        }
        public bool EmptyQueue()
        {
            return this.start == null && this.end == null;
        }
        public void Enqueue(Node<T> n)
        {
            if (EmptyQueue())
            {
                this.start = n;
                this.end = n;
            }
            else
            {
                n.Next = this.start;
                this.start = n;
            }
        }
        public void PrintQueue()
        {
            if (EmptyQueue())
            {
                Console.WriteLine("Empty Queue");
            }
            else
            {
                Node<T> n = this.start;
                while (n != null)
                {
                    Console.Write(n);
                    n = n.Next;
                }
                Console.WriteLine(" X ");
            }
        }
        public void Dequeue()
        {
            if (EmptyQueue())
            {
                Console.WriteLine("Empty Queue");
            }
            else
            {
                Node<T> n = this.start;
                if (n == this.end)
                {
                    n = null;
                }
                while (n.Next != this.end)
                {
                    n = n.Next;
                }
                this.end = n;
                n.Next = null;  
            }
        }
        public int getLength()
        {
            int count = 1;

            if (this.start == null)
            {
                return 0;
            }
            else
            {

                Node<T> curr = this.start;
                while (curr.Next != null)
                {
                    curr = curr.Next;
                    count++;
                }
            }
            return count;
        }
        public Node<T> getElementByIndex(int index)
        {
            if (index >= getLength())
            {
                throw new Exception("Not a valid index");
            }
            int i = 0;
            Node<T> curr = this.start;
            while (i < index)
            {
                curr = curr.Next;
            }
            return curr;
        }
        public bool Equals(T n)
        {
            if (n == null)
            {
                return false;
            }
            return this.GetHashCode() == n.GetHashCode();
        }

        // implementation for the foreach loop 
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.start;

            while (current != null)
            {
                yield return current.Content;
                current = current.Next;
            }
        }

    }
}
