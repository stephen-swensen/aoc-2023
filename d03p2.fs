module D03P2

let parseInput inputReader =
    let lines = inputReader.ReadAllLines ()
    lines
    |> Array.map (_.ToCharArray())

let move (map: char[][]) (right, down) (curI, curJ) =
    let nextJ = curJ + down
    if nextJ >= map.Length then
        None
    else
        let row = map.[nextJ]
        let nextI = (curI + right) %% row.Length
        Some (row.[nextI], (nextI, nextJ))

let countSlopeTrees input slope =
    (0,0)
    |> Seq.unfold (move input slope)
    |> Seq.filter ((=) '#')
    |> Seq.length

let run inputReader =
    let input = parseInput inputReader
    let slopes = [
        1,1
        3,1
        5,1
        7,1
        1,2 ]
    (1I, slopes)
    ||> Seq.fold (fun state slope -> ((countSlopeTrees input slope) |> bigint) * state)
