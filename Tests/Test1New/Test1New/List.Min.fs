/// Модуль с функцией, находящей наименьший элемент в списке, не используя рекурсию и List.Min
module ListMin

/// Найти наименьший элемент в списке.
let listMin list =
    Seq.fold (fun elem acc -> 
        acc) 0 list
    System.NotImplementedException    

[<EntryPoint>]
let main argv =   
    0 