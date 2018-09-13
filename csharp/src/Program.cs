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
			while (puzzle.Remaining > 0)
			{
				int remaining = puzzle.Remaining;
				for (int i = 0; i < 81; i++)
				{
					var square = puzzle.Get(i);
					if (square.Value == -1 && square.Possibilities.Count() == 1)
					{
						puzzle.Set(i, square.Possibilities.First());
					}
				}
				if (remaining == puzzle.Remaining)
				{
					Console.Out.WriteLine("Puzzle is stuck");
					break;
				}
			}

			Console.Out.WriteLine(puzzle);
		}
	}
}
