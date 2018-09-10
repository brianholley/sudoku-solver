package main

import "strings"
import "testing"

func TestPuzzle_Filler(t *testing.T) {
	puzzle, err := parsePuzzle(strings.Repeat("1", 81))
	if puzzle == nil {
		t.Errorf("Basic puzzle was not parsed correctly")
	}
	if err != nil {
		t.Errorf("Basic puzzle parsing returned error")
	}
}


func TestPuzzle_InvalidInput(t *testing.T) {
	cases := []struct {
		input, description string
	}{
		{strings.Repeat("-", 81), "Invalid characters"},
		{strings.Repeat("1", 80), "Input too short"},
		{strings.Repeat("1", 82), "Input too long"},
	}
	for _, c := range cases {
		puzzle, err := parsePuzzle(c.input)
		if puzzle != nil {
			t.Errorf("Invalid puzzle parsed correctly: %s", c.description)
		}
		if err == nil {
			t.Errorf("Invalid puzzle should fail: %s", c.description)
		}
	}
}


// func TestPuzzle_ValidIncomplete(t *testing.T) {
// 	var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
// 	Assert.AreEqual(-1, puzzle.Squares[0]);
// 	Assert.AreEqual(9, puzzle.Squares[2]);
// }


// func TestRow_Valid_StartingRowIndex(t *testing.T) {
// 	var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
// 	var row = puzzle.Row(0);
// 	Assert.IsTrue(new int[] {-1, 1, 9, -1, 6, -1, -1, 3, 4}.SequenceEqual(row.ToArray()));
// }


// func TestRow_Valid_NonStartingRowIndex(t *testing.T) {
// 	var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
// 	var row = puzzle.Row(14);
// 	Assert.IsTrue(new int[] {3, -1, 8, -1, 5, 1, -1, -1, -1}.SequenceEqual(row.ToArray()));
// }


// func TestColumn_Valid_StartingColumnIndex(t *testing.T) {
// 	var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
// 	var column = puzzle.Column(1);
// 	Assert.IsTrue(new int[] {1, -1, 4, -1, 7, -1, -1, -1, 8}.SequenceEqual(column.ToArray()));
// }


// func TestSquare_Valid_StartingColumnIndex(t *testing.T) {
// 	var puzzle = Puzzle.ParsePuzzle(".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.");
// 	var square = puzzle.Square(1);
// 	Assert.IsTrue(new int[] {-1, 1, 9, 3, -1, 8, 6, 4, -1}.SequenceEqual(square.ToArray()));
// }


// func TestPossibilities_Empty(t *testing.T) {
// 	var possibilities = Puzzle.Possibilities(Enumerable.Repeat(-1, 9), Enumerable.Repeat(-1, 9), Enumerable.Repeat(-1, 9));
// 	Assert.IsTrue(Enumerable.Range(1, 9).SequenceEqual(possibilities));
// }


// func TestPossibilities_None(t *testing.T) {
// 	var row = new int[] {1, 2, 3, -1, -1, -1, -1, -1, -1};
// 	var column = new int[] {-1, -1, -1, 4, 5, 6, -1, -1, -1};
// 	var square = new int[] {-1, -1, -1, -1, -1, -1, 7, 8, 9};
// 	var possibilities = Puzzle.Possibilities(row, column, square);
// 	Assert.AreEqual(0, possibilities.Count());
// }