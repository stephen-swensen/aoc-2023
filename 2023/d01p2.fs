module D01P2

open System
open System.Text.RegularExpressions

let tokenize (line: String) =
  //overlapping matches: https://stackoverflow.com/a/320478
  let ms = Regex.Matches(line, @"(?=(one|two|three|four|five|six|seven|eight|nine|[0-9]))")
  ms |> Seq.map (_.Groups[1].Value) |> Seq.toList

let decodeLine (line: String) =
  let numbers =
    tokenize line
    |> Seq.map (function
      | "one" -> "1"
      | "two" -> "2"
      | "three" -> "3"
      | "four" -> "4"
      | "five" -> "5"
      | "six" -> "6"
      | "seven" -> "7"
      | "eight" -> "8"
      | "nine" -> "9"
      | s -> s)
    |> Seq.toArray

  (numbers[0] + numbers[numbers.Length - 1])
  |> int

let run inputReader =
  inputReader.ReadAllLines()
  |> Seq.sumBy decodeLine

//----------------------------------------------------------
//Tests
open NUnit.Framework
open Swensen.Unquote

[<Test>]
let ``tokenize test`` () =
  tokenize "zoneight234" =! ["one"; "eight"; "2"; "3"; "4" ]
  tokenize "7pqrstsixteen" =! ["7"; "six" ]

[<Test>]
let ``decodeLine test`` () =
  decodeLine "pqr3stu8vwx" =! 38
  decodeLine "a1b2c3d4e5f" =! 15
  decodeLine "a1btwoc3d4e5f" =! 15

  decodeLine "two1nine" =! 29
  decodeLine "eightwothree" =! 83
  decodeLine "abcone2threexyz" =! 13
  decodeLine "xtwone3four" =! 24
  decodeLine "4nineeightseven2" =! 42
  decodeLine "zoneight234" =! 14
  decodeLine "7pqrstsixteen" =! 76

  decodeLine "abc2x3oneight" =! 28

[<Test>]
let ``run test`` () =
  let ir = InputReader.FromString("""two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen""")
  run ir =! 281
