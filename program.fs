module Program

open System

///Invoke a command following conventions. For example, if cmd = "d12p1", then
///the module function D12P1.run will be invoked with "d12p1.input" as its argument.
let runCommand cmd = 
    let inputFile = cmd + ".input"
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

    runMethod.Invoke(null, [|inputFile :> obj|])

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