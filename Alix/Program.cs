using System;

namespace Alix
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomQueue<string> waiters = new CustomQueue<string>();

            waiters.Enqueue("Renard");
            waiters.Enqueue("Gonzales");
            waiters.Enqueue("Wooky");
            waiters.Enqueue("Stephane");
            waiters.Enqueue("Carlito");
            waiters.Enqueue("Pierre");
            waiters.Enqueue("Gilbert");




            foreach (string waiter in waiters)
            {
                Console.WriteLine(waiter);
            }

            waiters.Dequeue();
            Console.WriteLine("\nAprès le Dequeue :");
            foreach (string waiter in waiters)
            {
                Console.WriteLine(waiter);
            }

            Console.ReadKey();
        }
    }
}
