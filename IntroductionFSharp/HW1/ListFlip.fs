/// Модуль с функцией обращения списка.
module ListFlip

    /// Обратить список.
    let listFlip list =        
        let rec loop listTail flipedList =    
            match listTail with                 
            | head :: tail -> loop tail (head :: flipedList)            
            | [] -> flipedList

        loop list []