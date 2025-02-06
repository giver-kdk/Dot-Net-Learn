using System;

namespace NullableTypes
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// int a = null;			// Not Allowed
			// Use '?' symbol to explicitely make variables nullable
			int? a = null;
			if (a == null) Console.WriteLine("Variable is Nullable");
			else Console.WriteLine("Variable is not Nullable");
		}
	}
}