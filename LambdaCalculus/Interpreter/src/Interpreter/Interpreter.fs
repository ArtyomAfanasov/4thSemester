module Interpreter

    // ((λa.(λb.b b) (λb.b b)) b) ((λc.(c b)) (λa.a))
    type LambdaTree<'a> =                   
        | Test of 'a
        | LambdaArgument of LambdaTree<'a>
        | AbstractArgument of LambdaTree<'a> ref
        | Lambda of LambdaTree<'a>

    let normalize lambda =
        let rec loop =
            ()
        loop

    [<EntryPoint>]
    let main arg =
        let a = Lambda(LambdaArgument(AbstractArgument(ref (AbstractArgument(ref ((Test("a"))))))))
        printfn "%A" a  
        match a with 
        | Lambda(LambdaArgument(x)) -> !x <- AbstractArgument(ref (AbstractArgument(ref ((Test("a"))))))
            
        printfn "%A" a  
        0