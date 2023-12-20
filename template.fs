module TEMPLATE

open System

let parseInput inputReader =
  let lines = inputReader.ReadAllLines()
  lines

let run inputReader =
  let input = parseInput inputReader
  0

//----------------------------------------------------------
//Tests
open NUnit.Framework
open Swensen.Unquote

[<Test; Ignore("Template Test")>]
let ``xxx test`` () =
  let text = """"""
  let reader = InputReader.FromString text
  run reader =! 0
