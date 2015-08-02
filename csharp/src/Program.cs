using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
	public class Program
	{
		static void Main(string[] args)
		{
		}

		public static IEnumerable<int> Puzzle(string input)
		{
			return input.ToCharArray(0, 81).Select(c => (c == '.' ? -1 : int.Parse(c + "")));
		}

		public static IEnumerable<int> Row(IEnumerable<int> puzzle, int rowIndex)
		{
			int rowStart = rowIndex - (rowIndex % 9);
			return puzzle.Skip(rowStart).Take(9);
		}

		public static IEnumerable<int> Possibilities(IEnumerable<int> row, IEnumerable<int> column, IEnumerable<int> square)
		{
			var used = new HashSet<int>(row.Concat(column).Concat(square));
			used.Remove(-1);
			return Enumerable.Range(1, 9).Except(used);
		}
	}
}
