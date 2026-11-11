module AdventOfCode2015.Input

open System.IO

/// Reads lines without line terminators, preserving empty lines.
let readLines (path: string) : seq<string> = File.ReadLines path

/// Reads all characters, including any line terminators.
let readChars (path: string) : char[] =
    File.ReadAllText(path).ToCharArray()
