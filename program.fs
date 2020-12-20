module Program

open System

[<EntryPoint>]
let main args =
    if args.Length <> 1 then
        failwithf "Excepted command like 'd12p1' as program argument, but instead got these args: %A" args

    let cmd = args.[0]
    let inputFile = cmd + ".input"
    let moduleName = cmd.ToUpper()
    let moduleType = Type.GetType(moduleName)
    let runMethod =
        moduleType.GetMethods()
        |> Seq.find (fun mi -> mi.Name = "run" && mi.GetParameters().Length = 1)

    let result = runMethod.Invoke(null, [|inputFile :> obj|])
    printfn "%s: %A" cmd result
    //stdout.WriteLine (D12P1.run "d12p1.input")
    0