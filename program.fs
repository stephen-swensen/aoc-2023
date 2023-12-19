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
            failwithf $"No input file found for command %s{cmd}, tried given input %s{givenInput} and fallback part 1 input %s{p1input}"

    inputFile, InputReader.FromFile inputFile

///Invoke a command following conventions. For example, if cmd = "d12p1", then
///the module function D12P1.run will be invoked with an InputReader resolved
///from file "d12p1.input" as its argument.
let runCommand cmd =
    let inputFile, inputReader = resolveInputReader cmd
    let moduleName = cmd.ToUpper()

    let moduleType =
        let ty = Type.GetType(moduleName)
        if ty |> isNull then
            failwithf $"Unable to load %s{moduleName} module"
        ty

    let runMethod =
        let mi =
            moduleType.GetMethods()
            |> Seq.tryFind (fun mi -> mi.Name = "run" && mi.GetParameters().Length = 1)
        match mi with
        | Some mi -> mi
        | None ->
            failwithf $"Unable to find `run` method taking single string argument in %s{moduleName} module"

    inputFile, runMethod.Invoke(null, [|inputReader :> obj|])

[<EntryPoint>]
let main args =
    let commands =
        if args.Length = 0 then
            IO.Directory.GetFiles(".", "d??p?.fs")
            |> Seq.map (fun path ->
                let fi = IO.FileInfo(path)
                fi.Name.Substring(0, fi.Name.Length - 3))
        else
            args |> Seq.ofArray

    for cmd in commands |> Seq.sort do
        let sw = Diagnostics.Stopwatch()
        sw.Start ()
        let inputFile, result = runCommand cmd
        sw.Stop ()
        printfn $"%s{cmd} (elapsed=%i{sw.ElapsedMilliseconds}ms, input=%s{inputFile}): %A{result}"
    0
