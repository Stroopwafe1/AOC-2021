using System;
using System.Collections.Generic;
using System.IO;

namespace Day10 {
	public class Program {
		static void Main(string[] args) {
			string[] input = File.ReadAllLines("./Input.txt");

			#region partOne
			/*int partOneScore = 0;
			foreach (string line in input) {
				Stack<char> illegals = GetIllegalChars(line);
				partOneScore += CalculateScorePartOne(illegals);
			}

			Console.WriteLine($"Output part one: {partOneScore}");*/
			#endregion

			#region partTwo
			List<long> scores = new List<long>();
			foreach (string line in input) {
				var chars = GetIllegalChars(line);
				Stack<char> illegals = chars.Item1;
				if (illegals.Count != 0)
					continue;
				Stack<char> current = chars.Item2;
				scores.Add(CalculateScorePartTwo(current));
			}
			scores.Sort();
			Console.WriteLine($"Output part two: {scores[scores.Count / 2]}");

			#endregion
		}

		private static int CalculateScorePartOne(Stack<char> illegals) {
			int score = 0;
			while (illegals.Count > 0) {
				char c = illegals.Pop();
				switch (c) {
					case ')':
						score += 3;
						break;
					case ']':
						score += 57;
						break;
					case '}':
						score += 1197;
						break;
					case '>':
						score += 25137;
						break;
				}
			}
			return score;
		}

		private static long CalculateScorePartTwo(Stack<char> current) {
			long score = 0;
			while (current.Count > 0) {
				char c = current.Pop();
				switch (c) {
					case '(':
						score = score * 5 + 1;
						break;
					case '[':
						score = score * 5 + 2;
						break;
					case '{':
						score = score * 5 + 3;
						break;
					case '<':
						score = score * 5 + 4;
						break;
				}
			}
			return score;
		}

		static (Stack<char>, Stack<char>) GetIllegalChars(string input) {
			Stack<char> current = new Stack<char>();
			Stack<char> illegal = new Stack<char>();

			foreach (char c in input) {
				char pop;
				switch (c) {
					case '(':
						current.Push(c);
						break;
					case ')':
						pop = current.Pop();
						if (pop != '(')
							illegal.Push(c);
						break;
					case '[':
						current.Push(c);
						break;
					case ']':
						pop = current.Pop();
						if (pop != '[')
							illegal.Push(c);
						break;
					case '{':
						current.Push(c);
						break;
					case '}':
						pop = current.Pop();
						if (pop != '{')
							illegal.Push(c);
						break;
					case '<':
						current.Push(c);
						break;
					case '>':
						pop = current.Pop();
						if (pop != '<')
							illegal.Push(c);
						break;
				}
				if (illegal.Count > 0) {
					return (illegal, current);
				}
			}
			return (illegal, current);
			/*if (current.Count != 0)
				return new Stack<char>();
			return illegal;*/
		}
	}
}

