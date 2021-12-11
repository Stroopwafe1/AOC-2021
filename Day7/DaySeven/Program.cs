using System;
using System.IO;
using System.Linq;

namespace DaySeven {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			string input = File.ReadAllText("./Input.txt");
			//string input = "16,1,2,0,4,2,7,1,2,14";
			var positions = input.Split(',').Select(int.Parse).ToList();
			int bestPos = 0;
			int leastFuel = int.MaxValue;
			for (int i = positions.Min(); i <= positions.Max(); i++) {
				// -------- Uncomment for part one ----------
				//int fuelForI = positions.Select(num => Math.Abs(num - i)).Sum();
				// ------------------------------------------
				
				// -------- Comment for part one ------------
				int fuelForI = 0;
				var movedPositions = positions.Select(num => Math.Abs(num - i)).ToList();
				foreach (int t in movedPositions) {
					for (int k = t; k > 0; k--) {
						fuelForI += k;
					}
				}
				// -------------------------------------------
					
				if (fuelForI < leastFuel) {
					leastFuel = fuelForI;
					bestPos = i;
				}
			}
			Console.WriteLine($"Best position is: {bestPos} with total fuel being: {leastFuel}");
		}
	}
}