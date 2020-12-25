module D04P2

open System

let parseInput inputReader =
    let lines = inputReader.ReadAllText ()
    let blocks = lines.Split([|"\n\n"; "\r\n"|], StringSplitOptions.None)
    blocks
    |> Seq.map (fun block ->
        let pairs = block.Split([|"\n"; "\r\n"; " "|], StringSplitOptions.RemoveEmptyEntries)
        pairs
        |> Seq.map (fun pair ->
            let kv = pair.Split([|':'|])
            kv.[0], kv.[1])
        |> Map)
    |> Seq.toList

let isValidField (key, value) =
    let isNumberWithinRange lowerBound upperBound (v:string) =
        match Int32.TryParse v with
        | false, _-> false
        | true, v -> v >= lowerBound && v <= upperBound

    let validationRules = Map.ofSeq <| [
        "byr", isNumberWithinRange 1920 2002
        "iyr", isNumberWithinRange 2010 2020
        "eyr", isNumberWithinRange 2020 2030
        "hgt", fun v -> true
        "hcl", fun v -> true
        "ecl", fun v -> true
        "pid", fun v -> true
    ]

    match validationRules |> Map.tryFind key with
    | None -> true
    | Some rule -> rule value

let hasRequiredFields (p: Collections.Generic.IDictionary<string,string>) =
    let validKeys = Set <| [
        "byr"
        "iyr"
        "eyr"
        "hgt"
        "hcl"
        "ecl"
        "pid"
    ]

    (p.Keys |> Set).IsSupersetOf validKeys

let isValidPassport p =
    hasRequiredFields p
    && (p |> Seq.forall (fun kvp -> isValidField (kvp.Key, kvp.Value)))

let run inputReader =
    let passports = parseInput inputReader

    passports
    |> Seq.filter isValidPassport
    |> Seq.length
