[<AutoOpen>]
module Prelude

///A correct mod implementation (whereas (%) is the remainder operator and may produce negative results)
let inline (%%) n m =
    ((n % m) + m) % m