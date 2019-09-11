module Interpreter

    // ((λa.(λb.b b) (λb.b b)) b) ((λc.(c b)) (λa.a))
    type LambdaTree<'a> =           
        | LeftBracket
        | RightBracket
        | End
        | LambdaArgument of LambdaTree<'a>
        | AbstractArgument of LambdaTree<'a>
        | Lambda of LambdaTree<'a>

    let normalize lambda =
        let rec loop =
            ()
        loop