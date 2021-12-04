using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayFour {
	public class Program {
		static void Main(string[] args) {
			string fileInput = File.ReadAllText("./Input.txt");
			string[] test = fileInput.Split("\n\r");
			string bingoNumberLine = test[0];

			// ---- SETUP ----
			BingoCard[] bingoCards = new BingoCard[test.Length - 1];
			for (int i = 0; i < bingoCards.Length; i++) {
				bingoCards[i] = new BingoCard();
				string bingoCard = test[i + 1];
				string[] bingoLines = bingoCard.Split("\r\n");
				for (int y = 0; y < bingoLines.Length; y++) {
					string[] bingoLinesNumbers = bingoLines[y].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
					for (int x = 0; x < bingoLinesNumbers.Length; x++) {
						string bingoLineNumber = bingoLinesNumbers[x];
						int number = int.Parse(bingoLineNumber.Trim());
						BingoItem<int> bingoItem = new(number);
						bingoCards[i].InsertItem(x, y, bingoItem);
					}
				}
			}

			// ---- GAME ----
			// ---- PART ONE ----
			int[] bingoNumbers = bingoNumberLine.Split(',').Select(int.Parse).ToArray();
			bool bingo = false;
			for (int i = 0; i < bingoNumbers.Length; i++) {
				if (bingo)
					break;
				for (int j = 0; j < bingoCards.Length; j++) {
					bingo = bingoCards[j].Crossout(bingoNumbers[i]);
					if (bingo) {
						// Calculate score of bingocard
						// Sum of all unmarked numbers * bingoNumbers[i]
						int outputPartOne = bingoCards[j].Sum * bingoNumbers[i];
						Console.WriteLine($"Output for part one is {outputPartOne}");
						break;
					}
				}
			}

			// ---- PART TWO ----
			foreach (BingoCard card in bingoCards)
				card.Reset();
			List<int> indicesWithBingo = new();
			int lastNum = 0;
			for (int i = 0; i < bingoNumbers.Length; i++) {
				
				for (int j = 0; j < bingoCards.Length; j++) {
					bool isAlreadyBingo = bingoCards[j].IsBingo;
					if (isAlreadyBingo)
						continue;
					bingo = bingoCards[j].Crossout(bingoNumbers[i]);
					
					if (bingo) {
						lastNum = bingoNumbers[i];
						indicesWithBingo.Add(j);
						continue;
					}
				}
			}
			int outputPartTwo = bingoCards[indicesWithBingo.Last()].Sum * lastNum;
			Console.WriteLine($"Output for part two is {outputPartTwo}");
		}
	}
}