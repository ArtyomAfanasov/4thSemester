module Interpreter

    // ((λa.(λb.b b) (λb.b b)) b) ((λc.(c b)) (λa.a))
    type Token =                   
        | Variable of string
        | LBracket
        | RBracket
        | Dot
        | Lambda

    let normalize (lambda : Token) =
        let rec loop tokens =
            ()
        loop

    // ToDo кол-во проходов = кол-во бета-редукций. Запоминать место с точкой, идти до первого аргумента, 
    // Возобновлять проход от точки до донца.

    [<EntryPoint>]
    let main arg =
        0