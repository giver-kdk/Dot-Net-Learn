using System;
using System.Threading.Tasks;

namespace AsyncAwaitApp
{
    public class Program
    {
        public static async Task MethodA()
        {
            await Task.Run(() => {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("Method A");
                }
            });
        }
        public static void MethodB()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Method B");
            }
        }
        static void Main(string[] args)
        {
            MethodA();
            MethodB();
        }
    }
}