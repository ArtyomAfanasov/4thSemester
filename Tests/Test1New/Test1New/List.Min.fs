/// Модуль с функцией, находящей наименьший элемент в списке, не используя рекурсию и List.Min
module ListMin

let listMin list =     
    Seq.fold (fun acc elem  -> 
        if elem < acc then
            elem
        else acc) (List.head list) list    

[<EntryPoint>]
let main argv =   
    0 