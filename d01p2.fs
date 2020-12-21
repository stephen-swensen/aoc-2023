module D01P2

let parseInput inputReader =
    let lines = inputReader.ReadAllLines ()
    lines
    |> Seq.map int
    |> Seq.toArray

let run inputReader =
    let input = parseInput inputReader
    let indices = Seq.init input.Length id
    let possibilities =
        (indices, indices)
        ||> Seq.allPairs
        |> Seq.allPairs indices
        |> Seq.map (fun (z, (x, y)) -> x,y,z)
        |> Seq.filter (fun (x,y,z) -> x <> y && y <> z && x <> z)
    let (x,y,z) =
        possibilities
        |> Seq.find (fun (x,y,z) ->
            input.[x] + input.[y] + input.[z] = 2020)
    input.[x] * input.[y] * input.[z]
