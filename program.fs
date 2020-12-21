module Program

open System

///Resolve FileInput from cmd. First we try the cmd + ".input". If that doesn't exist
///we try cmd.Substring(0, cmd.Length - 1) + "1" + ".input". That is, if there is
///no specific part 2 input file, we fall back on part 1 file input since part 2
///file input is often the same as part 1.
let resolveInputReader cmd =
    let givenInput = cmd + ".input"
    let p1input = cmd.Substring(0, cmd.Length - 1) + "1" + ".input"

    let inputFile =
        if IO.File.Exists givenInput then
            givenInput
        elif IO.File.Exists p1input then
            p1input
        else
            failwithf "No input file found for command %s, tried given input %s and fallback part 1 input %s"
                      cmd
                      givenInput
                      p1input

    InputReader.FromFile inputFile

///Invoke a command following conventions. For example, if cmd = "d12p1", then
///the module function D12P1.run will be invoked with an InputReader resolved
///from file "d12p1.input" as its argument.
let runCommand cmd =
    let inputReader = resolveInputReader cmd
    let moduleName = cmd.ToUpper()

    let moduleType =
        let ty = Type.GetType(moduleName)
        if ty |> isNull then
            failwithf "Unable to load %s module" moduleName
        ty

    let runMethod =
        let mi =
            moduleType.GetMethods()
            |> Seq.tryFind (fun mi -> mi.Name = "run" && mi.GetParameters().Length = 1)
        match mi with
        | Some mi -> mi
        | None ->
            failwithf "Unable to find `run` method taking single string argument in %s module" moduleName

    runMethod.Invoke(null, [|inputReader :> obj|])

[<EntryPoint>]
let main args =
    if args.Length <> 1 then
        failwithf "Expected command like 'd12p1' as program argument, but instead got these args: %A" args

    let cmd = args.[0]
    let sw = Diagnostics.Stopwatch()
    sw.Start ()
    let result = runCommand cmd
    sw.Stop ()
    printfn "%s (elapsed=%ims): %A" cmd sw.ElapsedMilliseconds result
    0