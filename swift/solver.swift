import Foundation

func getRow(_ puzzle: [Int?], row: Int) -> [Int?] {
    return puzzle.enumerated().filter({ 
        (i, element) in i >= row * 9 && i < (row + 1) * 9
    }).map({
        (i, element) in element
    })
}

func getColumn(_ puzzle: [Int?], col: Int) -> [Int?] {
    return puzzle.enumerated().filter({ 
        (i, element) in i % 9 == col 
    }).map({
        (i, element) in element
    })
}

func getSquareIndex(_ index: Int) -> Int {
    return (index % 9) / 3 + ((index / 9) / 3) * 3
}

func getSquare(_ puzzle: [Int?], square: Int) -> [Int?] {
    return puzzle.enumerated().filter({ 
        (i, element) in getSquareIndex(i) == square 
    }).map({
        (i, element) in element
    })
}

func getPossibilities(row: [Int?], col: [Int?], square: [Int?]) -> Set<Int> {
    let used = Set((row + col + square).filter({ element in element != nil }).map({
        (element: Int?) -> Int in element!
    }))
    var possibilities = Set(1...9)
    possibilities.subtract(used)
    return possibilities
}

func solve(_ puzzle: [Int?]) -> [Int?] {
    var solved = Array(puzzle)
    var changed = true
    while changed {
        changed = false
        for i in 0..<solved.count {
            if solved[i] == nil {
                let possibilities = getPossibilities(
                    row: getRow(solved, row:i), 
                    col: getColumn(solved, col:i), 
                    square: getSquare(solved, square:getSquareIndex(i)))
                if let value = possibilities.first {
                    solved[i] = value
                    changed = true
                }
            }
        }
    }
    return solved
}

func draw(_ puzzle: [Int?]) {
    for i in 0..<puzzle.count {
        print(puzzle[i] ?? ".", terminator:"")
        if (i+1) % 9 == 0 {
            print("")
        }
    }
}

if (CommandLine.arguments.count < 2) {
    print("Usage: swift solver.swift <puzzle>")
    print("Use dots for empty spaces, e.g. 12..5.78.")
} else {
    let puzzle = CommandLine.arguments[1].characters.map({
        (square: Character) -> Int? in Int(String(square))
    })
    if puzzle.count != 81 {
        print("Puzzle is not a 9x9 Sudoku")
    }
    else {
        let solved = solve(puzzle)
        draw(solved)
    }
}
