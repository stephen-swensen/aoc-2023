module D04P2

open System

let parseInput inputReader =
  let lines = inputReader.ReadAllText()
  let blocks = lines.Split([| "\n\n"; "\r\n" |], StringSplitOptions.None)

  blocks
  |> Seq.map (fun block ->
    let pairs =
      block.Split([| "\n"; "\r\n"; " " |], StringSplitOptions.RemoveEmptyEntries)

    pairs
    |> Seq.map (fun pair ->
      let kv = pair.Split([| ':' |])
      kv.[0], kv[1])
    |> Map)
  |> Seq.toList

let isValidField (key, value) =
  let isNumberWithinRange lowerBound upperBound s =
    match s with
    | Int32 v -> v >= lowerBound && v <= upperBound
    | _ -> false

  let isValidHeight s =
    let pattern = @"^([0-9]*)(cm|in)$"

    match s with
    | Regex pattern (_, [ Int32 v; "cm" ]) when v >= 150 && v <= 193 -> true
    | Regex pattern (_, [ Int32 v; "in" ]) when v >= 59 && v <= 76 -> true
    | _ -> false

  let isValidHairColor s =
    match s with
    | Regex @"^#([a-f0-9]){6}$" _ -> true
    | _ -> false

  let isValidEyeColor s =
    match s with
    | Regex @"^amb|blu|brn|gry|grn|hzl|oth$" _ -> true
    | _ -> false

  let isValidPid s =
    match s with
    | Regex @"^[0-9]{9}$" _ -> true
    | _ -> false

  let validationRules =
    Map.ofSeq
    <| [ "byr", isNumberWithinRange 1920 2002
         "iyr", isNumberWithinRange 2010 2020
         "eyr", isNumberWithinRange 2020 2030
         "hgt", isValidHeight
         "hcl", isValidHairColor
         "ecl", isValidEyeColor
         "pid", isValidPid
         "cid", (fun _ -> true) ]

  match validationRules |> Map.tryFind key with
  | None -> false
  | Some rule -> rule value

let hasRequiredFields (p: Collections.Generic.IDictionary<string, string>) =
  let validKeys = Set <| [ "byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid" ]

  (p.Keys |> Set).IsSupersetOf validKeys

let isValidPassport p =
  hasRequiredFields p
  && (p |> Seq.forall (fun kvp -> isValidField (kvp.Key, kvp.Value)))

let run inputReader =
  let passports = parseInput inputReader

  passports |> Seq.filter isValidPassport |> Seq.length

module Tests =
  open NUnit.Framework
  open Swensen.Unquote

  [<Test>]
  let ``validation scenarios`` () =
    let scenarios =
      [ ("byr", "1920"), true
        ("byr", "1919"), false
        ("hgt", "59in"), true
        ("hgt", "58in"), false
        ("hgt", "150cm"), true
        ("hgt", "149cm"), false
        ("hcl", "#abdf14"), true
        ("hcl", "#abdf145"), false
        ("hcl", "abdf12"), false
        ("hcl", "abdf1234"), false
        ("hcl", "#abdg34"), false
        ("ecl", "blu"), true
        ("ecl", "amb"), true
        ("ecl", "xxx"), false
        ("pid", "123456789"), true
        ("pid", "1234567890"), false ]

    for scenario, expected in scenarios do
      test <@ isValidField scenario = expected @>

  [<Test>]
  let ``isValidPassport valid sample`` () =
    let text =
      """pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980 hcl:#623a2f"""

    let passport = trap <@ (parseInput (InputReader.FromString text)).Head @>
    test <@ hasRequiredFields passport @>
    test <@ isValidPassport passport @>
