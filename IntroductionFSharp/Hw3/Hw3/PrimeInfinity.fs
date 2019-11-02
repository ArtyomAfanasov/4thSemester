/// Модуль с функцией, генерирующей бесконечную последовательность простых чисел.
module PrimeInfinity

let isPrime number = 
    let rec loop lessThanCurrentNumber =
        (lessThanCurrentNumber > ((sqrt (number |> double)) |> int)) ||
        (number % lessThanCurrentNumber <> 0 && loop (lessThanCurrentNumber + 1))

        
    
    loop 2

/// Сгенерировать бесконечную последовательность простых чисел.
let generatePrimeInfinity () =
    let dirtyInfinity = Seq.initInfinite (fun index ->
        let number = index + 2                   
        
        if isPrime number then number
        else 0)

    Seq.choose (fun x ->
        match x with
        | a when a <> 0 -> Some(a)
        | _ -> None) dirtyInfinity

/// Сгенерировать бесконечную последовательность простых чисел, используя Seq.filter
let generatePrimeInfinityByFilter () =
    let dirtyInfinity = Seq.initInfinite (fun index ->
        let number = index + 2

        if isPrime number then number
        else 0)

    Seq.filter (fun x -> x <> 0) dirtyInfinity