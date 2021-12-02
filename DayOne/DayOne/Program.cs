using System;
using System.IO;
using System.Linq;
using System.Net;

namespace DayOne {
	class Program {
		static void Main(string[] args) {
			int output = 0;
			string input = File.ReadAllText("./Input.txt");
			//Console.WriteLine(input);
			string[] parts = input.Split('\n');
			var parsedParts = parts.Select(int.Parse).ToList();
			for (int i = 0; i < parsedParts.Count; i++) {
				if (i + 3 >= parsedParts.Count) {
					break;
				}

				int groupA = parsedParts[i] + parsedParts[i + 1] + parsedParts[i + 2];
				int groupB = parsedParts[i + 1] + parsedParts[i + 2] + parsedParts[i + 3];
				
				//--------- Part one -----------
				// if (IsHigher(parsedParts[i], parsedParts[i - 1])) {
				// 	output++;
				// }
				if (IsHigher(groupB, groupA))
					output++;

				//-------- Part two ------------
			}

			Console.WriteLine($"Final output: {output}");
		}

		public static bool IsHigher(int a, int b) {
			return a > b;
		}
	}
}