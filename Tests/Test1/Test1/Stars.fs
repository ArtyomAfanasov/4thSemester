/// Модуль рисования квадрата.
module Stars

    /// Нарисовать квадрат.
    let squreString n =       
        if n <= 0 then None
        else
            /// Рисует строку квадрата.
            let checkInnerAndDrowLine acc step isInner = 
                if isInner then
                    let rec drowInner acc step =               
                        match step with
                        | _ when step = 0 || step = n - 1 -> drowInner (acc + "*") (step + 1)
                        | _ when step < n - 1 -> drowInner (acc + " ") (step + 1)
                        | _ -> acc

                    drowInner acc step
                else
                    let rec drowNotInner acc step =               
                        match step with                                
                        | _ when step <= n - 1 -> drowNotInner (acc + "*") (step + 1)
                        | _ -> acc

                    drowNotInner acc step
                           
            let rec loop step accStr =
                match step with
                | _ when step = 0 -> 
                    loop (step + 1) ((checkInnerAndDrowLine accStr 0 false) + "\n")
                | _ when step <= n - 2 -> 
                    loop (step + 1) ((checkInnerAndDrowLine accStr 0 true) + "\n")
                | _ when step = n - 1 -> 
                    loop (step + 1) ((checkInnerAndDrowLine accStr 0 false) + "\n")
                | _ -> Some(accStr)
                
            loop 0 ""