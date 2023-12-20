[<AutoOpen>]
module Prelude

#nowarn "42"

open System

//we alias checked operators globally since a lot of our puzzles trigger overflow conditions

///Checked.(+)
let inline (+) x y = Checked.(+) x y
///Checked.(-)
let inline (-) x y = Checked.(-) x y
///Checked.(*)
let inline (*) x y = Checked.(*) x y
///Checked.(/)
let inline (/) x y = Checked.(*) x y

///A correct mod implementation (whereas (%) is the remainder operator and may produce negative results)
let inline (%%) n m = ((n % m) + m) % m

///An optimized exclusive-or implementation. Logically equivalent to (<>)
let inline xor (x: bool) (y: bool) = (# "xor" x y : bool #)

let (|Int32|_|) (s: string) =
  match Int32.TryParse s with
  | true, v -> Some v
  | false, _ -> None

let isAsciiNumber (c: Char) =
  let c = c |> int
  c >= 48 && c <= 57

///If a the regular expression match is a Success, then returns m.value * m.Groups[1..] (explicit group values).
///If input is null, the match fails but no exception is thrown.
let (|Regex|_|) pattern input =
  if input |> isNull then
    None
  else
    let m = Text.RegularExpressions.Regex.Match(input, pattern)

    if m.Success then
      Some(m.Value, [ for x in m.Groups -> x.Value ].Tail)
    else
      None

///An interface for puzzle file input
type InputReader =
  { ReadAllLines: unit -> string[]
    ReadAllText: unit -> string }

  static member FromFile path =
    { ReadAllLines = (fun () -> IO.File.ReadAllLines path)
      ReadAllText = (fun () -> IO.File.ReadAllText path) }

  static member FromString(text: string) =
    { ReadAllLines = (fun () -> text.Split([| "\r\n"; "\n" |], StringSplitOptions.None))
      ReadAllText = (fun () -> text) }
