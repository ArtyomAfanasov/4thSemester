// Learn more about F# at http://fsharp.org

open System
open System.IO


[<EntryPoint>]
let main argv =
    let textReader =
        if DateTime.Today.DayOfWeek = DayOfWeek.Monday
        then Console.In
        else File.OpenText("input.txt") :> TextReader

    let makeArray () = Array.create 100 []

    let lists : int list[] = makeArray ()
    let lists2 = Array.create 100 []
    
    //('a * 'b) list -> 'a list

    let a = box 5

    let b = 5 :> obj

    let ia = unbox<int> a

    let ib = unbox<int> b
    0 // return an integer exit code
