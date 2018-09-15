module Wavecrash.Solver.Tests

open System
open Xunit

open Wavecrash.Solver.Program
open Wavecrash.Solver.Puzzle

let EasyPuzzle = ".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57."
let MediumPuzzle = ".5....6.2....3..9..9..8675........2956..7..4328........2679..1..4..6....3.7....6."
let HardPuzzle = ".....4.3......9.67.3.7...857..9......53.7.61......5..226...8.4.87.5......1.4....."

[<Fact>]
let TestPuzzleParse () =
    let puzzle = Program.parse EasyPuzzle
    Assert.Equal(-1, puzzle.[0])
    Assert.Equal(9, puzzle.[2])

