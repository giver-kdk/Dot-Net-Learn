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
			StreamWriter sw = new StreamWriter(myFile);
			sw.WriteLine("I am a text");
			sw.Write("I am another text");
			// Close stream writer after use. Otherwise, it won't complete task
			sw.Close();
		}
	}
}
