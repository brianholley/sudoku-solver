package main

import "errors"
import "fmt"
import "os"

func main() {
	if len(os.Args) < 2 {
		fmt.Println("Usage: solver <puzzle>")
		fmt.Println("Use dots for empty spaces, e.g. 12..5.78.")
		return
	}

	puzzle, err := parsePuzzle(os.Args[1])
	if err != nil {
		fmt.Println("Error")
		fmt.Printf("Arg: %s, len: %d", os.Args[1], len(os.Args[1]))
		return
	}

	changed := true
	for changed {
		changed = false
		for i, v := range puzzle {
			if v == -1 {
				possibilities := possibilities(row(puzzle, i), column(puzzle, i), square(puzzle, i))
				if len(possibilities) == 1 {
					puzzle[i] = possibilities[0]
					changed = true
				}
			}
		}
	}

	fmt.Println(puzzle)
}

func parsePuzzle(input string) ([]int, error) {

	if len(input) != 81 {
		return nil, errors.New("Input not the correct size")
	}

	output := make([]int, 81)
	for i, v := range input {
		if v == '.' {
			output[i] = -1
		} else {
			output[i] = int(v - '0')
			if output[i] < 1 || output[i] > 9 {
				return nil, errors.New("Invalid character found in input")
			}
		}
	}
	return output, nil
}

func row(puzzle []int, index int) ([]int) {
	rowStart := index - (index % 9)
	var row []int
	return append(row, puzzle[rowStart:rowStart+9]...)
}

func column(puzzle []int, index int) ([]int) {
	var column []int
	for i := index%9; i < 81; i += 9 {
		column = append(column, puzzle[i])
	}
	return column
}

func square(puzzle []int, index int) ([]int) {
	squareNumber := (index % 9) / 3 + ((index / 9) / 3) * 3
	start := int(squareNumber / 3) * 27 + (squareNumber % 3) * 3
	
	var square []int
	square = append(square, puzzle[start:start+3]...)
	square = append(square, puzzle[start+9:start+9+3]...)
	square = append(square, puzzle[start+18:start+18+3]...)
	return square
}

func possibilities(row []int, column []int, square []int) ([]int) {
	s := map[int]bool {1: true, 2: true, 3: true, 4: true, 5: true, 6: true, 7: true, 8: true, 9: true}
	for _, num := range append(row, append(column, square...)...) {
		_, ok := s[num]
		if ok {
			s[num] = false
		}
	}
	p := make([]int, 0)
	for k, v := range s {
		if v {
			p = append(p, k)
		}
	}
	return p
}