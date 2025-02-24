using System;
using System.Threading;             // 'Thread' class located here

namespace ThreadApp
{
	public class Program
	{
		public static void Print1()
		{
			for (int i = 0; i <= 10; i++)
			{
				Console.WriteLine(i);
				// Holds currently runnign thread for specified durations
				Thread.Sleep(500);
			}
		}
		public static void Print2()
		{
			for (int i = 10; i >= 0; i--)
			{
				Console.WriteLine(i);
				Thread.Sleep(500);
			}
		}
		public static void Main(string[] args)
		{
			Console.WriteLine("Start main thread");
			// Default 1st thread is always main thread
			Thread countUp = new Thread(Print1);			// Secondary or Worker Thread
			Thread countDown = new Thread(Print2);

			// Both threads runs the method concurrently here
			Console.WriteLine("Thread Status: " + countUp.ThreadState);
			Console.WriteLine("Thread Alive: " + countUp.IsAlive);
			countUp.Start();
			countDown.Start();
			Console.WriteLine("Thread Status: " + countUp.ThreadState);
			Console.WriteLine("Thread Alive: " + countUp.IsAlive);
		}
	}
}
