module D12P1

let parseInput inputReader =
    let lines = inputReader.ReadAllLines ()
    lines
    |> Seq.map (fun line -> line.[0], line.Substring(1, line.Length - 1) |> int)
    |> Seq.toList

let move (x, y) (action, value) =
    match action with
    | 'N' -> x, y + value
    | 'S' -> x, y - value
    | 'W' -> x + value, y
    | 'E' -> x - value, y
    | _ -> failwithf $"Invalid action: %A{action}"

let rotate face degrees =
    if degrees % 90 <> 0 then
        failwithf $"Unexpected degree rotation (must be divisible by 90): %i{degrees}"

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
        | _ -> failwithf $"Invalid action: %A{action}")

let run inputReader =
    let input = parseInput inputReader
    let _, (x,y) = travel input
    abs x + abs y

module Tests =
    open NUnit.Framework
    open Swensen.Unquote

    [<Test>]
    let ``rotate scenarios`` () =
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
            test <@ ignore scenario; actual = expected@>

    [<Test>]
    let ``sample input`` () =
        let text = """F10
N3
F7
R90
F11"""
        let reader = InputReader.FromString text
        run reader =! 25
