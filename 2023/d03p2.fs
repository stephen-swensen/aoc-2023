module D03P2

open System
open System.Collections.Generic

type Part = { Symbol: char; Coords: int * int }

let isSymbol c = c <> '.' && not (isDigit c)

//as we gather digits into a buffer (part number), we look around for any adjacent symbols (engine part)
let offsets =
  [ (0, -1) //left
    (-1, -1) //left / up
    (-1, 0) //up
    (-1, 1) //up / right
    (0, 1) //right
    (1, 1) //down / right
    (1, 0) //down
    (1, -1) ] //down / left

let parseInput inputReader =
  let rows = inputReader.ReadAllRows()

  seq {
    for i in 0 .. rows.Length - 1 do
      let mutable buffer = ""
      let hits = HashSet<Part>()

      let flushBuffer () =
        seq {
          if buffer <> "" && hits.Count > 0 then
            let num = Int32.Parse(buffer)

            for hit in hits do
              yield hit, num

          buffer <- ""
          hits.Clear()
        }

      for j in 0 .. rows[i].Length - 1 do
        let c = rows[i][j]

        if isDigit c then
          buffer <- buffer + (string c)

          for x, y in offsets do
            let i', j' = (i + x, j + y)

            if
              not (i' = -1 || j' = -1 || i' = rows[i].Length || j' = rows[i].Length)
              && isSymbol (rows[i'][j'])
            then
              ignore
              <| hits.Add(
                { Symbol = rows[i'][j']
                  Coords = (i', j') }
              )
        else
          yield! flushBuffer ()

        if j = rows[i].Length - 1 then
          yield! flushBuffer ()
  }

let run inputReader =
  let input = parseInput inputReader

  input
  |> Seq.groupBy fst
  |> Seq.filter (fun (part, nums) -> part.Symbol = '*' && nums |> Seq.length = 2)
  |> Seq.map snd
  |> Seq.map (fun nums -> nums |> Seq.map snd |> Seq.fold (*) 1)
  |> Seq.sum

//----------------------------------------------------------
//Tests
open NUnit.Framework
open Swensen.Unquote

[<Test>]
let ``sample test`` () =
  let text =
    """467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."""

  let reader = InputReader.FromString text
  run reader =! 467835
