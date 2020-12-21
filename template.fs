module TEMPLATE

open System

let parseInput inputReader =
    let lines = inputReader.ReadAllLines ()
    lines


let run inputReader =
    let input = parseInput inputReader
    input

module Tests =
    open NUnit.Framework
    open Swensen.Unquote

    [<Test; Ignore("Template Test")>]
    let ``test`` () =
        true =! true