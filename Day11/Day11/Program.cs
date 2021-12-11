using System;
using System.IO;

namespace Day11 {
	class Program {
		static void Main(string[] args) {
			string[] input = File.ReadAllLines("Input.txt");
			byte[,] octopuses = new byte[10, 10];
			for (int y = 0; y < input.Length; y++) {
				for (int x = 0; x < input[y].Length; x++) {
					octopuses[y, x] = byte.Parse(input[y][x].ToString());
				}
			}

			int partOneOutput = GetFlashesAfterSteps(octopuses, 100);
			Console.WriteLine($"Part one output: {partOneOutput}");
		}

		static int GetFlashesAfterSteps(byte[,] octopuses, int steps) {
			int flashes = 0;
			for (int i = 0; i < steps; i++) {
				for (int y = 0; y < octopuses.GetLength(0); y++) {
					for (int x = 0; x < octopuses.GetLength(1); x++) {
						octopuses[y, x]++;
					}
				}
				for (int y = 0; y < octopuses.GetLength(0); y++) {
					for (int x = 0; x < octopuses.GetLength(1); x++) {
						int flash = Flash(octopuses, x, y);
						flashes += flash;
						// Hidden part two answer in part one :3
						if (flash == 100)
							Console.WriteLine($"Every octopus flashed at step {i}");
						if (flash > 0)
							octopuses[y, x] = 0;
					}
				}
			}
			return flashes;
		}

		static int Flash(byte[,] octopuses, int x, int y) {
			if (x < 0 || x >= octopuses.GetLength(1))
				return 0;
			if (y < 0 || y >= octopuses.GetLength(0))
				return 0;
			if (octopuses[y, x] < 10)
				return 0;
			
			octopuses[y, x] = 0;
			if (x - 1 >= 0) {
				if (y - 1 >= 0)
					if (octopuses[y - 1, x - 1] != 0)
						octopuses[y - 1, x - 1]++;
				if (octopuses[y, x - 1] != 0)
					octopuses[y, x - 1]++;
				if (y + 1 < octopuses.GetLength(0))
					if (octopuses[y + 1, x - 1] != 0)
						octopuses[y + 1, x - 1]++;
			}

			if (y - 1 >= 0)
				if (octopuses[y - 1, x] != 0)
					octopuses[y - 1, x]++;
			if (y + 1 < octopuses.GetLength(0))
				if (octopuses[y + 1, x] != 0)
					octopuses[y + 1, x]++;

			if (x + 1 < octopuses.GetLength(1)) {
				if (y - 1 >= 0)
					if (octopuses[y - 1, x + 1] != 0)
						octopuses[y - 1, x + 1]++;
				if (octopuses[y , x + 1] != 0)
					octopuses[y, x + 1]++;
				if (y + 1 < octopuses.GetLength(0))
					if (octopuses[y + 1, x + 1] != 0)
						octopuses[y + 1, x + 1]++;
			}
			int flashes = 1 +
			              Flash(octopuses, x - 1, y - 1) + Flash(octopuses, x, y - 1) + Flash(octopuses, x + 1, y - 1) +
			              Flash(octopuses, x - 1, y) + Flash(octopuses, x + 1, y) +
			              Flash(octopuses, x - 1, y + 1) + Flash(octopuses, x, y + 1) + Flash(octopuses, x + 1, y + 1);
			octopuses[y, x] = 0;

			return flashes;
		}
	}
}