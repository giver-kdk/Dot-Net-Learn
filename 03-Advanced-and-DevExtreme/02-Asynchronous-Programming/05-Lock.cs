using System;
using System.Threading;             // 'Thread' class located here

// Lock allows resources(Method, File, etc.) to be accessed by one thread at a time
namespace LockApp
{
	// In a bank one transaction should occur at a time
	// Other transaction can't be done before previous transaction is completed/aborted
	public class Bank
	{
		// Object used as identifier for specific lock 
		public static object lockObj = new object();

		public static double Balance = 10;

		public static void Withdraw(double amount)
		{
			// We lock the transaction so that if multiple people/threads try to do transaction,
			// Only one person/thread can do the transaction at a time

			// One thread locks the lock when entering this region and unlocks when leaving
			// If other thread see the status as locked, then it waits until it unlocks
			lock (lockObj)
			{
				Console.WriteLine("The lock is locked!");

				// Let's assume it takes some time for transaction
				Thread.Sleep(500);
				if (amount > Balance)
				{
					Console.WriteLine("Insufficient Balance!");
				}
				else
				{
					Balance -= amount;                  //  Reduce balance for transaction
					Console.WriteLine("Withdrawn Amount: " + amount);
					Console.WriteLine("New Balance: " + Balance);
				}

				Console.WriteLine("The lock is unlocked!\n");
			}
		}
	}
	public class Program
	{
		public static void Main(string[] args)
		{
			// Multiple threads trying to withdraw money from the bank simultaneously
			Thread[] threads = new Thread[15];

			// Create threads
			for (int i = 0; i < 15; i++)
			{
				// Each thread is supposed to withdraw amount 1 from the bank
				threads[i] = new Thread(() => Bank.Withdraw(1));
			}

			// Run threads
			for (int i = 0; i < 15; i++)
			{
				// If there was no lock, all threads might access it randomly
				// And mess the transaction
				threads[i].Start();
			}
		}
	}
}
