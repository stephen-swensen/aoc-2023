module D01P1

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
        |> Seq.filter (fun (x,y) -> x <> y)
    let (x,y) =
        possibilities
        |> Seq.find (fun (x,y) -> 
            input.[x] + input.[y] = 2020)
    input.[x] * input.[y]
