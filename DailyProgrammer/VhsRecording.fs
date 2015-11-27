﻿module VhsRecording

type ProgramSchedule = {StartTime : int; EndTime : int}

let rec parse (times:list<string>) (schedules:list<ProgramSchedule>) = 
    match times with
    | [] -> schedules
    | head::next::tail -> 
        let schedule = {StartTime = (int head); EndTime = (int next)}
        let newSchedules = List.Cons(schedule, schedules)
        parse tail newSchedules
    | [_] -> 
        failwith "The input is not in the correct format"

let parseString (s:string) = 
    parse (s.Split(' ') |> List.ofSeq) []

let rec addToSchedule (scheduleSoFar:list<ProgramSchedule>) (possiblePrograms:list<ProgramSchedule>) (currentlyRecording:ProgramSchedule) = 
    match possiblePrograms with 
    | [] -> scheduleSoFar
    | head::tail -> 
        if head.StartTime <= currentlyRecording.EndTime
        then 
            let newSchedule = List.Cons(head, scheduleSoFar)
            addToSchedule newSchedule tail head
        else
            addToSchedule scheduleSoFar tail currentlyRecording

let schedule (programs:list<ProgramSchedule>) = 

    let schedules = [for i in 0..programs.Length - 1 do yield addToSchedule [] (programs |> List.skip (i + 1) ) programs.[i]]
    schedules |> List.maxBy (fun l -> l.Length)



