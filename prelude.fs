[<AutoOpen>]
module Prelude
#nowarn "42"

///A correct mod implementation (whereas (%) is the remainder operator and may produce negative results)
let inline (%%) n m =
    ((n % m) + m) % m

let inline xor (x:bool) (y:bool) = (# "xor" x y : bool #)