module FirstEntry

/// Вернуть номер первого вхождения элемента в список.
let firstEntry list wishfulElement =
    let rec loop tail =
        match tail with
        | head :: tail -> 
            if head = wishfulElement    
                then List.length list - (List.length tail + 1)
            else 
                loop tail

        // Заданного числа в массиве нет.
        | _ -> raise (System.Exception("Wishful number don't consist in list"))            
        
    loop list

[<EntryPoint>]
let main argv =    
    0 