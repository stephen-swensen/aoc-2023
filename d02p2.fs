module D02P2

open System

type Policy = { Pos1: int; Pos2: int; Letter: char }

let parseLine (line:string) =
    let parts = line.Split([|'-'; ' '; ':'|], StringSplitOptions.RemoveEmptyEntries)
    { Pos1=(parts.[0] |> int) - 1
      Pos2=(parts.[1] |> int) - 1
      Letter=parts.[2] |> char }, parts.[3]

let parseInput inputReader =
    let lines = inputReader.ReadAllLines ()
    lines
    |> Seq.map parseLine
    |> Seq.toList

let countValidPasswords input =
    let validPasswords =
        input
        |> Seq.filter (fun (policy, password:string) ->
            xor (password.[policy.Pos1] = policy.Letter)
                (password.[policy.Pos2] = policy.Letter))

    validPasswords |> Seq.length

let run inputReader =
    let input = parseInput inputReader
    countValidPasswords input
