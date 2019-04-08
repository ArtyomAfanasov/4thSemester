/// Модуль с функцией обращения списка.
module ListFlip

    /// Обратить список.
    let listFlip list =        
        let rec loop listTail accFlipedList =    
            match listTail with                 
            | head :: tail -> loop tail (head :: accFlipedList)            
            | [] -> accFlipedList

        loop list []