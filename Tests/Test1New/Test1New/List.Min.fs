/// Модуль с функцией, находящей наименьший элемент в списке, не используя рекурсию и List.Min
module ListMin

/// Найти наименьший элемент в списке.
let listMin list = 
    Seq.fold (fun elem acc -> 
        if elem < acc then
            elem
        else acc) 215336146 list    

[<EntryPoint>]
let main argv =   
    0 