using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW29052019_Thread
{
    class Program
    {
        static List<int> numbers = new List<int>(1000);
        static void Main(string[] args)
        {
            //Question 1

            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int n1 = r.Next(9);
                int n2 = r.Next(9);
                Console.Write($"{n1} X {n2} = ");
                Thread t = new Thread(FiveSeconds);
                t.Start();
                int result = Convert.ToInt32(Console.ReadLine());
                CheckResult(ref n1, ref n2, ref result, ref t);

            }

            //Question 2

            Stopwatch sp = new Stopwatch();
            sp.Start();

            //Fast Way Of Threads
            List<Thread> threads = new List<Thread>(10);
            for (int i = 0; i < threads.Capacity; i++)
            {
                threads.Add(new Thread(Set100ItemsInList));
                threads[i].Start(i * 100 + 1);
                threads[i].Join();
                sp.Stop();
                Console.WriteLine(sp.ElapsedMilliseconds);

            }

            //Slow Way
            for (int i = 0; i < 10; i++)
            {
                Set100ItemsInList(i * 100 + 1);
            }
            Console.WriteLine($"Total: {sp.ElapsedMilliseconds}");
           
        }
        private static void FiveSeconds()
        {
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i+1}");
                Thread.Sleep(1000);
            }
            Thread.CurrentThread.Abort();
        }
        static void CheckResult(ref int n1, ref int n2, ref int result, ref Thread t)
        {
            if(n1 * n2 == result && t.IsAlive == true)
            {
                Console.WriteLine("You Are Great!!\nHere new Exerice for you:\n");
            }
            if (n1 * n2 == result && t.IsAlive == false)
            {
                Console.WriteLine("You Rigth, But Your Time Is over... Try Again\n");
            }
            if(n1 * n2 != result)
            {
                Console.WriteLine("Sorry, But Your Answer Is Uncorrect! Try New Exerice:\n");
            }
        }
        static void Set100ItemsInList(object obj)
        {
            int num = (int)obj;
            for (int i = num; i < num+100; i++)
            {
                numbers.Add(1);
                Console.WriteLine($"{i}: {numbers[i - 1]}");

            }
        }
    }
}
