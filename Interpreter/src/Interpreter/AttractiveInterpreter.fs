/// Использование member'ов в DU для удобства программирования.
module AttractiveInterpreter

/// Лямбда-терм.
type Term = 
    | Variable of char
    | Application of Term * Term
    | LambdaAbstraction of char * Term    

    /// Получить локальный алфавит аргумента из свободных переменных.
    member this.GetLocalFreeAlphabet argument =
        let rec setAlphabet term alphabet ignoreChars =
            match term with
            | Variable(name) ->
                if List.contains name ignoreChars then alphabet
                else name :: alphabet
            | Application(func, arg) ->
                setAlphabet arg (setAlphabet func alphabet ignoreChars) ignoreChars
            | LambdaAbstraction(name, nextTerm) ->
                if List.contains name ignoreChars then 
                    setAlphabet nextTerm alphabet ignoreChars
                else 
                    setAlphabet nextTerm alphabet (name :: ignoreChars)        
            
        setAlphabet argument [] []
        |>
        List.distinct

    /// Выполнить альфа-конверсию.
    member this.PerformAlphaConversion conflictNames outerTerm =
        let rec perform term =
            match term with
            | Variable(name) -> 
                if List.contains name conflictNames then 
                    Variable((char)(name.ToString().ToUpperInvariant()))
                else
                    term
            | Application(func, argument) -> 
                Application(perform func , perform argument )
            | LambdaAbstraction(name, nextTerm) ->
                if List.contains name conflictNames then
                    LambdaAbstraction((char)(name.ToString().ToUpperInvariant()), perform nextTerm )                                                                                    
                else
                    LambdaAbstraction(name, perform nextTerm)   

        let rec findLambda innerTerm =       
            match innerTerm with 
            | LambdaAbstraction(_, _) -> perform innerTerm            
            | Application(func, arg) -> Application(findLambda func, findLambda arg)
            | Variable(_) -> innerTerm

        findLambda outerTerm

    /// Выполнить подстановку.
    member this.PerformSubstitution replacementName termForSubstitution argument =
        let rec perform term =
            match term with
            | Variable(nameInLamdaAbstract) ->
                if nameInLamdaAbstract = replacementName then argument
                else term
            | LambdaAbstraction(name, nextTerm) -> 
                if name = replacementName then term
                else LambdaAbstraction(name, perform nextTerm)
            | Application(func, innerArgument) ->
                Application(perform func, perform innerArgument)

        perform termForSubstitution
    
    /// Сосчитать количество аппликаций.
    member this.CountApplications outerTerm =
        let rec count term sum  =
            match term with
            | Application(func, argumenmt) -> 
                 (count func (sum + 1)) 
                 |> 
                 (count argumenmt)
            | Variable(_) -> sum
            | LambdaAbstraction(_, nextTerm) -> count nextTerm sum 

        count outerTerm 0 

    /// Нормализованный терм.
    member this.Normalized =
        let applicationsNumber = this.CountApplications this

        let rec findAndReduceRedex term =
            match term with
            | Variable(_) -> term
            | LambdaAbstraction(name, nextTerm) -> LambdaAbstraction(name, findAndReduceRedex nextTerm)
            | Application(func, argument) ->
                match func with
                | Variable(_) -> Application(func, findAndReduceRedex argument)
                | Application(_, _) -> Application(findAndReduceRedex func, findAndReduceRedex argument)
                | LambdaAbstraction(name, nextTerm) -> 
                    let freeAlphabet = this.GetLocalFreeAlphabet argument                                
                    let convertedNextTerm = this.PerformAlphaConversion freeAlphabet nextTerm                            
                
                    this.PerformSubstitution name convertedNextTerm argument

        let rec loop step term =
            if step > (applicationsNumber) then term
            else 
                let newTerm = findAndReduceRedex term
                loop (step + 1) newTerm

        loop 0 this        