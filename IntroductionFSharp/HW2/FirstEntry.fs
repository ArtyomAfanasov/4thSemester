module FirstEntry

    let firstEntry list wishfulNumber =
        let rec loop tail =
            match tail with
            | head :: tail -> 
                if head = wishfulNumber 
                    then List.length list - (List.length tail + 1)
                else 
                    loop tail

            // Заданного числа в массиве нет.
            | _ -> -1 
        
        loop list