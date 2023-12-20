module D01P1

open System

let decodeLine (line: String) =
  let numbers =
    line.ToCharArray()
    |> Array.filter isAsciiNumber

  String([| numbers[0]; numbers[numbers.Length - 1] |])
  |> Int32.Parse

let run inputReader =
  inputReader.ReadAllLines()
  |> Seq.sumBy decodeLine

//----------------------------------------------------------
//Tests
open NUnit.Framework
open Swensen.Unquote

[<Test>]
let ``decodeLine test`` () =
  decodeLine "pqr3stu8vwx" =! 38
  decodeLine "a1b2c3d4e5f" =! 15
