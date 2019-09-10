module Interpreter

    type LambdaTree =   
        | Term
        | Lambda of LambdaTree

    let normalize lambda =
        let rec loop =
            ()
        loop