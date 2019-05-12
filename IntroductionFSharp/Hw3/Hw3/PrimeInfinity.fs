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
let generatePrimeInfinityOld () =
    Seq.initInfinite (fun index ->
        let n = index + 4
        if (n = 3) then
            5
        else            
            let factorialOfN = factorial n
            // Формула
            let resForDebug = ((factorialOfN % (n + 1)) / n) * (n - 1) + 2
            resForDebug)      


/// Сгенерировать бесконечную последовательность простых чисел.
let generatePrimeInfinityNewOld () =
    let dirtyInfinity = Seq.initInfinite (fun index ->
        let number = index + 2
                
        let rec loop lessThanCurrentNumber =
            match lessThanCurrentNumber with 
            | number -> true
            | _ -> 
                if (number % lessThanCurrentNumber = 0) then false
                else loop (lessThanCurrentNumber + 1)

        let isPrime = loop 2
        
        if isPrime then number
        else 0)

    Seq.choose (fun x ->
        match x with
        | a when a <> 0 -> Some(a)
        | _ -> None) dirtyInfinity

let generatePrimeInfinity () =
    Seq.initInfinite (fun index ->
        let number = index + 2
                
        let rec loop lessThanCurrentNumber =
            match lessThanCurrentNumber with 
            | _ when number = lessThanCurrentNumber -> true
            | _ -> 
                if (number % lessThanCurrentNumber = 0) then false
                else loop (lessThanCurrentNumber + 1)

        let isPrime = loop 2
        
        if isPrime then number
        else 0)