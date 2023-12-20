module D02P1

open System

type Cube =
  | Green
  | Blue
  | Red

type Game =
  { Id: int
    CubeSets: ((Cube * int) Set) list }

let parseInput inputReader =
  let lines = inputReader.ReadAllLines()

  lines
  |> Seq.map (fun line ->
    let gameString, setsString =
      let top = line.Split(":", StringSplitOptions.TrimEntries)
      top[0], top[1]

    let gameId = gameString.Split(' ')[1] |> int

    let sets =
      setsString.Split(';', StringSplitOptions.TrimEntries)
      |> Seq.map (fun setString ->
        let colorStrings = setString.Split(',', StringSplitOptions.TrimEntries)

        colorStrings
        |> Seq.map (fun colorString ->
          let parts = colorString.Split(' ', StringSplitOptions.TrimEntries)
          let count = parts[0] |> int

          let cube =
            match parts[1] with
            | "green" -> Green
            | "red" -> Red
            | "blue" -> Blue
            | color -> failwithf $"Invalid color %s{color}"

          cube, count)
        |> Set)
      |> Seq.toList

    { Id = gameId; CubeSets = sets })
  |> Seq.toList

let run inputReader =
  let input = parseInput inputReader

  input
  |> Seq.filter (fun game ->
    game.CubeSets
    |> Seq.forall (fun cubeSet ->
      cubeSet
      |> Seq.forall (fun (cube, count) ->
        match cube with
        | Red when count <= 12 -> true
        | Green when count <= 13 -> true
        | Blue when count <= 14 -> true
        | _ -> false)))
  |> Seq.sumBy _.Id
