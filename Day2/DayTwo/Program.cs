using System;
using System.IO;

namespace DayTwo {
	class Program {
		static void Main(string[] args) {
			string input = File.ReadAllText("./input.txt");

			Console.WriteLine("Part one output: " + PartOne(input));
			Console.WriteLine("Part two output: " + PartTwo(input));
		}

		public static int PartOne(string input) {
			int posX = 0, depth = 0;
			string[] parts = input.Split('\n');
			for (int i = 0; i < parts.Length; i++) {
				string[] instructions = parts[i].Split(' ');
				switch (instructions[0]) {
					case "forward":
						posX += int.Parse(instructions[1]);
						break;
					case "down":
						depth += int.Parse(instructions[1]);
						break;
					case "up":
						depth -= int.Parse(instructions[1]);
						break;
				}
			}

			return posX * depth;
		}

		public static int PartTwo(string input) {
			int posX = 0, depth = 0, aim = 0;
			string[] parts = input.Split('\n');
			for (int i = 0; i < parts.Length; i++) {
				string[] instructions = parts[i].Split(' ');
				int amount = int.Parse(instructions[1]);
				switch (instructions[0]) {
					case "forward":
						posX += amount;
						depth += aim * amount;
						break;
					case "down":
						aim += amount;
						break;
					case "up":
						aim -= amount;
						break;
				}
			}

			return posX * depth;
		}
	}
}