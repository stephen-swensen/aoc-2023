module D01P1

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
        |> Seq.filter (fun (x,y) -> x <> y)
    let (x,y) =
        possibilities
        |> Seq.find (fun (x,y) ->
            input.[x] + input.[y] = 2020)
    input.[x] * input.[y]
