namespace Day01.Tests

open System
open System.IO
open Xunit

module ProgramTests =
    let private readInput () =
        let inputPath = Path.Combine(AppContext.BaseDirectory, "data", "input.txt")
        AdventOfCode2015.Input.readChars inputPath

    [<Theory>]
    [<InlineData("(())", 0)>]
    [<InlineData("()()", 0)>]
    [<InlineData("(((", 3)>]
    [<InlineData("(()(()(", 3)>]
    [<InlineData("))(((((", 3)>]
    [<InlineData("())", -1)>]
    [<InlineData("))(", -1)>]
    [<InlineData(")))", -3)>]
    [<InlineData(")())())", -3)>]
    let ``Part1_ProcessesInstructions_ReturnsFinalFloor`` (instructions: string) (expectedFloor: int) =
        let actualFloor = Program.part1 (instructions.ToCharArray())
        Assert.Equal(expectedFloor, actualFloor)

    [<Fact>]
    let ``Part1_ActualInput_ReturnsPuzzleAnswer`` () =
        let actualFloor = Program.part1 (readInput ())
        Assert.Equal(74, actualFloor)

    [<Theory>]
    [<InlineData(")", 1)>]
    [<InlineData("()())", 5)>]
    let ``Part2_ReachesBasement_ReturnsFirstBasementPosition`` (instructions: string) (expectedPosition: int) =
        let actualPosition = Program.part2 (instructions.ToCharArray())
        Assert.Equal(Some expectedPosition, actualPosition)

    [<Fact>]
    let ``Part2_ActualInput_ReturnsPuzzleAnswer`` () =
        let actualPosition = Program.part2 (readInput ())
        Assert.Equal(Some 1795, actualPosition)

    [<Theory>]
    [<InlineData("")>]
    [<InlineData("(")>]
    [<InlineData("()()")>]
    let ``Part2_NeverReachesBasement_ReturnsNone`` (instructions: string) =
        let actualPosition = Program.part2 (instructions.ToCharArray())
        Assert.Equal(None, actualPosition)
