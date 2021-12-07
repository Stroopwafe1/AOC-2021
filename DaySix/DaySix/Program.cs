using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaySix {
	class Program {
		private static int NumberOfDays = 256;
		
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			string input = File.ReadAllText("./Input.txt");
			PartOne(input, 80);
			PartTwo(input, 256);
		}

		public static void PartOne(string input, int numberofdays) {
			List<Lanternfish> lanternfish = input.Split(',').Select(s => new Lanternfish(int.Parse(s))).ToList();
			List<Lanternfish> newFish = new List<Lanternfish>();
			for (int i = 0; i < numberofdays; i++) {
				foreach (var fish in lanternfish) {
					if (fish.NextDay())
						newFish.Add(new Lanternfish(8));
				}
				lanternfish.AddRange(newFish);
				newFish.Clear();
			}

			Console.WriteLine($"Amount of lanternfish after {numberofdays} days: {lanternfish.Count}");
		}

		public static void PartTwo(string input, int numberofdays) {
			ulong[] days = new ulong[9];
			var dayInput = input.Split(',').Select(int.Parse);
			foreach (var day in dayInput) {
				days[day]++;
			}

			for (int i = 0; i < numberofdays; i++) {
				ulong under0 = days[0];
				days[0] = days[1];
				days[1] = days[2];
				days[2] = days[3];
				days[3] = days[4];
				days[4] = days[5];
				days[5] = days[6];
				days[6] = days[7] + under0;
				days[7] = days[8];
				days[8] = under0;
			}

			ulong total = days[0] + days[1] + days[2] + days[3] + days[4] + days[5] + days[6] + days[7] + days[8];
			Console.WriteLine($"Amount of lanternfish after {numberofdays} days: {total}");
		}
	}
}