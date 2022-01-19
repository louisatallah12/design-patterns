using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Node <T> 
    {
        private T content;
        private Node <T> next;
        public Node()
        {
            this.content = default(T);
            this.next = null;
        }
        public Node(T Content, Node<T> Next)
        {
            this.content = Content;
            this.next = Next;
        }
        public Node <T> Next
        {
            get { return this.next; }
            set { this.next = value; }
        }
        public T Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        public override string ToString()
        {
            return this.content + " -> ";
        }
        
    }
}
