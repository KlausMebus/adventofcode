# 2015 Day 2 - Learnings

## Pipelines and function composition

`|>` passes a value to a function, keeping the data flow readable from left to right:

```fsharp
line.Split 'x' |> Array.map int
```

`>>` combines two functions into a new function: apply the first, then pass its result to the second.

```fsharp
parsePackage >> getRibbonLength
```

This is equivalent to:

```fsharp
fun line -> getRibbonLength (parsePackage line)
```

The composed function can be passed directly to `Seq.sumBy`:

```fsharp
packageLines
|> Seq.sumBy (parsePackage >> getRibbonLength)
```

Each line is parsed, its ribbon length is calculated, and the lengths are summed without creating an intermediate collection.

**Distinction:** `|>` moves a value through functions; `>>` builds a function from functions.

## Pattern matching with guards

A pattern binds values; a `when` guard adds a condition that must also hold:

```fsharp
let d1, d2 =
    match l, w with
    | a, b when a <= b -> a, b
    | a, b -> b, a
```

Branches are tried from top to bottom. The first branch keeps the order when `l <= w`; otherwise, the second swaps the values. This establishes `d1 <= d2`.

That invariant makes selecting the two smallest dimensions straightforward:

```fsharp
match d1, d2, h with
| a, b, c when b <= c -> a, b
| a, _, c -> a, c
```

If `h` is at least the larger of the first two dimensions, keep both. Otherwise, replace the larger one with `h`. `_` discards a value that is not needed. The resulting pair contains the two smallest dimensions, but is not necessarily sorted.

## Small functions are building blocks for composition

Giving each operation its own function makes it independently understandable, reusable, and testable:

| Function | Responsibility |
| --- | --- |
| `parsePackage` | Convert a line into a dimensions tuple. |
| `getTwoSmallestDimensions` | Select the dimensions needed for the smallest perimeter. |
| `getVolume` | Calculate the volume needed for the bow. |
| `getRibbonLength` | Combine the perimeter and bow requirements. |

Their types define how they fit together:

```text
parsePackage:    string -> int * int * int
getRibbonLength: int * int * int -> int
composition:    string -> int
```

Because the output type of `parsePackage` matches the input type of `getRibbonLength`, they compose directly with `>>`.

**Takeaway:** extract meaningful transformations into functions, then assemble them into the overall calculation. Composition becomes a natural consequence of clear responsibilities and compatible types.