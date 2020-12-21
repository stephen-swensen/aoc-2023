[<AutoOpen>]
module Prelude
#nowarn "42"

open System

///A correct mod implementation (whereas (%) is the remainder operator and may produce negative results)
let inline (%%) n m =
    ((n % m) + m) % m

///An optimized exclusive or implementation. Logically equivalent to (<>)
let inline xor (x:bool) (y:bool) = (# "xor" x y : bool #)

///An interface for puzzel file input
type InputReader = { ReadAllLines: unit -> string [] } with
    static member FromFile path =
        { ReadAllLines = (fun () -> IO.File.ReadAllLines path) }
    static member FromString (text:string) =
        { ReadAllLines = (fun () -> text.Split([|"\r\n"; "\n"|], StringSplitOptions.None) ) }