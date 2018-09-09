open System

let puzzle (input:string) = 
    input.ToCharArray(0, 81) |> Seq.map (fun c -> 
        match c with
        | '.' -> -1
        | c when char.IsDigit c -> int c - int '0'
        | _ -> -1)

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
