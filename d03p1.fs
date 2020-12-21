module D03P1

let parseInput inputReader =
    let lines = inputReader.ReadAllLines ()
    lines
    |> Array.map (fun line -> line.ToCharArray())

let move (map: char[][]) (right, down) (curI, curJ) =
    let nextJ = curJ + down
    if nextJ >= map.Length then
        None
    else
        let row = map.[nextJ]
        let nextI = (curI + right) %% row.Length
        Some (row.[nextI], (nextI, nextJ))

let run inputReader =
    let input = parseInput inputReader
    (0,0)
    |> Seq.unfold (move input (3,1))
    |> Seq.filter ((=) '#')
    |> Seq.length
