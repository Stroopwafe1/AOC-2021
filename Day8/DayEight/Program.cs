using System;
using System.IO;
using System.Linq;

namespace DayEight {
	class Program {
		static void Main(string[] args) {
			string[] aocInput = File.ReadAllLines("./Input.txt");
			string[] onlyOuputValues = aocInput.Select(s => s.Split(" | ")[1]).ToArray();
			int numberOfSimpleValues = 0;
			foreach (var ouputValue in onlyOuputValues) {
				numberOfSimpleValues += ouputValue.Split(' ').Count(s => s.Length == 2 || s.Length == 4 || s.Length == 3 || s.Length == 7);
			}

			Console.WriteLine($"Number of simple values in output: {numberOfSimpleValues}");

			int partTwoOutput = 0;
			foreach (string line in aocInput) {
				string input = line.Split(" | ")[0];
				string output = line.Split(" | ")[1];
				Segment[] decodedInput = DecodeInput(input);
				string[] outputNumbers = output.Split(" ");
				for (int index = 0; index < outputNumbers.Length; index++) {
					var outputNumber = outputNumbers[index];
					string sortedSignal = SortString(outputNumber);
					int number = decodedInput.First(s => sortedSignal.Equals(s.Signal)).Number;
					partTwoOutput += (number * (1000 / (int)Math.Pow(10, index)));
				}
			}

			Console.WriteLine($"Part two output: {partTwoOutput}");
		}

		public static Segment[] DecodeInput(string input) {
			Segment[] returnValue = new Segment[10];
			string[] numbers = input.Split(" ");
			for (int i = 0; i < numbers.Length; i++) {
				char[] chars = numbers[i].ToCharArray();
				Array.Sort(chars);
				numbers[i] = new string(chars);
			}
			returnValue[1] = new Segment() { Number = 1, Signal = numbers.First(s => s.Length == 2) };
			returnValue[4] = new Segment() { Number = 4, Signal = numbers.First(s => s.Length == 4) };
			returnValue[7] = new Segment() { Number = 7, Signal = numbers.First(s => s.Length == 3) };
			returnValue[8] = new Segment() { Number = 8, Signal = numbers.First(s => s.Length == 7) };

			numbers = numbers.Where(s => s.Length == 5 || s.Length == 6).ToArray();
			// Find the 6, it's the only digit with length of 6 and doesn't have both of the characters from 1
			returnValue[6] = new Segment() { Number = 6, Signal = numbers.First(s => s.Length == 6 && !ContainsSameChars(s, returnValue[1].Signal)) };
			returnValue[5] = new Segment() { Number = 5, Signal = numbers.First(s => s.Length == 5 && ContainsSameChars(returnValue[6].Signal,s)) };
			returnValue[9] = new Segment() { Number = 9, Signal = numbers.First(s => s.Length == 6 && !s.Equals(returnValue[6].Signal) && ContainsSameChars(s, returnValue[5].Signal)) };
			numbers = numbers.Where(s => !(s.Equals(returnValue[6].Signal) || s.Equals(returnValue[5].Signal) || s.Equals(returnValue[9].Signal))).ToArray();
			
			returnValue[0] = new Segment() { Number = 0, Signal = numbers.First(s => s.Length == 6) };
			returnValue[3] = new Segment() { Number = 3, Signal = numbers.First(s => s.Length == 5 && ContainsSameChars(s, returnValue[7].Signal)) };
			returnValue[2] = new Segment() { Number = 2, Signal = numbers.First(s => s.Length == 5 && !ContainsSameChars(s, returnValue[7].Signal)) };
			
			return returnValue;
		}

		public static string SortString(string s) {
			char[] chars = s.ToCharArray();
			Array.Sort(chars);

			return new string(chars);
		}

		public static bool ContainsSameChars(string input, string comparison) {
			return comparison.ToCharArray().All(input.Contains);
		}
	}

	public class Segment {
		public int Number { get; set; }
		public string Signal { get; set; }
	}
}