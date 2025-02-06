using System;

namespace CustomExceptionApp
{
	public class InvalidAge : Exception
	{
		public InvalidAge() { }
		public InvalidAge(string msg) : base(msg) { }
	}
	public class Program
	{
		public static void CheckAge(int age)
		{
			try
			{
				if (age < 16)
				{
					throw new InvalidAge("Age should be above 16");
				}
				else
				{
					Console.WriteLine("Valid Age is: " + age);
				}
			}
			catch (InvalidAge exp)
			{
				Console.WriteLine(exp.Message);
			}
			finally
			{
				Console.WriteLine("Age Checking Completed!");
			}

		}
		public static void Main(string[] args)
		{
			int age1 = 15;
			int age2 = 20;

			CheckAge(age1);
			CheckAge(age2);
		}
	}
}