using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayThree {
	class Program {
		static void Main(string[] args) {
			string input = File.ReadAllText("./Input.txt");
			string[] binaryInput = input.Split('\n');
			int firstBit = (int)Math.Pow(2, binaryInput[0].Length - 1);
			List<int> parsedInput = binaryInput.Select(s => Convert.ToInt32(s, 2)).ToList();
			Console.WriteLine(firstBit);
			
			Console.WriteLine($"Part one output: {PartOne(firstBit, parsedInput)}");
			Console.WriteLine($"Part two output: {PartTwo(firstBit, parsedInput)}");
		}

		private static int PartOne(int firstBit, List<int> input) {
			int gammaRate = 0;
			int maxBit = 0;
			for (int i = firstBit; i >= 1; i /= 2) {
				int count = input.Count(num => (num & i) == i);
				if (count > input.Count / 2)
					gammaRate |= i;
				maxBit |= i;
			}

			int epsilonRate = gammaRate ^ maxBit;
			Console.WriteLine($"Debugging: {gammaRate} ^ {maxBit} = {epsilonRate}");
			return epsilonRate * gammaRate;
		}

		private static int PartTwo(int firstBit, List<int> input) {
			List<int> inputOxy = input.ToList();
			List<int> inputCO2 = input.ToList();
			int oxygen = 0, CO2 = 0;
			for (int i = firstBit; i >= 1; i /= 2) {
				int oxyCountOnes = inputOxy.Count(num => (num & i) == i); //Counting the ones in the oxygen list
				int oxyCountZeros = inputOxy.Count - oxyCountOnes;
				int CO2CountOnes = inputCO2.Count(num => (num & i) == i); //Counting the ones in the co2 list
				int CO2CountZeros = inputCO2.Count - CO2CountOnes;
				if (inputOxy.Count > 1) {
					inputOxy = oxyCountOnes >= oxyCountZeros // If the number of 1 bits is more than or equal to the amount of 0 bits
						? inputOxy.Where(num => (num & i) == i).ToList() // Filter all the numbers with a 1 bit in this position
						: inputOxy.Where(num => (num & i) == 0).ToList(); // Else filter all the numbers with a 0 bit in this position
				}

				if (inputCO2.Count > 1) {
					inputCO2 = CO2CountZeros <= CO2CountOnes // If the number of 0 bits is less than or equal to the amount of 1 bits
						? inputCO2.Where(num => (num & i) == 0).ToList() // Filter all the numbers with a 0 bit in this position
						: inputCO2.Where(num => (num & i) == i).ToList(); // Else filter all the numbers with a 1 bit in this position
				}
			}

			oxygen = inputOxy[0];
			CO2 = inputCO2[0];

			Console.WriteLine($"Debugging: {oxygen} * {CO2} = {oxygen * CO2}");
			return oxygen * CO2;
		}
	}
}