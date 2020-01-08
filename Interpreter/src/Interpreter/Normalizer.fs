/// Содержит нормализатор лямбда-исчисления.
module Normalizer

/// Лямбда-терм.
[<StructuredFormatDisplay("{String}")>]
type Term = 
    | Variable of string
    | Application of Term * Term
    | LambdaAbstraction of string * Term

    /// Converts expression to string.
    member this.String =
        match this with
            | Variable name -> name
            | Application (func, arg) -> sprintf "(%A %A)" func arg
            | LambdaAbstraction (param, body) -> sprintf "λ%s.%A" param body

    /// Converts expression to string.
    override this.ToString() = this.String

/// Получить локальный алфавит аргумента из свободных переменных.
let getLocalFreeAlphabet argument =
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
let performAlphaConversion conflictNames outerTerm =
    let rec perform term =
        match term with
        | Variable(name) -> 
            if List.contains name conflictNames then 
                Variable((string)(name.ToString().ToUpperInvariant()))
            else
                term
        | Application(func, argument) -> 
            Application(perform func , perform argument )
        | LambdaAbstraction(name, nextTerm) ->
            if List.contains name conflictNames then
                LambdaAbstraction((string)(name.ToString().ToUpperInvariant()), perform nextTerm )                                                                                    
            else
                LambdaAbstraction(name, perform nextTerm)   

    let rec findLambda innerTerm =       
        match innerTerm with 
        | LambdaAbstraction(_, _) -> perform innerTerm            
        | Application(func, arg) -> Application(findLambda func, findLambda arg)
        | Variable(_) -> innerTerm

    findLambda outerTerm

/// Выполнить подстановку.
let performSubstitution replacementName termForSubstitution argument =
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
let countApplications outerTerm =
    let rec count term sum  =
        match term with
        | Application(func, argumenmt) -> 
             (count func (sum + 1)) 
             |> 
             (count argumenmt)
        | Variable(_) -> sum
        | LambdaAbstraction(_, nextTerm) -> count nextTerm sum 

    count outerTerm 0 

/// Нормализовать терм.
let normalizeTerm outerTerm =
    let applicationsNumber = countApplications outerTerm

    let rec findAndReduceRedex term =
        match term with
        | Variable(_) -> term
        | LambdaAbstraction(name, nextTerm) -> LambdaAbstraction(name, findAndReduceRedex nextTerm)
        | Application(func, argument) ->
            match func with
            | Variable(_) -> Application(func, findAndReduceRedex argument)
            | Application(_, _) -> Application(findAndReduceRedex func, findAndReduceRedex argument)
            | LambdaAbstraction(name, nextTerm) -> 
                let freeAlphabet = getLocalFreeAlphabet argument                                
                let convertedNextTerm = performAlphaConversion freeAlphabet nextTerm                            
                
                performSubstitution name convertedNextTerm argument

    let rec loop step term =
        if step > (applicationsNumber) then term
        else 
            let newTerm = findAndReduceRedex term
            loop (step + 1) newTerm

    loop 0 outerTerm        