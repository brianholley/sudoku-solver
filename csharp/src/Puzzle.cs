using System;
using System.Collections.Generic;
using System.Linq;

namespace Wavecrash.Solver
{
	public class GridSquare
	{
		public int Value { get; set; }
		public List<int> Possibilities { get; set; } = new List<int>();
	}

	public class Puzzle
	{
		public static Puzzle ParsePuzzle(string serialized)
		{
			if (serialized == null || serialized.Length != 81)
				throw new ArgumentOutOfRangeException();

			int[] squares = serialized.ToCharArray(0, 81).Select(c => (c == '.' ? -1 : int.Parse(c + ""))).ToArray();
			return new Puzzle(squares);
		}

		private GridSquare[] _squares = new GridSquare[81];

		public Puzzle(int[] squares)
		{
			for (int i=0; i < squares.Length; i++)
			{
				_squares[i] = new GridSquare { Value = squares[i] };
				if (squares[i] != -1)
					Remaining--;
			}
			for (int i=0; i < squares.Length; i++)
				_squares[i].Possibilities = RecalcPossibilities(i);
		}

		public GridSquare Get(int index) { return _squares[index]; }
		
		public void Set(int index, int value) 
		{
			_squares[index].Value = value;

			Row(index).ForEach(s => s.Possibilities.Remove(value));
			Column(index).ForEach(s => s.Possibilities.Remove(value));
			Square(index).ForEach(s => s.Possibilities.Remove(value));
		}

		public int Remaining { get; } = 81;

		public override string ToString()
		{
			string s = "";
			for (int i = 0; i < 81; i++)
			{
				s += _squares[i].Value.ToString() ?? ".";
				if ((i+1) % 9 == 0)
					s += "\n";
			}
			return s;
		}

		public List<GridSquare> Row(int index)
		{
			int rowStart = index - (index % 9);
			return _squares.Skip(rowStart).Take(9).ToList();
		}

		public List<GridSquare> Column(int index)
		{
			int column = index % 9;
			return _squares.Where((v, i) => (i % 9 == column)).ToList();
		}

		public List<GridSquare> Square(int index)
		{
			int square = (index % 9) / 3 + ((index / 9) / 3) * 3;
			return _squares.Where((v, i) => ((i % 9) / 3 + ((i / 9) / 3) * 3 == square)).ToList();
		}

		private List<int> RecalcPossibilities(int index)
		{
			return Possibilities(
				Row(index).Where(s => s.Value != -1).Select(s => s.Value), 
				Column(index).Where(s => s.Value != -1).Select(s => s.Value), 
				Square(index).Where(s => s.Value != -1).Select(s => s.Value));
		}
		
		public static List<int> Possibilities(IEnumerable<int> row, IEnumerable<int> column, IEnumerable<int> square)
		{
			var used = new HashSet<int>(row.Concat(column.Concat(square)));
			return Enumerable.Range(1, 9).Except(used).ToList();
		}
	}
}
