/// Модуль с функцией, генерирующей бесконечную последовательность простых чисел.
module PrimeInfinity

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

/// Сгенерировать бесконечную последовательность простых чисел.
let generatePrimeInfinity () =
    Seq.initInfinite (fun index ->
        let n = index + 1
        if (n = 3) then
            5
        else            
            let factorialOfN = factorial n
            // Формула
            let resForDebug = ((factorialOfN % (n + 1)) / n) * (n - 1) + 2
            resForDebug)         