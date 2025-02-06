using System;
using System.IO;                    // 'FileInfo' class located here

// StreamReader and StreamWriter is used to read and write characters from and into byte stream
namespace StreamRWApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// STREAM WRITE: We can directly write string 
			string myFile = @"myFile.txt";
			StreamReader sr = new StreamReader(myFile);
			Console.WriteLine(sr.Peek());               // Reads one byte and returns byte value(But cursor doesn't move forwarded)
														//Console.WriteLine(sr.Read());				// Reads one byte and returns byte value
			Console.WriteLine(sr.ReadLine());           // Reads bytes of one entire line and returns string
			Console.WriteLine(sr.ReadToEnd());          // Reads bytes from current to end of file and returns string
														// Close stream reader after use
			sr.Close();
		}
	}
}
