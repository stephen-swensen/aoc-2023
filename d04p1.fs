module D04P1

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
    |> dict)
  |> Seq.toList

let validKeys = Set <| [ "byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid" ]

let run inputReader =
  let passports = parseInput inputReader

  passports
  |> Seq.map (fun p -> p.Keys |> Set)
  |> Seq.filter (_.IsSupersetOf(validKeys))
  |> Seq.length

module Tests =
  open NUnit.Framework
  open Swensen.Unquote

  [<Test>]
  let ``sample input`` () =
    let text =
      """ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in"""

    let reader = InputReader.FromString text
    run reader =! 2
