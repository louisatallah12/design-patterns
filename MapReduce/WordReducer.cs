using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections;
using System.Text;
using System.Threading;

namespace MapReduce
{
    public class WordReducer<T>
    {
        public static ConcurrentBag<string> wordBag = new ConcurrentBag<string>();
        public BlockingCollection<string> wordChunks = new BlockingCollection<string>(wordBag);

        /// 

        /// 1.  Produce 250 character or less chunks of text.
        /// 2.  Break chunks on the first space encountered before 250 characters.
        /// 
        /// 
        /// 
        public IEnumerable<string> produceWordBlocks(T fileText)
        {
            
            int blockSize = 250;
            int startPos = 0;
            int len = 0;
            string f = fileText.ToString();
            for (int i = 0; i < f.Length; i++)
            {
                i = i + blockSize > f.Length - 1 ? f.Length - 1 : i + blockSize;

                while (i >= startPos && f[i] != ' ')
                {
                    i--;
                }

                if (i == startPos)
                {
                    i = i + blockSize > (f.Length - 1) ? f.Length - 1 : i + blockSize;
                    len = (i - startPos) + 1;
                }
                else
                {
                    len = i - startPos;
                }

                yield return f.Substring(startPos, len).Trim();
                startPos = i;
            }
        }


        public void mapWords(T fileText)
        {
            Parallel.ForEach<string>(produceWordBlocks(fileText), wordBlock =>
            {   //split the block into words
                string m  = wordBlock.ToString();
                string[] words = m.Split(' ');
                StringBuilder wordBuffer = new StringBuilder();

                //cleanup each word and map it
                foreach (string word in words)
                {   //Remove all spaces and punctuation
                    foreach (char c in word)
                    {
                        if (char.IsLetterOrDigit(c) || c == '\'' || c == '-')
                            wordBuffer.Append(c);
                    }
                    //Send word to the wordChunks Blocking Collection
                    if (wordBuffer.Length > 0)
                    {
                        wordChunks.Add(wordBuffer.ToString());
                        wordBuffer.Clear();
                    }
                }
            });

            wordChunks.CompleteAdding();
        }

        public ConcurrentDictionary<string,int> wordStore = new ConcurrentDictionary<string,int>();

        public void reduceWords()
        {
            Parallel.ForEach(wordChunks.GetConsumingEnumerable(), word =>
            {   //if the word exists, use a thread safe delegate to increment the value by 1
                //otherwise, add the word with a default value of 1
                wordStore.AddOrUpdate(word, 1, (key, oldValue) => Interlocked.Increment(ref oldValue));
            });
        }

        public void mapReduce(T fileText)
        {   //Reset the Blocking Collection, if already used
            if (wordChunks.IsAddingCompleted)
            {
                wordBag = new ConcurrentBag<string>();
                wordChunks = new BlockingCollection<string>(wordBag);
            }

            //Create background process to map input data to words
            System.Threading.ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                mapWords(fileText);
            });

            //Reduce mapped words
            reduceWords();
        }
        
        public override string ToString()
        {
            return this as string;
        }
    }

    
}
