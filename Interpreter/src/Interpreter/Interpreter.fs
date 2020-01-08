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