open AdventOfCode2015
open System.IO

let inputFilePath = Path.Combine(__SOURCE_DIRECTORY__, "data", "input.txt")

let upOrDown c =
    match c with
    | '(' -> 1
    | ')' -> -1
    | _ -> 0

let part1 (allChars: char[]) : int =
    allChars |> Array.map upOrDown |> Array.sum

let part2 (allChars: char[]) : int option =
    allChars
    |> Seq.map upOrDown
    |> Seq.scan (+) 0
    |> Seq.tryFindIndex (fun floor -> floor < 0)

[<EntryPoint>]
let main _ : int =
    let allChars = Input.readChars inputFilePath
    let sum = part1 allChars
    let basementPosition = part2 allChars
    printfn "Part 1: %d" sum
    match basementPosition with
    | Some position -> printfn "Part 2: %d" position
    | None -> printfn "Part 2: Santa never enters the basement."
    0
