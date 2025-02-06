using System;

namespace ExceptionHandle
{
	public class Program
	{
		static void Divide(int a, int b)
		{
			try
			{
				// 'DivideByZeroException()' is a Class. So, use 'new'
				if(b == 0) throw new DivideByZeroException();
				else Console.WriteLine("Division is: {0}", a/b);
			}
			// Only one 'catch' runs among these
			// Here, 'DivideByZeroException' is a type for error
			catch(DivideByZeroException err)
			{
				Console.WriteLine(err.Message);
				Console.WriteLine(err.GetType().Name);
			}
			// Here, 'Exception' is a type for error
			catch(Exception err)
			{
				// Default Exception
				Console.WriteLine("Default Error Message");
				Console.WriteLine(err.Message);
				Console.WriteLine(err.GetType().Name);
			}
			// Runs anyway
			finally
			{
				Console.WriteLine("Division Finished!");
			}
		}
		static void Main(string[] args)
		{
			int a = 5, b = 0;
			Divide(a, b);
		}
	}
}