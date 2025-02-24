using System;
using System.Threading;             // 'Thread' class located here

// Primary thread is resposible for executing code through main program
// Secondary thread is any other thread except primary thread

// Foreground Thread runs in foreground and doesn't let the application to close until it stops
// Background Thread runs in background until app is running and it stops when the app closes
// 'Thread' class threads are forground threads by default. But, it can be changed to background

// Thread allows us to run multiple pieces of code by sharing CPU resource
// Threads execution is shceduled by the OS
// Higher the number of processors, higher number of threads it can handle
namespace ThreadApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Default 1st thread is always main thread
			Thread mainThread = Thread.CurrentThread;
			mainThread.Name = "main";

			Console.WriteLine(mainThread.Name);
			// Main thread is a foreground thread
			Console.WriteLine(mainThread.IsBackground);             // Output: False
			Console.WriteLine(mainThread.Priority);                 // Output: Normal
			Console.WriteLine(mainThread.ManagedThreadId);          // Output: 1
			Console.WriteLine(mainThread.ThreadState);              // Output: Running
			Console.WriteLine(mainThread.IsAlive);                  // Output: True
		}
	}
}
