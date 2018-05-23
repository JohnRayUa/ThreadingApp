using System;
using System.Threading;

namespace ThreadingApp
{
    class Program
    {
        static int X;
        static int Y;
        static bool exit = false;
        static AutoResetEvent ready;
        static Random rnd;
        static volatile ItemContainer items;

        static void Main(string[] args)
        {
            #region Set Variable
            do
            {
                ReadOnlyInt(ref X, "X");
                if (X > 64 || X == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The X is incorrect. Please enter X from 1 to 64");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (X > 64 || X == 0);

            ReadOnlyInt(ref Y, "Y");
            #endregion

            Thread[] threads = new Thread[X];
            items = new ItemContainer(Y);
            ready = new AutoResetEvent(true);
            rnd = new Random();

            Console.WriteLine("Count of thread : {0}, Container limit: {1}", X, Y);

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Adder);
                threads[i].IsBackground = true;
                threads[i].Start(i);
                Thread.Sleep(100);
            }


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Click Enter for Get Statistic");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();

            GetStatisticAndExit(threads);

        }

        private static void GetStatisticAndExit(Thread[] threads)
        {
            exit = true;
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("\tNumber\t|\tId\t|\tCount\t");
            Console.WriteLine("-----------------------------------------------------");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
                Console.WriteLine(String.Format("\t{0}\t|\t{1}\t|\t{2}\t", i, threads[i].ManagedThreadId.ToString(), items.GetCount(threads[i].ManagedThreadId)));
                Console.WriteLine("-----------------------------------------------------");

            }
            Console.WriteLine("Maximum of elements: {0}\n\n Click Enter fo exit", items.Maximum);
            Console.ReadLine();
            Console.ReadKey();
        }

        private static void Adder(object i)
        {
            int item = (int)i;

            for (; ; )
            {
                try
                {
                    if (exit)
                        break;

                    ready.WaitOne();

                    items.AddToContainer(Thread.CurrentThread.ManagedThreadId , rnd.Next(0, 1000));
                    ready.Set();

                    Thread.Sleep(rnd.Next(0, 1000));
                }
                catch (Exception err)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Error in thread {0} : {1}", item, err.Message);
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }
        }

        static void ReadOnlyInt(ref int value , string nameOfVariable)
        {
            string _val = "";
            Console.Write("Enter your {0}: " , nameOfVariable);
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    int val = 0;
                    bool _x = int.TryParse(key.KeyChar.ToString(), out val);
                    if (_x)
                    {
                        _val += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            int.TryParse(_val, out value);
            Console.WriteLine();
            Console.WriteLine("The {0} entered is : {1}", nameOfVariable , value);
        }

    }
}
