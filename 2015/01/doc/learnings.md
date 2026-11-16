# 2015 Day 1 – Learnings

**Pattern:** a sequence of instructions, each changing a running state by some amount.

1. Turn each instruction into a floor change, using `upOrDown`:

   | Character | Meaning | Floor change |
   | --- | --- | --- |
   | `(` | go up one floor | `+1` |
   | `)` | go down one floor | `-1` |

2. Combine the floor changes depending on what is asked:
   - only the **final state** → reduce them (sum / fold)
   - the **moment a condition first holds** → produce every intermediate state (scan) and search it

## Functions used

| Function | General idea | Why it fit here |
| --- | --- | --- |
| `Array.sumBy f` | Map and sum in one pass, a shortcut for `map` + `sum` (a specialised `fold`). | Part 1 only needs the final floor, which is the sum of all deltas. |
| `Seq.map f` | Transform each element lazily. | Turns characters into floor deltas without creating an intermediate array. |
| `Seq.scan f seed` | Like `fold`, but returns **every** intermediate state, including the seed. | Part 2 needs the floor after *each* step, not just at the end. |
| `Seq.tryFindIndex pred` | Index of the first element that matches, or `None`. | Finds the first step that reaches the basement, and handles the "never" case safely. |

## Insights

- **`scan` = `fold` with history.** If the question is "when does X first happen?", use `scan`. If it's "what's the result at the end?", use `fold`/`sum`.
- **The seed fixes the indexing for free.** `scan` emits the seed (floor 0) at index 0, so the state after character *n* is at index *n*. That matches the puzzle's 1-based positions with no `+ 1`.
- **Laziness gives early exit.** `Seq` is lazy, so `tryFindIndex` stops at the first match instead of scanning the whole input.
- **`option` over sentinels.** `tryFindIndex` returns `int option` rather than `-1`, so the "never reaches the basement" case has to be handled explicitly.
