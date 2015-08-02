using System;
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
			var puzzle = Program.Puzzle(new string('1', 81));
			Assert.AreEqual(81, puzzle.Count());
			Assert.AreEqual(1, puzzle.First());
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void Puzzle_InvalidCharacters()
		{
			var puzzle = Program.Puzzle(new string('-', 81));
			puzzle.First();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Puzzle_InvalidTooShort()
		{
			var puzzle = Program.Puzzle(new string('1', 80));
			puzzle.First();
		}

		[TestMethod]
		public void Puzzle_InvalidTooLong()
		{
			var puzzle = Program.Puzzle(new string('1', 82));
			Assert.AreEqual(81, puzzle.Count());
		}

		[TestMethod]
		public void Puzzle_ValidIncomplete()
		{
			var puzzle = Program.Puzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
			Assert.AreEqual(81, puzzle.Count());
			Assert.AreEqual(-1, puzzle.First());
			Assert.AreEqual(9, puzzle.Skip(2).First());
		}
	}
}
