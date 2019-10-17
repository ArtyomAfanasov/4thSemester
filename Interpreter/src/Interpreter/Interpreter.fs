// передавать каждый раз новый Map для alpha-конверсии, которая будет на лету. -- Заметки для меня :)

// ToDo: найти как налету изменять терм, думая, что альфа-конверсия уже готова.
module Interpreter
 
/// Лямбда-терм.
type Term = 
    | Variable of char
    | Application of Term * Term
    | LambdaAbstraction of char * Term

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
    let rec perform term  =
        match term with 
        | Variable(name) -> 
            if List.contains name conflictNames then 
                Variable((char)(name.ToString().ToUpperInvariant()))
            else
                term
        | Application(func, argument) -> 
            Application(perform func , perform argument )
        | LambdaAbstraction(name, nextTerm) ->
            LambdaAbstraction((char)(name.ToString().ToUpperInvariant()), perform nextTerm )
            (*if List.contains name conflictNames then
                LambdaAbstraction((char)(name.ToString().ToUpperInvariant()), perform nextTerm )                                                                                    
            else
                // return term -- это неверно. Нужно удалять символы из алфавита просто.                
                // term
                LambdaAbstraction(name, perform nextTerm )
            *)

    perform outerTerm

(*let prepareToAlphaConversion outerTerm =
    let rec setAlphabet alphabet term =
        match term with
        | Variable(_) -> alphabet
        | Application(func, argument) -> 
            (setAlphabet alphabet func) @ (setAlphabet alphabet argument)
        | LambdaAbstraction(name, nextTerm) -> setAlphabet (name :: alphabet) nextTerm

    setAlphabet [] outerTerm
*)

/// Выполнить подстановку.
let performSubstitution replacementName termForSubstitution argument =
    let rec perform term =
        match term with
        | Variable(nameInLamdaAbstract) ->
            if nameInLamdaAbstract = replacementName then argument
            else term
        | LambdaAbstraction(name, nextTerm) -> 
            // Здесь тоже неверно, т.к. внутри абстракции может быть нужная переменная.
            // UPD: Нет, всё-таки это верно, т.к. мы по одномй переменной заменяем.
            if name = replacementName then term
            else LambdaAbstraction(name, perform nextTerm)
        | Application(func, innerArgument) -> Application(perform func, perform innerArgument)

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
            | LambdaAbstraction(name, nextTerm) -> performSubstitution name nextTerm argument

    let rec loop step term =
        if step > (applicationsNumber) then term
        else 
            let newTerm = findAndReduceRedex term
            loop (step + 1) newTerm

    loop 0 outerTerm        