using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerAndConsumer
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
        private int[] buffer = { 0, 0, 0, 0, 0, 0 };

        private int bufferCount = 0;

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

        private int readIndex = 0, writeIndex = 0;

        public LetSynchronized() { }

        public int getBuffer()
        {
            int readValue = 0;
            lock (this)
            {
                if (bufferCount <= 0)
                {
                    Console.WriteLine("The buffer have no data,please wait a moment");
                    Monitor.Wait(this);
                }
                else
                {
                    readValue = buffer[readIndex];
                    bufferCount--;
                    readIndex = (readIndex + 1) % buffer.Length;
                    Console.WriteLine("The buffer have:{0}", bufferCount);

                    Monitor.Pulse(this);
                    
                }
                return readValue;
            }
        }

        public void setBuffer(int writeValue)
        {
            lock (this)
            {
                if (bufferCount >= buffer.Length)
                {
                    Console.WriteLine("The buffer is full,please wait a moment");
                    Monitor.Wait(this);
                }
                else
                {
                    buffer[writeIndex] = writeValue;
                    bufferCount++;
                    writeIndex = (writeIndex + 1) % buffer.Length;
                    Console.WriteLine("The buffer have:{0}", bufferCount);
                    Monitor.Pulse(this);
                }
            }
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
                    if (shareDate.ProduceCount >= 70)
                    {
                        Console.WriteLine("The produce is full:{0}", shareDate.ProduceCount);
                        break;
                    }
                    else
                    {
                        shareDate.setBuffer(1);
                        shareDate.ProduceCount++;

                        Console.WriteLine("set the data to buffer:");

                        string name = Thread.CurrentThread.Name;
                        Console.WriteLine(name + " done producing");
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
                    if (shareDate.ConsumeCount >= 70)
                    {
                        Console.WriteLine("The consume is full:{0}", shareDate.ConsumeCount);
                        break;
                    }
                    else
                    {
                        int value = 0;
                        value = shareDate.getBuffer();
                        shareDate.ConsumeCount++;
                        Console.WriteLine("get the data from buffer:" + value);

                        string name = Thread.CurrentThread.Name;
                        Console.WriteLine(name + " done consuming");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
