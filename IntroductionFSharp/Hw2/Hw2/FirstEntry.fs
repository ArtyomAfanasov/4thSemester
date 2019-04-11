module FirstEntry

/// Вернуть номер первого вхождения элемента в список. None, если числа нет.
let firstEntry list wishfulElement =
    let rec loop tail =
        match tail with
        | head :: tail -> 
            if head = wishfulElement    
                then Some(List.length list - (List.length tail + 1))
            else 
                loop tail

        // Заданного числа в массиве нет.
        | _ -> None          
        
    loop list

[<EntryPoint>]
let main argv =    
    0 