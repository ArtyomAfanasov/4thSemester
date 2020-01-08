/// Пример использования
module Example

open System
open Interpreter
open Normalizer

[<EntryPoint>]
let main argv =

    // display λ chars correctly
    Console.OutputEncoding <- Text.Encoding.Unicode

    let myOwn = 
        "(lx.ly.(x y) b)"
            |> Parse.parse
    myOwn 
    |> normalizeTerm
    |> printfn "%A"

    0