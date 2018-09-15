namespace Wavecrash.Solver

open System

module Puzzle =
    type GridSquare(value: int, possibilities: list<int>) =
        let value = value
        let possibilities = possibilities
        new() = GridSquare(-1, list<int>.Empty)

    let row (puzzle:int[], index:int) = 
        let rowStart = index - (index % 9)
        puzzle.[rowStart..rowStart + 9]

    let column (puzzle:int[], index:int) = 
        puzzle
            |> Seq.mapi (fun i v -> if i % 9 = index % 9 then Some(v) else None)
            |> Seq.choose id

    let squareNumber (index:int) = 
        (index % 9) / 3 + ((index / 9) / 3) * 3

    let square (puzzle:int[], index:int) = 
        let square = squareNumber index
        puzzle
            |> Seq.mapi (fun i v -> if squareNumber i  = square then Some(v) else None)
            |> Seq.choose id
