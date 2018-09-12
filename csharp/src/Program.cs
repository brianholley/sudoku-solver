using System;
using System.Collections.Generic;
using System.Linq;

namespace Wavecrash.Solver
{
	public class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.Out.WriteLine("Usage: solver <puzzle>");
				Console.Out.WriteLine("Use dots for empty spaces, e.g. 12..5.78.");
				return;
			}

			var puzzle = Puzzle.ParsePuzzle(args[0]);
			bool changed;
			do
			{
				changed = false;
				for (int i = 0; i < puzzle.Squares.Length; i++)
				{
					if (puzzle.Squares[i] == -1)
					{
						var possibilities = Puzzle.Possibilities(puzzle.Row(i), puzzle.Column(i), puzzle.Square(i));
						if (possibilities.Count() == 1)
						{
							puzzle.Squares[i] = possibilities.First();
							changed = true;
						}
					}
				}
			} while (changed);

			Console.Out.WriteLine(puzzle);
		}
	}
}
