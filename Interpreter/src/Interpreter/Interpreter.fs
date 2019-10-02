// !!! Сделать member в DU и рекурсивно билдить DU в нём ?
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

/// Закрывающие скобки.
let openedBrackets = ['('; '['; '{'; '<'] 

/// Получить аргумент для подстановки в редекс.
let getArgumentForSubstitution expression =
    match expression with 
    | 
    ()

/// Выполнить альфа-конверсию.
let changeByAlphaConversion expression =
    ()

type LambdaExpression =
    | LeftBr
    | RightBr

    | Lambda of LambdaExpression
    | Parameter of string * LambdaExpression
    
    // Следует после "точки" в лямбда-выражении.
    | Argument of string * LambdaExpression
    | EOF

    member this.ReduceByBetaReduction = 
        let rec reduce expression =
            ()
           
        reduce this 

[<EntryPoint>]
let main argv =
    let l = Lambda(Parameter("x", (Argument("x", EOF))))
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