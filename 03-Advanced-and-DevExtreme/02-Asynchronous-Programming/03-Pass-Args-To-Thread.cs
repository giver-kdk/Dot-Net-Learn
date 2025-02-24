using System;
using System.Threading;             // 'Thread' class located here

namespace ThreadApp
{
	public class Program
	{
		public static void Print1(int max)
		{
			for (int i = 0; i <= max; i++)
			{
				Console.WriteLine(i);
				// Holds currently runnign thread for specified durations
				Thread.Sleep(500);
			}
		}
		public static void Print2(int max)
		{
			for (int i = max; i >= 0; i--)
			{
				Console.WriteLine(i);
				Thread.Sleep(500);
			}
		}
		public static void Main(string[] args)
		{
			Console.WriteLine("Start main thread");
			// Pass argument to thread's method usign lambda expression
			Thread countUp = new Thread(() => Print1(5));
			Thread countDown = new Thread(() => Print2(5));

			// Both threads runs the method concurrently here
			countUp.Start();
			countDown.Start();
		}
	}
}
