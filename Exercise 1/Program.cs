using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        
        
        {
            Node<int> n1 = new Node<int>(1, null);
            Node<int> n2 = new Node<int>(2, null);
            CustomQueue<int> q = new CustomQueue<int>();
            q.Enqueue(n1);
            q.Enqueue(n2);
            q.Enqueue(new Node<int>(3, null));
            q.Enqueue(new Node<int>(4, null));
            q.PrintQueue();
            q.Dequeue();
            Console.Write("After the dequeue : ");
            q.PrintQueue();
            foreach(int n in q)
            {
                Console.Write(n +" -> ");
            }
            Console.WriteLine("X");
            Console.ReadKey();
        }
    }
}
