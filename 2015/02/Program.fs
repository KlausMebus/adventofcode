open AdventOfCode2015
open System.IO

let inputFilePath = Path.Combine(__SOURCE_DIRECTORY__, "data", "input.txt")

let parsePackage (line: string) : int * int * int =
    let parts = line.Split 'x' |> Array.map int
    parts.[0], parts.[1], parts.[2]

let getSideAreas (l, w, h) =
    let side1 = l * w
    let side2 = w * h
    let side3 = h * l
    side1, side2, side3


// Returns the two smallest dimensions of the package
let getTwoSmallestDimensions (l, w, h) =
    let d1, d2 =
        match l, w with
        | a, b when a <= b -> a, b
        | a, b -> b, a

    let twoSmallest = 
        match d1, d2, h with
        | a, b, c when b <= c -> a, b
        | a, _, c -> a, c

    twoSmallest

let getVolume (l, w, h) = l * w * h

let getRibbonLength (l, w, h) =
    let d1, d2 = getTwoSmallestDimensions (l, w, h)
    2 * (d1 + d2) + getVolume (l, w, h)

let minimumSide (s1, s2, s3) =
    min s1 (min s2 s3)

let part1 (packageLines: string[]) : int =
    packageLines 
    |> Seq.map parsePackage
    |> Seq.sumBy (fun (l, w, h) ->
        let side1, side2, side3 = getSideAreas (l, w, h)
        2 * (side1 + side2 + side3) + minimumSide (side1, side2, side3)
    )

let part2 (packageLines: string[]) : int =
    packageLines
    |> Seq.sumBy (parsePackage >> getRibbonLength)


[<EntryPoint>]
let main _ : int =
    let input = Input.readLines inputFilePath |> Seq.toArray
    let part1Answer = part1 input
    let part2Answer = part2 input
    printfn "Part 1: %d" part1Answer
    printfn "Part 2: %d" part2Answer
    0
