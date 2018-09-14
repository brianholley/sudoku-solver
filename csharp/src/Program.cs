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
                            if (ApplyTwoPossibilitiesConstraint(square.Possibilities, puzzle.Row(i)))
								progress = true;
                            if (ApplyTwoPossibilitiesConstraint(square.Possibilities, puzzle.Column(i)))
								progress = true;
                            if (ApplyTwoPossibilitiesConstraint(square.Possibilities, puzzle.Square(i)))
								progress = true;
                        }
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    if (ApplyOnlyOpenPlaceConstraint(puzzle.Row(i * 9)))
                        progress = true;
                    if (ApplyOnlyOpenPlaceConstraint(puzzle.Column(i)))
                        progress = true;
                    if (ApplyOnlyOpenPlaceConstraint(puzzle.Square((i / 3) * 27 + (i % 3) * 3)))
                        progress = true;
                }
                if (remaining == puzzle.Remaining && !progress)
                {
                    Console.Out.WriteLine("Puzzle is stuck");
                    break;
                }
            }
        }

		// This constraint is that there are two squares which have the same 
		// two possibilities, therefore no other square can be one of those two values
        private static bool ApplyTwoPossibilitiesConstraint(List<int> possibilities, List<GridSquare> constraints)
        {
			bool progress = false;
            var matches = constraints.Where(s => s.Possibilities.SequenceEqual(possibilities));
            if (matches.Count() > 1)
            {
                constraints.Where(s => !s.Possibilities.SequenceEqual(possibilities)).ToList().ForEach(s =>
                {
					if (s.Possibilities.Intersect(possibilities).Any())
					{
						progress = true;
						s.Possibilities.Remove(possibilities[0]);
						s.Possibilities.Remove(possibilities[1]);
					}
                });
            }
			return progress;
        }

		// This constraint is that there is only one open place in the 
		// row/column/square that can contain a value
		private static bool ApplyOnlyOpenPlaceConstraint(List<GridSquare> constraints)
        {
            bool progress = true;
            var counts = new int[10];
            constraints.ForEach(c => 
            {
                c.Possibilities.ForEach(p => counts[p]++);
            });
            for (int i=1; i < counts.Length; i++)
            {
                if (counts[i] == 1)
                {
                    constraints.First(c => c.Possibilities.Contains(i)).Possibilities = new List<int> { i };
                    progress = true;
                }
            }
            return progress;
		}
    }
}
