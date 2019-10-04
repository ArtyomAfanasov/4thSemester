﻿// !!! Сделать member в DU и рекурсивно билдить DU в нём ?
// Юзать (код ниже) + рекурсия при создании DU.
// Через DU искать beta-reduction. Потом ToString(). Потом рекурсивный билд нового.

// НЕ НАДО рефлексию. ЧИСТО РЕКУРСИВНО БИЛДИТЬ НУЖНО DU.

(*
open Microsoft.FSharp.Reflection

let toString (x:'a) = 
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name

let fromString<'a> (s:string) =
    match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
    |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
    |_ -> None

type A = X|Y|Z with
    override this.ToString() = toString this
    static member fromString s = fromString<A> s
*)

module Interpreter

// Через скобки узнавать где нужно заменять.
/// Выполнить альфа-конверсию.
let changeByAlphaConversion expression =
    ()

type Term =
    | OpenedBr of Term
    | ClosedBr of Term

    | Lambda of Term
    | Parameter of string * Term
    | Dot of Term
    
    // Следует после "точки" в лямбда-выражении.
    | Argument of string * Term
    | EOF

    | Zaglishka
    
    member this.ReduceByBetaReduction = 
        let rec reduce expression =
            ()
           
        reduce this

(*let getNextTerm term = 
    match term with 
    | OpenedBr(desiredTerm) -> "", desiredTerm
    | ClosedBr(desiredTerm) -> "", desiredTerm
    | Lambda(desiredTerm) -> "", desiredTerm
    | Parameter(name, desiredTerm) -> name, desiredTerm
    | Dot(desiredTerm) -> "", desiredTerm
    | Argument(name, desiredTerm) -> name, desiredTerm
    | EOF -> "", EOF*)

/// Цикл поиска первой лямбды.
let rec loopLambda term = 
    match term with 
    | Lambda(_) -> term
    
    | OpenedBr(nextTerm) | ClosedBr(nextTerm) | Parameter(_, nextTerm) 
    | Dot(nextTerm) | Argument(_, nextTerm) ->  loopLambda nextTerm
           
    | EOF -> EOF
    | _ -> failwith "Should never get here."

/// Цикл поиска аргумента для подстановки.
let rec loopArgument numberNotClosedBr term =
    match term with
    | OpenedBr(nextTerm) -> loopArgument (numberNotClosedBr + 1) nextTerm        
    | ClosedBr(nextTerm) -> 
        let curNumberNotClosedBr = numberNotClosedBr - 1
        if curNumberNotClosedBr = 0 then
            // ToDo. взять аргумент и обработать хвост 
            Zaglishka
        else
            loopArgument curNumberNotClosedBr nextTerm                                
    
    | Lambda(nextTerm) | Dot(nextTerm) | Parameter(_, nextTerm) | Argument(_, nextTerm) -> 
        loopArgument numberNotClosedBr nextTerm
 
    | EOF -> EOF
    | _ -> failwith "Should never get here."

/// Найти терм для подстановки.
let findArgumentForSubstitution baseTerm =
    let firstLambdaAndRest = loopLambda baseTerm
    if firstLambdaAndRest = EOF then baseTerm    
    else 
        let argumentAndRest = loopArgument 1 firstLambdaAndRest
        if argumentAndRest = EOF then baseTerm
        else argumentAndRest

/// Получить аргумент для подстановки в редекс и остаточное выражение.
let getArgumentAndRest baseTerm =
     let argumentAndRest = findArgumentForSubstitution baseTerm    
     () // todo
 
[<EntryPoint>]
let main argv =
    let l = Lambda(Parameter("x", (Argument("x", Argument("y", EOF)))))
    let rec loop expr substit =             
        match expr with
        | Lambda a -> Lambda(loop a substit)
        | Parameter (s, a) -> substit

    let a(b) = l
    printfn "%A" (a(b))
    printfn "%A" <| loop l (Argument("x", Argument("y", EOF)))
    0

(*
type ShaderProgram = | ShaderProgram of id:int

type IPrintable =
    abstract Print: unit -> unit

type Shape =
    | Circle of float
    | EquilateralTriangle of float
    | Square of float
    | Rectangle of float * float

    member this.Area =
        match this with
        | Circle r -> 2.0 * 3. * r
        | EquilateralTriangle s -> s * s * sqrt 3.0 / 4.0
        | Square s -> s * s
        | Rectangle(l, w) -> l * w

    interface IPrintable with
        member this.Print () =
            match this with
            | Circle r -> printfn "Circle with radius %f" r
            | EquilateralTriangle s -> printfn "Equilateral Triangle of side %f" s
            | Square s -> printfn "Square with side %f" s
            | Rectangle(l, w) -> printfn "Rectangle with length %f and width %f" l w

type C = Circle of int | Rectangle of int * int

type Contact = Email of string | Phone of int



let email = Email "bob@example.com"
printfn "%A" email    // nice
printfn "%O" email    // ugly!

[1..10]
|> List.map Circle
|> printfn "%A"

[1..10]
|> List.zip [21..30]
|> List.map Rectangle
|> printfn "%A"
*)