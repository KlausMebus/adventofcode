namespace Day02.Tests

open System
open System.IO
open Xunit

module ProgramTests =

    let private readInput () =
        let inputPath = Path.Combine(AppContext.BaseDirectory, "data", "input.txt")
        File.ReadAllLines inputPath

    [<Theory>]
    [<InlineData("2x3x4", 58)>]
    [<InlineData("1x1x10", 43)>]
    let ``Part1_SmallExamples`` (input: string, expected: int) =
        let l, w, h = Program.parsePackage input
        let side1, side2, side3 = Program.getSideAreas (l, w, h)
        let minSide = Program.minimumSide (side1, side2, side3)
        let sideSum = 2 * (side1 + side2 + side3) + minSide
        Assert.Equal(expected, sideSum)

    [<Fact>]
    let ``Part1_ExampleInput_ReturnsExpectedResult`` () =
        let input = readInput ()
        let result = Program.part1 input
        Assert.Equal(1606483, result)

    [<Theory>]
    [<InlineData("2x3x4", 34)>]
    [<InlineData("1x1x10", 14)>]
    let ``Part2_SmallExamples`` (input: string, expected: int) =
        let l, w, h = Program.parsePackage input
        let ribbonLength = Program.getRibbonLength (l, w, h)
        Assert.Equal(expected, ribbonLength)

    [<Fact>]
    let ``Part2_ExampleInput_ReturnsExpectedResult`` () =
        let input = readInput ()
        let result = Program.part2 input
        Assert.Equal(3842356, result)
