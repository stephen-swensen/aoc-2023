module D02P2

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
  let games = parseInput inputReader
  let minSets =
    games
    |> Seq.map (fun game ->
      game.CubeSets
      |> Set.unionMany
      |> Seq.groupBy fst
      |> Seq.map (fun (cube, cnts) -> cube, cnts |> Seq.map snd |> Seq.max))

  minSets
  |> Seq.map (fun minSet -> minSet |> Seq.map snd |> Seq.fold (*) 1)
  |> Seq.sum
