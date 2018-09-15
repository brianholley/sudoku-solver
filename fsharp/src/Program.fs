namespace Wavecrash.Solver

open System

module Program = 
    let parse (input:string): int list = 
        input.ToCharArray(0, 81) |> Seq.map (fun c -> 
            match c with
            | '.' -> -1
            | c when Char.IsDigit c -> int c - int '0'
            | _ -> -1) |> Seq.toList

    [<EntryPoint>]
    let main argv = 
        printfn "%A" argv
        0 // return an integer exit code
