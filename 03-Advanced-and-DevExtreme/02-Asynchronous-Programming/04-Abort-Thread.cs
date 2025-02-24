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
		public static void Main(string[] args)
		{
			Console.WriteLine("Start main thread");
			// Default 1st thread is always main thread
			Thread countUp = new Thread(Print1);            // Secondary or Worker Thread

			countUp.Start();

			if (countUp.IsAlive)
			{
				// Stops thread 
				countUp.Interrupt();
			}
		}
	}
}
