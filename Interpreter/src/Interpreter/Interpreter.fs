// передавать каждый раз новый Map для alpha-конверсии, которая будет на лету. -- Заметки для меня :)
module Interpreter
 
type Term = 
    | Variable of char
    | Applique of Term * Term
    | LambdaAbstract of char * Term

/// Выполнить альфа-конверсию.
let prepareToAlphaConversion outerTerm =
    let rec setAlphabet alphabet term =
        match term with
        | Variable(_) -> alphabet
        | Applique(func, argument) -> 
            (setAlphabet alphabet func) @ (setAlphabet alphabet argument)
        | LambdaAbstract(name, nextTerm) -> setAlphabet (name :: alphabet) nextTerm

    setAlphabet [] outerTerm

/// Выполнить бетта-редукцию.
let bettaReduction replacementName termForSubstitution argument =
    let rec reduce term =
        match term with
        | Variable(nameInLamdaAbstract) ->
            if nameInLamdaAbstract = replacementName then argument
            else Variable(nameInLamdaAbstract)
        | LambdaAbstract(name, nextTerm) -> LambdaAbstract(name, reduce nextTerm)
        | Applique(func, innerArgument) ->
            match func with 
            | Variable(_) -> Applique(reduce func, reduce innerArgument)
            | Applique(_, _) -> 
                let debug = Applique(reduce func, reduce innerArgument)
                debug
            | LambdaAbstract(name, nextTerm) -> 
                let debug = Applique(LambdaAbstract(name, reduce nextTerm), innerArgument)
                debug

    reduce termForSubstitution

/// Сосчитать количество аппликаций.
let countApplique outerTerm =
    let rec count number term =
        match term with
        | Applique(func, _) -> count (number + 1) func
        | Variable(_) -> number
        | LambdaAbstract(_, nextTerm) -> count number nextTerm

    count 0 outerTerm

/// Нормализовать терм.
let normalizeTerm outerTerm =
    let appliqueNumber = countApplique outerTerm

    let rec findAndReduceRedex term =
        match term with
        | Variable(name) -> Variable(name)
        | LambdaAbstract(name, nextTerm) -> LambdaAbstract(name, findAndReduceRedex nextTerm)
        | Applique(func, argument) -> 
            match func with
            | Variable(name) -> Applique(Variable(name), argument)
            | Applique(_, _) -> Applique(findAndReduceRedex func, argument)
            | LambdaAbstract(name, nextTerm) -> bettaReduction name nextTerm argument

    let rec loop step term =
        if step > (appliqueNumber) then term
        else 
            let newTerm = findAndReduceRedex term
            loop (step + 1) newTerm

    loop 0 outerTerm
        

[<EntryPoint>]
let main argv =
    0