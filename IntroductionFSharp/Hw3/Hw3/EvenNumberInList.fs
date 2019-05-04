/// Модуль подсчёта чётных чисел в списке.
module EvenNumberInList

let countEvenNumberBySeqFilter list =
    Seq.filter (fun x -> x % 2 = 0) list 
    |> Seq.length   

let countEvenNumberBySeqFold list =
    let countAllNumbers = List.length list
    Seq.fold (fun acc elem -> acc - (elem % 2) ) countAllNumbers list

let countEvenNumberBySeqMap list =
    Seq.map (fun x -> x % 2) list 
    |> Seq.filter (fun x -> x = 0) 
    |> Seq.length

[<EntryPoint>]
let main argv =     
    0 