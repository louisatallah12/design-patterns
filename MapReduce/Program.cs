using System;
using System.IO;
using System.Collections.Generic;

namespace MapReduce
{
    class Program
    {
        static void Main(string[] args)
        {
            WordReducer<string> entier = new WordReducer<string>();
            StreamReader  file = new StreamReader("file.txt");
            string f = file.ReadLine();
            entier.mapReduce(f);


            
            foreach (KeyValuePair<string, int> kvp in entier.wordStore)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }
            
            Console.ReadKey();
        }
    }
}
