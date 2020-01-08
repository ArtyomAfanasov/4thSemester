/// Пример использования
module Example

open System
open Interpreter
open Normalizer
open System.IO

[<EntryPoint>]
let main argv =

    // display λ chars correctly
    Console.OutputEncoding <- Text.Encoding.Unicode

    let readLines = seq {
        use sr = new StreamReader ("../../../../../tests/Input/input.txt")
        while not sr.EndOfStream do
            yield sr.ReadLine ()
    }

    Seq.iter (fun term ->
        printfn "Не нормализованный терм: %s" term
        term |> Parse.parse |> normalizeTerm |> 
        printfn "Нормализованный терм: %A\n"
        ) readLines

    0