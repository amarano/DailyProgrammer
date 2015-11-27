// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open VhsRecording

[<EntryPoint>]
let main argv = 
    let r = parseString "1530 1600 1555 1645 1600 1630 1635 1715"
    let sch = schedule r
    printfn "%A" sch
    System.Console.ReadKey()
    0 // return an integer exit code
