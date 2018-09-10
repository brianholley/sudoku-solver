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
		for i := 0; i < 81; i++ {
			if puzzle[i] == -1 {
				possibilities := possibilities(row(puzzle, i), column(puzzle, i), square(puzzle, i))
				if len(possibilities) == 1 {
					puzzle[i] = possibilities[0]
					fmt.Printf("Found %d at index %d\n", possibilities[0], i)
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
	for i:=0; i<81; i++ {
		if input[i] == '.' {
			output[i] = -1
		} else {
			output[i] = int(input[i] - '0')
			if output[i] < 1 || output[i] > 9 {
				return nil, errors.New("Invalid character found in input")
			}
		}
	}
	return output, nil
}

func row(puzzle []int, index int) ([]int) {
	rowStart := index - (index % 9)
	return puzzle[rowStart:rowStart+9]
}

func column(puzzle []int, index int) ([]int) {
	column := make([]int, 9)
	i := index%9
	for c:=0; c < 9; c++ {
		column[c] = puzzle[i + c * 9]
	}
	return column
}

func square(puzzle []int, index int) ([]int) {
	squareNumber := (index % 9) / 3 + ((index / 9) / 3) * 3
	start := int(squareNumber / 3) * 27 + (squareNumber % 3) * 3
	
	square := puzzle[start:start+3]
	square = append(square, puzzle[start+9:start+9+3]...)
	square = append(square, puzzle[start+18:start+18+3]...)
	return square
}

func possibilities(row []int, column []int, square []int) ([]int) {
	s := []int{1, 2, 3, 4, 5, 6, 7, 9}
	for _, num := range append(row, append(column, square...)...) {
		if num > 0 && num <= len(s) && s[num-1] == num {
			s = append(s[:num-1], s[num:len(s)]...)
		}
	}
	return s
}