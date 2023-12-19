module D02P1

open System

type Policy = { Min: int; Max: int; Letter: char }

let parseLine (line:string) =
    let parts = line.Split([|'-'; ' '; ':'|], StringSplitOptions.RemoveEmptyEntries)
    { Min=parts.[0] |> int
      Max=parts.[1] |> int
      Letter=parts[2] |> char }, parts[3]

let parseInput inputReader =
    let lines = inputReader.ReadAllLines ()
    lines
    |> Seq.map parseLine
    |> Seq.toList

let countValidPasswords input =
    let validPasswords =
        input
        |> Seq.filter (fun (policy, password:string) ->
            let letterCount =
                password.ToCharArray ()
                |> Seq.filter (fun c -> c = policy.Letter)
                |> Seq.length
            letterCount >= policy.Min && letterCount <= policy.Max)

    validPasswords |> Seq.length

let run inputReader =
    let input = parseInput inputReader
    countValidPasswords input

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
