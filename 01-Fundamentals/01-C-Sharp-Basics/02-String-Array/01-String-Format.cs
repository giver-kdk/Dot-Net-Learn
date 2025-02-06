using System;

namespace OutputFormat
{
	public class Program
	{
		static void Main(string[] args)
		{
			float value = 1500.234675f;
			// Default currency is '$'
			Console.WriteLine("Currency Format: {0:c}", value);
			// Specify precision after 'c'
			Console.WriteLine("Currency Format: {0:c1}", value);
			Console.WriteLine("Currency Format: {0:c2}", value);
			Console.WriteLine("Currency Format: {0:c3}", value);

			Console.WriteLine();
			// 'n3' specifies thousand format with precision 3 
			Console.WriteLine("Thousand Format: {0:n3}", value);

			Console.WriteLine();
			Console.WriteLine("Float Format: {0:f3}", value);

			int integer = 25;
			Console.WriteLine();
			// 'd4' reserves 4 spaces for number and fills with '0' on left if space left
			Console.WriteLine("Padded Format: {0:d4}", integer);
		}
	}
}