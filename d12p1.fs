module Day12Part1

///A correct mod implementation that handles negative numbers
let inline (%%) n m =
    ((n % m) + m) % m

let readInput fileName =
    let lines = System.IO.File.ReadAllLines fileName
    lines 
    |> Seq.map (fun line -> line.[0], line.Substring(1, line.Length - 1) |> int)
    |> Seq.toList

let move (x, y) (action, value) =
    match action with
    | 'N' -> x, y + value
    | 'S' -> x, y - value
    | 'W' -> x + value, y
    | 'E' -> x - value, y
    | _ -> failwithf "Invalid action: %A" action

let rotate face degrees =
    if degrees % 90 <> 0 then
        failwithf "Unexcepted degree rotation (must be divisible by 90): %i" degrees

    let step = degrees / 90 
    let compass = ['N'; 'E'; 'S'; 'W']
    let facePosition = compass |> List.findIndex ((=)face)
    let rotatedPosition = (facePosition + step) %% compass.Length
    compass.[rotatedPosition]

let travel input = 
    (('E', (0,0)), input)
    ||> List.fold (fun (face, (x, y)) (action, value) ->
        match action with
        | 'N' | 'S' | 'W' | 'E' -> face, move (x, y) (action, value)
        | 'F' -> face, move (x, y) (face, value)
        | 'L' -> (rotate face -value), (x, y)
        | 'R' -> (rotate face value), (x, y)
        | _ -> failwithf "Invalid action: %A" action)

let run fileName =
    let input = readInput fileName
    let _, (x,y) = travel input
    abs x + abs y

printfn "%A" (run "d12p1.input") //1457

let testRotate () =
    let scenarios = [
        'E', 0, 'E'
        'E', 90, 'S'
        'E', 180, 'W'
        'E', 270, 'N'
        'E', 360, 'E'
        'E', 450, 'S'
        'E', -90, 'N'
        'E', -180, 'W'
        'E', -270, 'S'
        'E', -360, 'E'
        'E', -450, 'N'
    ]
    for (face, degree, expected) as scenario in scenarios do
        let actual = rotate face degree
        if actual <> expected then
            failwithf "Actual=%A did not equal expected in scenario=%A" actual scenario

    printfn "All testRotate scenarios passed"

//testRotate ()
