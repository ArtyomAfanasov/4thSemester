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

            //match func with 
            //| Variable(_) -> Application(perform func, perform innerArgument)
            //| Application(_, _) ->
            //    let debug = Application(perform func, perform innerArgument)
            //    debug
            //| LambdaAbstraction(name, nextTerm) -> 
            //    let debug = Application(LambdaAbstraction(name, perform nextTerm), innerArgument)
            //    debug
    
// TODO Нормальный подсчёт всех аппликаций, либо другой вариант цикла.
/// Сосчитать количество аппликаций.
let countApplique outerTerm =
    let rec countLeft listForCounting term =
        match term with
        | Application(func, _) -> 
            countLeft (listForCounting + 1) func
        | Variable(_) -> listForCounting
        | LambdaAbstraction(_, nextTerm) -> countLeft listForCounting nextTerm

    let rec countRight listForCounting term =
        match term with
        | Application(_, argument) -> 
            countRight (listForCounting + 1) argument
        | Variable(_) -> listForCounting
        | LambdaAbstraction(_, nextTerm) -> countRight listForCounting nextTerm

    let numberOfApplications = countLeft 0 outerTerm
    countRight numberOfApplications outerTerm

/// Нормализовать терм.
let normalizeTerm outerTerm =
    let appliqueNumber = countApplique outerTerm

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
        if step > (appliqueNumber) then term
        else 
            let newTerm = findAndReduceRedex term
            loop (step + 1) newTerm

    loop 0 outerTerm        