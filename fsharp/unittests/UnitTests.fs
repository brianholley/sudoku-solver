

let testFunction fn name word expected =
    let actual = fn word
    let equal = (actual = expected)
    printfn "%s: %s (Word=%s Expected=%s Actual=%s)" (if equal then "PASS" else "FAIL") name word expected actual

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
