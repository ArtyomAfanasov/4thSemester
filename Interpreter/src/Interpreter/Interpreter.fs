/// Объекты интерпретатора лямбда-выражений.
module Interpreter

open System
open Normalizer

module Parse =

    open FParsec

    let private parseExpr, private parseExprRef =
        createParserForwardedToRef<Term, unit>()

    let private parseName =
        many1Chars (satisfy (fun c ->
            Char.IsLetterOrDigit(c) && (c <> 'λ') && (c <> 'l')))

    let private parseVariable =
        parseName
            |>> Variable

    let private parseApplication =
        pipe5
            (skipChar '(')
            parseExpr
            (many1 <| skipChar ' ')
            parseExpr
            (skipChar ')')
            (fun _ func _ arg _ ->
                Application (func, arg))

    let private parseLambda =
        pipe4
            (skipAnyOf ['λ'; '^'; '\\'; 'l'])
            parseName
            (skipChar '.')
            parseExpr
            (fun _ param _ body ->
                LambdaAbstraction (param, body))

    do parseExprRef :=
        choice [
            parseVariable
            parseApplication
            parseLambda
        ]

    let parse str =
        let parser = !parseExprRef .>> eof   // force consumption of entire string
        match run parser str with
            | Success (expr, _, _) -> expr
            | Failure (msg, _, _) -> failwith msg



///// Переменная
//let (!!) value = Variable(value)
//
///// Лямбда-абстракция
//let (!+) name term = LambdaAbstraction(name, term)
//
///// Аппликация
//let (!*) term1 term2 = Application(term1, term2)
//
//// `(\l x. x) b`
//// let term = !* (!+ 'x' (!! 'x')) (!! 'b')

/////let interpret (input : string) =
/////    if input.Length = 0 then ()
/////    else
/////        let lastCharIndex = input.Length - 1
/////
/////        let rec parse applicationAmount charIndex : Term =
/////        
/////            // Последней может быть только Variable
/////            if charIndex = lastCharIndex then 
/////                printfn "Debug. %A" <| input.Chars lastCharIndex
/////                Variable(input.Chars lastCharIndex)
/////            else
/////                let curChar = input.Chars charIndex
/////                match curChar with
/////                | ',' -> Variable(curChar) // загулшка
/////                // заглушка | 'A' -> Application(parse (applicationAmount + 1) (charIndex + 1), parse (applicationAmount + 1) (charIndex + 1))
/////                | 'L' -> Variable(curChar) // заглушка
/////                | 'V' -> Variable(curChar)
/////                | _ -> failwith "Should not get to this."
/////
/////        printfn "%A" <| parse 0 0 
    //String.iteri (fun index char ->
    //    printfn "Debug. %A" char
    //    
    //    
    //
    //    if index = lastChar then 
    //        ()
    //    else    
    //        printfn "%A" <| input.Chars (index + 1))
    //        
    //    input