using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProduceAndConsumeList
{
    class Program
    {
        static void Main(string[] args)
        {
            int producerNum = 2;
            int consumerNum = 3;

            LetSynchronized shareData = new LetSynchronized();

            // The producer
            Thread[] producerThread = new Thread[producerNum];
            for (int i = 0; i < producerThread.Length; i++)
            {
                Producer producer = new Producer(shareData);
                producerThread[i] = new Thread(new ThreadStart(producer.Produce));
                producerThread[i].Name = "producer_" + i;
            }

            // The Consumer
            Thread[] consumerThread = new Thread[consumerNum];
            for (int i = 0; i < consumerThread.Length; i++)
            {
                Consumer consume = new Consumer(shareData);
                consumerThread[i] = new Thread(new ThreadStart(consume.Consume));
                consumerThread[i].Name = "consume_" + i;
            }

            producerThread[0].Priority = ThreadPriority.AboveNormal;
            producerThread[1].Priority = ThreadPriority.BelowNormal;

            for (int i = 0; i < producerThread.Length; i++)
            {
                producerThread[i].Start();
            }

            consumerThread[0].Priority = ThreadPriority.Highest;
            consumerThread[1].Priority = ThreadPriority.AboveNormal;
            consumerThread[2].Priority = ThreadPriority.Normal;
            for (int i = 0; i < consumerThread.Length; i++)
            {
                consumerThread[i].Start();
            }

            Thread.Sleep(1000);
            Console.ReadKey();
        }
    }

    public class LetSynchronized
    {
        private List<int> bufferList = new List<int>();
        private int bufferListCount = 6;

        private int produceCount = 0;
        public int ProduceCount
        {
            get { return produceCount; }
            set { produceCount = value; }
        }

        private int consumeCount = 0;
        public int ConsumeCount
        {
            get { return consumeCount; }
            set { consumeCount = value; }
        }

        public LetSynchronized() { }

        public int getBuffer()
        {
            int readValue = 0;
            if (bufferList.Count <= 0)
            {
                Console.WriteLine("The buffer have no data,please wait a moment");
                Console.WriteLine();
                Monitor.Wait(this);
            }
            else
            {
                readValue = bufferList[bufferList.Count - 1];
                bufferList.RemoveAt(bufferList.Count - 1);

                Console.WriteLine("The buffer have:{0}", bufferList.Count);
                Thread.Sleep(500);
                Monitor.Pulse(this);
            }
            return readValue;
        }

        public bool setBuffer(int writeValue)
        {
            bool flag = true;
            if (bufferList.Count >= bufferListCount)
            {
                Console.WriteLine("The buffer is full,please wait a moment");
                Console.WriteLine();
                flag = false;
                Monitor.Wait(this);
            }
            else
            {
                bufferList.Add(writeValue);
                Console.WriteLine("The buffer have:{0}", bufferList.Count);
                Thread.Sleep(500);
                Monitor.Pulse(this);
            }

            return flag;
        }
    }

    public class Producer
    {
        public LetSynchronized shareDate;
        public Producer(LetSynchronized shareDate)
        {
            this.shareDate = shareDate;
        }

        public void Produce()
        {
            while (true)
            {
                lock (shareDate)
                {
                    if (shareDate.ProduceCount >= 50)
                    {
                        Console.WriteLine("The produce is full:{0}", shareDate.ProduceCount);
                        Console.WriteLine();
                        break;
                    }

                    if (shareDate.setBuffer(1))
                    {
                        shareDate.ProduceCount++;

                        Console.WriteLine("set:");

                        string name = Thread.CurrentThread.Name;
                        Console.WriteLine(name);
                        Console.WriteLine();
                    }
                }
            }
        }
    }

    public class Consumer
    {
        public LetSynchronized shareDate;
        public Consumer(LetSynchronized shareDate)
        {
            this.shareDate = shareDate;
        }

        public void Consume()
        {
            while (true)
            {
                lock (shareDate)
                {
                    if (shareDate.ConsumeCount >= 50)
                    {
                        Console.WriteLine("The consume is full:{0}", shareDate.ConsumeCount);
                        Console.WriteLine();
                        break;
                    }

                    int value = 0;
                    value = shareDate.getBuffer();
                    if (value == 1)
                    {
                        shareDate.ConsumeCount++;
                        Console.WriteLine("get:" + value);

                        string name = Thread.CurrentThread.Name;
                        Console.WriteLine(name);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
