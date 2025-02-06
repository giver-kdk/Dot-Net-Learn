/*
	- We know 'reference types' makes the original variable modified anyhow. 
	- To modify original variable of 'value type', use 'ref' or 'out' keyword
*/
using System;

namespace PassRef
{
	public class Program
	{
		// Using 'ref'
		static void Modify(ref int i)
		{
			i *= 2;
			Console.WriteLine("Modified i = {0}", i);
		}


		// 'out' used when method returns multiple values and 
		// Variable can declared on time of calling the method
		public static string Increment(out int num)
		{
			num = 1;        // Needs to be initialized inside method
			num++;          // Equivalent to returning one thing
							// Returning another thing
			return String.Format("Increment is {0}", num);
		}


		// Using 'ref readonly' passes variable reference but we can't modify it
		public static string PrintInfo(ref readonly int a)
		{
			return String.Format("Value of a is {0}", a);
		}


		// Using 'in' also passes readonly reference.
		// New thing is it creates temporary variable for passed argument
		public static string PrintData(in int b)
		{
			return String.Format("Value of b is {0}", b);
		}


		static void Main(string[] args)
		{
			// Using 'ref' parameter
			int i = 10;
			Console.WriteLine("Before Modification,  i = {0}", i);
			// Passing reference of variable as argument
			Modify(ref i);
			Console.WriteLine("After Modification,  i = {0}", i);


			// Using 'out' parameter.
			string result = Increment(out int n);       // Declaring 'out' parameter during function call
			Console.WriteLine(result);
			// NOTICE that we can access 'n' declared on 'Increment()' function call. 
			// This function is the scope of 'n'
			Console.WriteLine("Value of n: {0}", n);


			// Using 'ref readonly' parameter
			int a = 3;
			Console.WriteLine(PrintInfo(ref a));


			// Using 'in' parameter
			int b = 3;
			Console.WriteLine(PrintData(in b));
		}
	}
}
