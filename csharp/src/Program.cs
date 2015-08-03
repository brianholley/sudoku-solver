using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
	public class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.Out.WriteLine("Usage: sodoku-solver-csharp <puzzle>");
				Console.Out.WriteLine("Use dots for empty spaces, e.g. 12..5.78.");
				return;
			}
			Puzzle(args[0]);
		}

		public static IEnumerable<int> Puzzle(string input)
		{
			return input.ToCharArray(0, 81).Select(c => (c == '.' ? -1 : int.Parse(c + "")));
		}

		public static IEnumerable<int> Row(IEnumerable<int> puzzle, int index)
		{
			int rowStart = index - (index % 9);
			return puzzle.Skip(rowStart).Take(9);
		}

		public static IEnumerable<int> Column(IEnumerable<int> puzzle, int index)
		{
			int column = index % 9;
			return puzzle.Where((v, i) => (i % 9 == column));
		}

		public static IEnumerable<int> Square(IEnumerable<int> puzzle, int index)
		{
			int square = (index % 9) / 3 + ((index / 9) / 3) * 3;
			return puzzle.Where((v, i) => ((i % 9) / 3 + ((i / 9) / 3) * 3 == square));
		}

		public static IEnumerable<int> Possibilities(IEnumerable<int> row, IEnumerable<int> column, IEnumerable<int> square)
		{
			var used = new HashSet<int>(row.Concat(column).Concat(square));
			used.Remove(-1);
			return Enumerable.Range(1, 9).Except(used);
		}
	}
}
