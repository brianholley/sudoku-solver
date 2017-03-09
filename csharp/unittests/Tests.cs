using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;

namespace UnitTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void Puzzle_Filler()
		{
			var puzzle = Puzzle.ParsePuzzle(new string('1', 81));
			Assert.IsNotNull(puzzle);
			Assert.AreEqual(1, puzzle.Squares[0]);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void Puzzle_InvalidCharacters()
		{
			Puzzle.ParsePuzzle(new string('-', 81));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Puzzle_InvalidTooShort()
		{
			Puzzle.ParsePuzzle(new string('1', 80));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Puzzle_InvalidTooLong()
		{
			Puzzle.ParsePuzzle(new string('1', 82));
		}

		[TestMethod]
		public void Puzzle_ValidIncomplete()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			Assert.AreEqual(-1, puzzle.Squares[0]);
			Assert.AreEqual(9, puzzle.Squares[2]);
		}

		[TestMethod]
		public void Row_Valid_StartingRowIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var row = puzzle.Row(0);
			Assert.IsTrue(new int[] {-1, 1, 9, -1, 6, -1, -1, 3, 4}.SequenceEqual(row.ToArray()));
		}

		[TestMethod]
		public void Row_Valid_NonStartingRowIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var row = puzzle.Row(14);
			Assert.IsTrue(new int[] {3, -1, 8, -1, 5, 1, -1, -1, -1}.SequenceEqual(row.ToArray()));
		}

		[TestMethod]
		public void Column_Valid_StartingColumnIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var column = puzzle.Column(1);
			Assert.IsTrue(new int[] {1, -1, 4, -1, 7, -1, -1, -1, 8}.SequenceEqual(column.ToArray()));
		}

		[TestMethod]
		public void Square_Valid_StartingColumnIndex()
		{
			var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			var square = puzzle.Square(1);
			Assert.IsTrue(new int[] {-1, 1, 9, 3, -1, 8, 6, 4, -1}.SequenceEqual(square.ToArray()));
		}

		[TestMethod]
		public void Possibilities_Empty()
		{
			var possibilities = Puzzle.Possibilities(Enumerable.Repeat(-1, 9), Enumerable.Repeat(-1, 9), Enumerable.Repeat(-1, 9));
			Assert.IsTrue(Enumerable.Range(1, 9).SequenceEqual(possibilities));
		}

		[TestMethod]
		public void Possibilities_None()
		{
			var row = new int[] {1, 2, 3, -1, -1, -1, -1, -1, -1};
			var column = new int[] {-1, -1, -1, 4, 5, 6, -1, -1, -1};
			var square = new int[] {-1, -1, -1, -1, -1, -1, 7, 8, 9};
			var possibilities = Puzzle.Possibilities(row, column, square);
			Assert.AreEqual(0, possibilities.Count());
		}
	}
}
