[<AutoOpen>]
module Prelude
#nowarn "42"

open System

//we alias checked operators globally since a lot of our puzzels trigger overflow conditions

///Checked.(+)
let inline (+) x y = Checked.(+) x y
///Checked.(-)
let inline (-) x y = Checked.(-) x y
///Checked.(*)
let inline (*) x y = Checked.(*) x y

///A correct mod implementation (whereas (%) is the remainder operator and may produce negative results)
let inline (%%) n m =
    ((n % m) + m) % m

///An optimized exclusive-or implementation. Logically equivalent to (<>)
let inline xor (x:bool) (y:bool) = (# "xor" x y : bool #)

///An interface for puzzel file input
type InputReader = {
    ReadAllLines: unit -> string []
    ReadAllText: unit -> string
} with
    static member FromFile path =
        { ReadAllLines = (fun () -> IO.File.ReadAllLines path)
          ReadAllText = (fun () -> IO.File.ReadAllText path) }
    static member FromString (text:string) =
        { ReadAllLines = (fun () -> text.Split([|"\r\n"; "\n"|], StringSplitOptions.None) )
          ReadAllText = (fun () -> text) }