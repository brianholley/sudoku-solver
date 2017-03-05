import Foundation

if (CommandLine.arguments.count < 2) {
    print("Usage: swift solver.swift <puzzle>")
    print("Use dots for empty spaces, e.g. 12..5.78.")
} else {
    print("Hello, Sudoku Solver!")
}
