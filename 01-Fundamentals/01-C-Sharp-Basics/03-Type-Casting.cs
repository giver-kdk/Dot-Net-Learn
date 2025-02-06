using System;

namespace TypeCasting
{
	// 'Class' is also a type
	public class MyClass
	{
		public string name;
	}

	public class Program
	{
		static void Main(string[] args)
		{
			// Implicit Type Conversion: It is automatic and has no data loss
			// Data type converts from: Small -> Large
			int n1 = 5;
			double n2 = n1;					// Int -> Double
			Console.WriteLine(n2);

			// Explicit Type Conversion: It is manual and has data loss
			// Date type converts form: Large -> Small
			double n3 = 10.53d;
			int n4 = (int) n3;				// Double -> Int
			Console.WriteLine(n4);

			// Converting to custom Class type
			object myObj1 = new MyClass();
			MyClass myObj2 = (MyClass) myObj1;
			myObj2.name = "Giver";
			Console.WriteLine("Object to Class, Name: {0}", myObj2.name);

			// Type Conversion Methods
			// If the data-type values are in string, then we can convert as follows:
			int n5 = int.Parse("100");
			bool n6 = bool.Parse("true");
			float n7 = float.Parse("4.2");
			// Passing variable as argument
			string value1 = "5.62";
			double n8 = double.Parse(value1);
			Console.WriteLine("String to Data Type");
			Console.WriteLine(n5);
			Console.WriteLine(n6);
			Console.WriteLine(n7);
			Console.WriteLine(n8);

			// Other data-type to string
			string value2 = n5.ToString();
			string value3 = n6.ToString();
			string value4 = n7.ToString();
			Console.WriteLine("Data Type to String");
			Console.WriteLine(value2);
			Console.WriteLine(value3);
			Console.WriteLine(value4);

			// Accessing data type of variable
			Console.WriteLine("'n1' variable is of data type: {0}", n1.GetType());
			Console.WriteLine("'value2' variable is of data type: {0}", value2.GetType());
		}
	}
}