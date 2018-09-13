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
            SolvePuzzle(puzzle);
            Console.Out.WriteLine(puzzle);
        }

        private static void SolvePuzzle(Puzzle puzzle)
        {
            while (puzzle.Remaining > 0)
            {
                int remaining = puzzle.Remaining;
                bool progress = false;
                for (int i = 0; i < 81; i++)
                {
                    var square = puzzle.Get(i);
                    if (square.Value == -1)
                    {
                        if (square.Possibilities.Count() == 1)
                        {
                            puzzle.Set(i, square.Possibilities.First());
                        }
                        if (square.Possibilities.Count() == 2)
                        {
                            if (CheckTwoPossibilitiesConstraint(square.Possibilities, puzzle.Row(i)))
								progress = true;
                            if (CheckTwoPossibilitiesConstraint(square.Possibilities, puzzle.Column(i)))
								progress = true;
                            if (CheckTwoPossibilitiesConstraint(square.Possibilities, puzzle.Square(i)))
								progress = true;
                        }
                    }
                }
                if (remaining == puzzle.Remaining && !progress)
                {
                    Console.Out.WriteLine("Puzzle is stuck");
                    break;
                }
            }
        }

        private static bool CheckTwoPossibilitiesConstraint(List<int> possibilities, List<GridSquare> constraints)
        {
            var matches = constraints.Where(s => s.Possibilities.SequenceEqual(possibilities));
            if (matches.Count() > 1)
            {
                constraints.Where(s => !s.Possibilities.SequenceEqual(possibilities)).ToList().ForEach(s =>
                {
                    s.Possibilities.Remove(possibilities[0]);
                    s.Possibilities.Remove(possibilities[1]);
                });
            	return true;
            }
			return false;
        }
    }
}
