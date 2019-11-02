/// Модуль подсчёта чётных чисел в списке.
module EvenNumberInList

/// Посчитать количество чётных чисел в списке. Первый вариант.
let countEvenNumberBySeqFilter list =
    Seq.filter (fun x -> x % 2 = 0) list 
    |> Seq.length   
 
/// Посчитать количество чётных чисел в списке. Второй вариант.
let countEvenNumberBySeqFold list =
    let countAllNumbers = List.length list
    Seq.fold (fun acc elem ->                
        acc - (abs elem % 2)) countAllNumbers list

/// Посчитать количество чётных чисел в списке. Третий вариант.
let countEvenNumberBySeqMap list =
    Seq.map (fun x -> x % 2) list
    |> Seq.filter (fun x -> x = 0)
    |> Seq.length

[<EntryPoint>]
let main argv =     
    0 