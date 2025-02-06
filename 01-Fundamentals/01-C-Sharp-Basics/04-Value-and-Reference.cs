/*
	Date types can be: 
		1) Value Type => Simple data types, structures and enums 
		2) Reference Type => Array, String, Object, Class, Delegate, Interface, Record and Dynamic
	1) Value Types: 
		- Types whose value gets passed when passed by value
	2) Reference Types: 
		- Types whose reference gets passed even when passed by value
		- Remember passing array name in C-function actually modifies original array
*/
using System;

namespace ValueReference
{
	public class Program
	{
		int value;
		static void Modify(int i)
		{
			i *= 2;
			Console.WriteLine("Modified i = {0}", i);
		}
		static void ModifyRef(Program obj)
		{
			obj.value *= 2;
			Console.WriteLine("Modified value = {0}", obj.value);
		}
		static void Main(string[] args)
		{
			int i = 10;
			Program obj = new Program();
			obj.value = 15;

			Console.WriteLine("Before Modification, i = {0} and value = {1}", i, obj.value);
			Modify(i);
			ModifyRef(obj);
			// 'i' value is not modified
			// 'obj.value' gets modified even though it was passed by value. Same for Array
			Console.WriteLine("After Modification, i = {0} and value = {1}", i, obj.value);
		}
	}
}