using System;
using System.IO;

namespace DayFive {

	internal class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");

			byte[,] array = new byte[1000, 1000];
			string[] input = File.ReadAllLines("./Input.txt");
			foreach (string line in input) {
				string[] coords = line.Split(" -> ");
				string pair1 = coords[0];
				string pair2 = coords[1];

				int x1 = int.Parse(pair1.Split(',')[0]);
				int y1 = int.Parse(pair1.Split(',')[1]);
				int x2 = int.Parse(pair2.Split(',')[0]);
				int y2 = int.Parse(pair2.Split(',')[1]);

				// ----- Uncomment for part one -----
//				if (x1 != x2 && y1 != y2)
//					continue;
				// ----------------------------------

				int yMin = Math.Min(y1, y2);
				int yMax = Math.Max(y1, y2);
				int xMin = Math.Min(x1, x2);
				int xMax = Math.Max(x1, x2);

				for (int y = yMin; y <= yMax; y++) {
					for (int x = xMin; x <= xMax; x++) {
						// ----- Comment for part one -------
						if (x1 != x2 && y1 != y2) {
							if (!IsInLine(x1, x, x2, y1, y, y2))
								continue;
						} else {
							if (!IsInNonDiagonalLine(x1, x, x2, y1, y, y2))
								continue;
						}
						// ----------------------------------
						array[y, x]++;
					}
				}
			}

			int counter = 0;
			for (int y = 0; y < array.GetLength(0); y++) {
				for (int x = 0; x < array.GetLength(1); x++) {
					if (array[y, x] >= 2)
						counter++;
				}
			}

			Console.WriteLine($"Output = {counter}");
		}

		private static bool IsDiagonal(int row1, int col1, int row2, int col2) {
			return (Math.Abs(row1 - col1) == Math.Abs(row2 - col2) || row1 + col1 == row2 + col2) && !(row1 == row2 && col1 == col2);
		}

		private static bool IsInLine(int x1, int x, int x2, int y1, int y, int y2) {
			int a = y2 - y1;
			int b = x1 - x2;
			int c = a * x1 + b * y1;
			return a * x + b * y == c;
		}

		public static bool IsInNonDiagonalLine(int x1, int x, int x2, int y1, int y, int y2) {
			return (x1 - x) * (y1 - y) == (x - x2) * (y - y2);
		}

		private static bool IsHorizontal(int row1, int col1, int row2, int col2) {
			return row1 == row2 && col1 != col2;
		}

		private static bool IsVertical(int row1, int col1, int row2, int col2) {
			return row1 != row2 && col1 == col2;
		}

		private static bool IsCardinalDirection(int row1, int col1, int row2, int col2) {
			return IsDiagonal(row1, col1, row2, col2) || IsHorizontal(row1, col1, row2, col2) || IsVertical(row1, col1, row2, col2);
		}
	}
}