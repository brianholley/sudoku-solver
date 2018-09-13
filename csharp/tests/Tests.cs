using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Wavecrash.Solver;

namespace Wavecrash.Solver.Tests
{
	public class Tests
	{
		private const string EasyPuzzle = ".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.";
		private const string MediumPuzzle = ".5....6.2....3..9..9..8675........2956..7..4328........2679..1..4..6....3.7....6.";
		[Fact]
		public void Puzzle_Filler()
		{
			var puzzle = Puzzle.ParsePuzzle(new string('1', 81));
			Assert.NotNull(puzzle);
			Assert.Equal(1, puzzle.Get(0).Value);
		}

		[Fact]
		public void Puzzle_InvalidCharacters()
		{
            Assert.Throws<FormatException>(() => 
            {
			    Puzzle.ParsePuzzle(new string('-', 81));
            });
		}

		[Fact]
		public void Puzzle_InvalidTooShort()
		{
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {
			    Puzzle.ParsePuzzle(new string('1', 80));
            });
		}

		[Fact]
		public void Puzzle_InvalidTooLong()
		{
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {
			    Puzzle.ParsePuzzle(new string('1', 82));
            });
		}

		[Fact]
		public void Puzzle_ValidIncomplete()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			Assert.Equal(-1, puzzle.Get(0).Value);
			Assert.Equal(9, puzzle.Get(2).Value);
		}

		[Fact]
		public void Row_Valid_StartingRowIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var row = puzzle.Row(0);
			Assert.True(new int[] {-1, 1, 9, -1, 6, -1, -1, 3, 4}.SequenceEqual(row.Select(s => s.Value).ToArray()));
		}

		[Fact]
		public void Row_Valid_NonStartingRowIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var row = puzzle.Row(14);
			Assert.True(new int[] {3, -1, 8, -1, 5, 1, -1, -1, -1}.SequenceEqual(row.Select(s => s.Value).ToArray()));
		}

		[Fact]
		public void Column_Valid_StartingColumnIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var column = puzzle.Column(1);
			Assert.True(new int[] {1, -1, 4, -1, 7, -1, -1, -1, 8}.SequenceEqual(column.Select(s => s.Value).ToArray()));
		}

		[Fact]
		public void Square_Valid_StartingColumnIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var square = puzzle.Square(1);
			Assert.True(new int[] {-1, 1, 9, 3, -1, 8, 6, 4, -1}.SequenceEqual(square.Select(s => s.Value).ToArray()));
		}

		[Fact]
		public void Possibilities_Empty()
		{
			var possibilities = Puzzle.Possibilities(Enumerable.Repeat(-1, 9), Enumerable.Repeat(-1, 9), Enumerable.Repeat(-1, 9));
			Assert.True(Enumerable.Range(1, 9).SequenceEqual(possibilities));
		}

		[Fact]
		public void Possibilities_None()
		{
			var row = new int[] {1, 2, 3, -1, -1, -1, -1, -1, -1};
			var column = new int[] {-1, -1, -1, 4, 5, 6, -1, -1, -1};
			var square = new int[] {-1, -1, -1, -1, -1, -1, 7, 8, 9};
			var possibilities = Puzzle.Possibilities(row, column, square);
			Assert.Empty(possibilities);
		}
	}
}
