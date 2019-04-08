/// Модуль подсчёта факториала
module Factorial

    /// Считает факториал от 'input'.
    let factorial input =   
        match input with 
        | _ when input > 0 ->
            let rec loop input acc =
                if input = 1 then
                    acc
                else 
                    loop (input - 1) (acc * input)   
            loop input 1

        | 0 -> 1                            
        | _ -> raise (System.ArgumentException("Факториал не определён для отрицательных чисел."))           