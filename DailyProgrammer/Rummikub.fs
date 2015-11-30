module Rummikub

type Color = Red | Black | Purple | Yellow
type StandardTile = {Color: Color ; Value : int}
type Tile = Standard of StandardTile | Joker

let parseColor (c:char) = 
    match c with
    | 'R' -> Red
    | 'B' -> Black
    | 'P' -> Purple
    | 'Y' -> Yellow
    | _ -> failwith "Not a valid color"

let parseTile (t:string) = 
    if t.Length <> 2 then failwith "Tile format is invalid"
    let color = parseColor t.[0]
    let value = int t.[1]
    {Color = color ; Value = value}

let groups (tiles:seq<StandardTile>) = 
    tiles |> Seq.groupBy (fun t -> t.Value) |> Seq.filter(fun g -> (snd g) |> Seq.length >= 3)

let runs (tiles:seq<StandardTile>) = 
    let groups = tiles |> Seq.groupBy(fun t -> t.Color) 
                |> Seq.map(fun x -> snd x) 
                |> Seq.map(fun g -> (g |> Seq.sortBy(fun x -> x.Value)))
                |> Array.ofSeq
    groups

let findRuns (g:array<StandardTile>) = 
    let mutable currentEnd = 0
    let mutable currentStart = 0 
    seq{ 
        for i in 0..(Seq.length g) do
        if g.[i].Value = g.[i].Value - 1 then
            currentEnd <- currentEnd + 1
        else
            if currentEnd > currentStart + 1 then
            yield g.[currentStart..currentEnd]
        }
        
