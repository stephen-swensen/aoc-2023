module D01P2

let readInput fileName =
    let lines = System.IO.File.ReadAllLines fileName
    lines 
    |> Seq.map int
    |> Seq.toArray

let run fileName =
    let input = readInput fileName
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
