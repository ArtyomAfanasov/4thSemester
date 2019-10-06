// передавать каждый раз новый Map для alpha-конверсии, которая будет на лету.

// Аппликация -- вот что главное в исчислении будет.

module NewInterpreter
 
/// Множество переменных. При альфа-конверсии ToUpper.
let alphabet = ['a'..'z']

type Term = 
    | Variable of char
    | Applique of Term * Term
    | LambdaAbstract of char * Term

/// Выполнить альфа-конверсию.
let alphaConversion term =
   
    ()

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
            | Applique(_, _) -> Applique(reduce func, reduce innerArgument)

    reduce termForSubstitution

let countApplique outerTerm =
    let rec count number term =
        match term with
        | Applique(func, _) -> count (number + 1) func
        | Variable(_) -> number
        | LambdaAbstract(_, nextTerm) -> count number nextTerm

    count 0 outerTerm

/// Выполнить бетта-редукцию.
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
        if step > appliqueNumber then term
        else 
            let newTerm = findAndReduceRedex term
            loop (step + 1) newTerm

    loop 0 outerTerm
        

[<EntryPoint>]
let main argv =
    0