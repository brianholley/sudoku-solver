namespace Wavecrash.Solver

open System

module Puzzle =
    type GridSquare(value: int, possibilities: list<int>) =
        let value = value
        let possibilities = possibilities
        new() = GridSquare(-1, list<int>.Empty)

    let row (puzzle:list<int>) (index:int) : list<int> = 
        let rowStart = index - (index % 9)
        puzzle.[rowStart..rowStart + 8]

    let column (puzzle:list<int>) (index:int) : list<int> = 
        puzzle
            |> Seq.mapi (fun i v -> if i % 9 = index % 9 then Some(v) else None)
            |> Seq.choose id
            |> Seq.toList

    let squareNumber (index:int) = 
        (index % 9) / 3 + ((index / 9) / 3) * 3

    let square (puzzle:list<int>) (index:int) : list<int> = 
        let square = squareNumber index
        puzzle
            |> Seq.mapi (fun i v -> if squareNumber i  = square then Some(v) else None)
            |> Seq.choose id
            |> Seq.toList
