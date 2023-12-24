module D04P1

open System

type Card =
  { WinningNumbers: int list
    ScratchedNumbers: int list }

let cardscore wins = pown 2 (wins - 1)

let parseInput inputReader =
  let lines = inputReader.ReadAllLines()

  lines
  |> Seq.map (fun line ->
    let arrays =
      (line.Split(':', StringSplitOptions.TrimEntries)[1])
        .Split('|', StringSplitOptions.TrimEntries)
      |> Array.map (fun nums ->
        nums.Split(' ', StringSplitOptions.TrimEntries ||| StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map int
        |> Seq.toList)

    { WinningNumbers = arrays[0]
      ScratchedNumbers = arrays[1] })

let run inputReader =
  let input = parseInput inputReader

  input
  |> Seq.sumBy (fun card ->
    let winners =
      card.ScratchedNumbers
      |> Seq.filter (fun sn -> card.WinningNumbers |> List.contains sn)
      |> Seq.length

    cardscore winners)

//----------------------------------------------------------
//Tests
open NUnit.Framework
open Swensen.Unquote

[<Test>]
let ``cardscore test`` () =
  cardscore 0 =! 0
  cardscore 1 =! 1
  cardscore 2 =! 2
  cardscore 3 =! 4
  cardscore 4 =! 8

[<Test>]
let ``sample input test`` () =
  let text =
    """Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"""

  let reader = InputReader.FromString text
  run reader =! 13
