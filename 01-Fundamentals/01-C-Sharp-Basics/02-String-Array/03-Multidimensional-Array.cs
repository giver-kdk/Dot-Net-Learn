using System;

namespace Multidimensional
{
	public class Program
	{
		static void Main(string[] args)
		{
			// 2 commas means a 3D array
			string[,,] names = {
				{
					{"Ram", "Shyam", "Hari"},
					{"Rita", "Sita", "Gita"},
					{"Gopal", "Krishna", "Gadesh"}
				},
				{
					{"Alex", "James", "Smith"},
					{"Clara", "Marie", "John"},
					{"Ben", "Alita", "Jordan"}
				}
			};

			// Here, 'array.Rank' gives total number of dimesions in the array
			Console.WriteLine("Total Dimension: {0}", names.Rank);
			Console.WriteLine("Dimension Length: {0}", names.GetLength(0));
			Console.WriteLine("Dimension Length: {0}", names.GetLength(1));
			Console.WriteLine("Dimension Length: {0}", names.GetLength(2));

			// 3D Array Traversal 
			for(int i = 0; i < names.GetLength(0); i++)
			{
				for(int j = 0; j < names.GetLength(1); j++)
				{
					for(int k = 0; k < names.GetLength(2); k++)
					{
						Console.Write("{0}\t", names[i, j, k]);
					}
					Console.Write("\t");
				}
				Console.WriteLine();
			}

			// 3 commas means 4D array
			int[,,,] nums = {
				{
					{
						{1, 2, 3},
						{4, 5, 6},
						{7, 8, 9}
					},
					{
						{7, 8, 9},
						{1, 2, 3},
						{4, 5, 6}
					}
				},
				{
					{
						{7, 8, 9},
						{1, 2, 3},
						{4, 5, 6}
					},
					{
						{1, 2, 3},
						{7, 8, 9},
						{4, 5, 6}
					},
				}
			};
			// 4D Array Traversal 
			for(int i = 0; i < nums.GetLength(0); i++)
			{
				for(int j = 0; j < nums.GetLength(1); j++)
				{
					for(int k = 0; k < nums.GetLength(2); k++)
					{
						for(int l = 0; l < nums.GetLength(3); l++)
						{
							Console.Write("{0}\t", nums[i, j, k, l]);
						}
						Console.Write("\t");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}
		}
	}
}