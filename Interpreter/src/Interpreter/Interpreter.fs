// передавать каждый раз новый Map для alpha-конверсии, которая будет на лету. -- Заметки для меня :)
module Interpreter
 
type Term = 
    | Variable of char
    | Application of Term * Term
    | LambdaAbstraction of char * Term

/// Выполнить альфа-конверсию.
let prepareToAlphaConversion outerTerm =
    let rec setAlphabet alphabet term =
        match term with
        | Variable(_) -> alphabet
        | Application(func, argument) -> 
            (setAlphabet alphabet func) @ (setAlphabet alphabet argument)
        | LambdaAbstraction(name, nextTerm) -> setAlphabet (name :: alphabet) nextTerm

    setAlphabet [] outerTerm

/// Выполнить подстановку.
let performSubstitution replacementName termForSubstitution argument =
    let rec perform term =
        match term with
        | Variable(nameInLamdaAbstract) ->
            if nameInLamdaAbstract = replacementName then argument
            else term
        | LambdaAbstraction(name, nextTerm) -> LambdaAbstraction(name, perform nextTerm)
        | Application(func, innerArgument) -> Application(perform func, perform innerArgument)

    perform termForSubstitution
    
/// Сосчитать количество аппликаций.
let countApplications outerTerm =
    let rec countLeft term sum  =
        match term with
        | Application(func, argumenmt) -> 
             (countLeft func (sum + 1)) 
             |> 
             (countLeft argumenmt)
        | Variable(_) -> sum
        | LambdaAbstraction(_, nextTerm) -> countLeft nextTerm sum 

    countLeft outerTerm 0 

/// Нормализовать терм.
let normalizeTerm outerTerm =
    let applicationsNumber = countApplications outerTerm

    let rec findAndReduceRedex term =
        match term with
        | Variable(name) -> term
        | LambdaAbstraction(name, nextTerm) -> LambdaAbstraction(name, findAndReduceRedex nextTerm)
        | Application(func, argument) -> 
            match func with
            | Variable(_) -> Application(func, findAndReduceRedex argument)
            | Application(_, _) -> Application(findAndReduceRedex func, findAndReduceRedex argument)
            | LambdaAbstraction(name, nextTerm) -> performSubstitution name nextTerm argument

    let rec loop step term =
        if step > (applicationsNumber) then term
        else 
            let newTerm = findAndReduceRedex term
            loop (step + 1) newTerm

    loop 0 outerTerm        