package main

import "fmt"
import "strings"
import "testing"

const testPuzzle = ".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57."

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

func areArraysEqual(a []int, b []int) (bool) {
	if len(a) != len(b) {
		return false
	}
	for i, v := range a {
		if b[i] != v {
			return false
		}
	}
	return true
}

func TestRow_Valid(t *testing.T) {
	cases := []struct {
		index int
		match []int
	}{
		{0, []int {-1, 1, 9, -1, 6, -1, -1, 3, 4}},
		{14, []int {3, -1, 8, -1, 5, 1, -1, -1, -1}},
	}
	puzzle, err := parsePuzzle(testPuzzle)
	if len(puzzle) != 81 || err != nil {
		t.Errorf("Puzzle did not parse correctly")
	}
	for _, c := range cases {
		row := row(puzzle, c.index)
		if len(row) != 9 {
			t.Errorf("Row is not the correct length: %d", len(row))
		}
		if !areArraysEqual(row, c.match) {
			fmt.Println("Incorrect row: ", row)
			t.Errorf("Row is incorrect")
		}
	}
}

func TestColumn_Valid(t *testing.T) {
	cases := []struct {
		index int
		match []int
	}{
		{1, []int {1, -1, 4, -1, 7, -1, -1, -1, 8}},
		{20, []int {9, 8, -1, -1, -1, 5, -1, -1, -1}},
	}
	puzzle, err := parsePuzzle(testPuzzle)
	if len(puzzle) != 81 || err != nil {
		t.Errorf("Puzzle did not parse correctly")
	}
	for _, c := range cases {
		column := column(puzzle, c.index)
		if len(column) != 9 {
			t.Errorf("Column is not the correct length: %d", len(column))
		}
		if !areArraysEqual(column, c.match) {
			fmt.Println("Incorrect column: ", column)
			t.Errorf("Column is incorrect")
		}
	}
}

func TestSquare_Valid(t *testing.T) {
	cases := []struct {
		index int
		match []int
	}{
		{1, []int {-1, 1, 9, 3, -1, 8, 6, 4, -1}},
		{10, []int {-1, 1, 9, 3, -1, 8, 6, 4, -1}},
		{66, []int {5, -1, -1, 1, 7, -1, -1 ,3, -1}},
	}
	puzzle, err := parsePuzzle(testPuzzle)
	if len(puzzle) != 81 || err != nil {
		t.Errorf("Puzzle did not parse correctly")
	}
	for _, c := range cases {
		square := square(puzzle, c.index)
		if len(square) != 9 {
			t.Errorf("Square is not the correct length: %d", len(square))
		}
		if !areArraysEqual(square, c.match) {
			fmt.Println("Incorrect square: ", square)
			t.Errorf("Square is incorrect")
		}
	}
}

func TestPossibilities_Puzzle(t *testing.T) {
	cases := []struct {
		index int
		match []int
	}{
		{10, []int {2}},
	}
	puzzle, err := parsePuzzle(testPuzzle)
	if len(puzzle) != 81 || err != nil {
		t.Errorf("Puzzle did not parse correctly")
	}
	for _, c := range cases {
		poss := possibilities(row(puzzle, c.index), column(puzzle, c.index), square(puzzle, c.index))
		if !areArraysEqual(poss, c.match) {
			fmt.Println("Incorrect possibilities: ", poss)
			t.Errorf("Possibilities are incorrect")
		}
	}
}


func TestPossibilities_None(t *testing.T) {
	row := []int{1, 2, 3, -1, -1, -1, -1, -1, -1}
	column := []int{-1, -1, -1, 4, 5, 6, -1, -1, -1}
	square := []int{-1, -1, -1, -1, -1, -1, 7, 8, 9}
	
	possibilities := possibilities(row, column, square)
	if len(possibilities) != 0 {
		t.Errorf("Possibilities incorrect: %d found", len(possibilities))
	}
}

func TestPossibilities_SingleValue(t *testing.T) {
	row := []int{-1, 2, 3, -1, -1, -1, -1, -1, -1}
	column := []int{-1, -1, -1, 4, 5, 6, -1, -1, -1}
	square := []int{-1, -1, -1, -1, -1, -1, 7, 8, 9}
	
	possibilities := possibilities(row, column, square)
	if len(possibilities) != 1 {
		t.Errorf("Possibilities incorrect: %d found", len(possibilities))
	} else if possibilities[0] != 1 {
		t.Errorf("Possibilities incorrect: %d found", possibilities[0])
	}
}