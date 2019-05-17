/// Модуль с функцией, генерирующей бесконечную последовательность простых чисел.
module PrimeInfinity

/// Сгенерировать бесконечную последовательность простых чисел.
let generatePrimeInfinity () =
    let dirtyInfinity = Seq.initInfinite (fun index ->
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

    Seq.choose (fun x ->
        match x with
        | a when a <> 0 -> Some(a)
        | _ -> None) dirtyInfinity

/// Сгенерировать бесконечную последовательность простых чисел, используя Seq.filter
let generatePrimeInfinityByFilter () =
    let dirtyInfinity = Seq.initInfinite (fun index ->
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

    Seq.filter (fun x -> x <> 0) dirtyInfinity