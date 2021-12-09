using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayNine {
	class Program {
		static void Main(string[] args) {
			string[] input = File.ReadAllLines("Input.txt");
			HeightMapItem[,] heightmap = new HeightMapItem[input.Length, input[0].Length];
			for (int y = 0; y < input.Length; y++) {
				for (int x = 0; x < input[y].Length; x++) {
					heightmap[y, x] = new HeightMapItem() { Height = byte.Parse(input[y][x].ToString()) };
				}
			}
			#region PartOne
			/*
			int riskLevel = 0;
			
			for (int y = 0; y < input.Length; y++) {
				for (int x = 0; x < input[y].Length; x++) {

					byte current = heightmap[y, x].Height;
					byte left = x - 1 >= 0 ? heightmap[y, x - 1].Height : byte.MaxValue;
					byte up = y - 1 >= 0 ? heightmap[y - 1, x].Height : byte.MaxValue;
					byte right = x + 1 < input[y].Length ? heightmap[y, x + 1].Height : byte.MaxValue;
					byte down = y + 1 < input.Length ? heightmap[y + 1, x].Height : byte.MaxValue;

					byte lowestByte = getLowestByte(left, up, down, right, current);
					heightmap[y, x].Visited = true;
					if (lowestByte != current) {
						heightmap[y, x].CurrentLowest = false;
					} else if (lowestByte == right) {
						heightmap[y, x + 1].CurrentLowest = true;
					} else if (lowestByte == down) {
						heightmap[y + 1, x].CurrentLowest = true;
					} else if (lowestByte == current) {
						riskLevel += 1 + current;
					}
				}
			}
			Console.WriteLine($"Output part one: {riskLevel}");
			*/
			#endregion
			#region PartTwo
			List<int> BasinSizes = new List<int>();
			int partTwoOutput = 0;
			for (int y = 0; y < heightmap.GetLength(0); y++) {
				for (int x = 0; x < heightmap.GetLength(1); x++) {
					int basinCount = GetBasinCount(heightmap, x, y);
					if (basinCount == 0)
						continue;
					BasinSizes.Add(basinCount);
				}
			}

			var highestThree = BasinSizes.OrderByDescending(i => i).Take(3).ToList();
			partTwoOutput = highestThree[0] * highestThree[1] * highestThree[2];
			Console.WriteLine($"Part two output: {partTwoOutput}");
			#endregion
		}

		public static int GetBasinCount(HeightMapItem[,] arr, int x, int y) {
			if (x < 0 || x >= arr.GetLength(1))
				return 0;
			if (y < 0 || y >= arr.GetLength(0))
				return 0;
			if (arr[y, x].Visited)
				return 0;
			if (arr[y, x].Height == 9)
				return 0;

			arr[y, x].Visited = true;
			return 1 + GetBasinCount(arr, x - 1, y) + GetBasinCount(arr, x, y - 1) + GetBasinCount(arr, x + 1, y) +
			       GetBasinCount(arr, x, y + 1);
		}

		public static byte getLowestByte(params byte[] bytes) {
			return bytes.Min();
		}
	}

	class HeightMapItem {
		public byte Height { get; set; }
		public bool Visited { get; set; }
		public bool CurrentLowest { get; set; }
	}
}