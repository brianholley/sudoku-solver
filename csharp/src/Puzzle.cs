using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
	public class Puzzle
	{
		public static Puzzle ParsePuzzle(string serialized)
		{
			if (serialized == null || serialized.Length != 81)
				throw new ArgumentOutOfRangeException();

			var squares = serialized.ToCharArray(0, 81).Select(c => (c == '.' ? -1 : int.Parse(c + ""))).ToArray();
			return new Puzzle(squares);
		}

		public int[] Squares { get; private set; }
		public Puzzle(int[] squares)
		{
			Squares = squares;
		}

		public override string ToString()
		{
			string s = "";
			for (int i = 0; i < 9; i++)
			{
				s += string.Join(" ", Squares.Skip(i * 9).Take(9).ToArray().Select(c => (c == -1 ? "." : c.ToString())));
			}
			return s;
		}

		public IEnumerable<int> Row(int index)
		{
			int rowStart = index - (index % 9);
			return Squares.Skip(rowStart).Take(9);
		}

		public IEnumerable<int> Column(int index)
		{
			int column = index % 9;
			return Squares.Where((v, i) => (i % 9 == column));
		}

		public IEnumerable<int> Square(int index)
		{
			int square = (index % 9) / 3 + ((index / 9) / 3) * 3;
			return Squares.Where((v, i) => ((i % 9) / 3 + ((i / 9) / 3) * 3 == square));
		}

		public static IEnumerable<int> Possibilities(IEnumerable<int> row, IEnumerable<int> column, IEnumerable<int> square)
		{
			var used = new HashSet<int>(row.Concat(column).Concat(square));
			used.Remove(-1);
			return Enumerable.Range(1, 9).Except(used);
		}
	}
}
