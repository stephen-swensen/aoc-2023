module D02P1

open System

type Policy = { Min: int; Max: int; Letter: char }

let parseLine (line:string) = 
    let parts = line.Split([|'-'; ' '; ':'|], StringSplitOptions.RemoveEmptyEntries)
    { Min=parts.[0] |> int
      Max=parts.[1] |> int
      Letter=parts.[2] |> char }, parts.[3]


let readInput fileName =
    let lines = System.IO.File.ReadAllLines fileName
    lines 
    |> Seq.map parseLine
    |> Seq.toList

let run fileName =
    let input = readInput fileName
    0

module Tests =
    open NUnit.Framework
    open Swensen.Unquote
    [<Test>]
    let ``parseLine`` () =
        let line = "2-7 s: qwdngzbtsntgzmxz"
        let actual = parseLine line
        let expected = 
            { Min=2
              Max=7
              Letter='s' }, "qwdngzbtsntgzmxz"

        actual =! expected