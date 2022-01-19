using System;
using System.Collections.Generic;
using System.Text;

namespace Alix
{
    public class Node<T>
    {
        public Node(T content)
        {
            Next = null;
            Content = content;
        }

        public Node<T> Next { get; set; }
        public T Content { get; set; }
    }
}
