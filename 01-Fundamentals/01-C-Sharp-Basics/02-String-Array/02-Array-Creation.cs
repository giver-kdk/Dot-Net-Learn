using System;

namespace ArrayCreation
{
	public class Program
	{
		const int SIZE = 5;
		static void Main(string[] args)
		{
			// Declare array with 'new' because in C#, data types are just classes
			int[] numbers = new int[SIZE];			
			string[] names = new string[SIZE];			
			float[] heights = new float[SIZE];			
			// Define array
			numbers[0] = 1;
			numbers[1] = 2;
			numbers[2] = 3;
			numbers[3] = 4;
			numbers[4] = 5;

			// Accessing array length
			Console.WriteLine("Array Length: {0}", numbers.Length);

			// Accessing array elements
			for(int i = 0; i < SIZE; i++)
			{
				Console.WriteLine("numbers[{0}]: {1}", i, numbers[i]);
				// Array value can also be accessed using 'GetValue()' method
				// Console.WriteLine("numbers[{0}]: {1}", i, numbers.GetValue(i));
			}

			// Declaration and Definition of Array 
			string[] countries = {"Nepal", "India", "China", "Turkey", "America"}; 
			// Array of any data type, but elements should be of similar data type
			var marks = new[] {80, 60, 50, 70, 45};
			var vehicles = new[] {"Car", "Bike", "Cycle", "Van", "Plane"};
			// var[] vehicles = {"Car", "Bike"}      // This is not valid in case of 'var'

			// Array of any data type and varying elements data type
			object[] person = {"Giver", 21, "Nepal", 5.6, "August 14 2002"};
			Console.WriteLine();
			for(int i = 0; i < person.Length; i++)
			{
				Console.WriteLine(person[i]);
			}
		}
	}
}