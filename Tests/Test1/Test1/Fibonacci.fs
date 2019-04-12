/// Модуль с функцией, считающей сумму всех чисел Фибоначчи, не превосходящих 1000000.
module FibonacciNumber

/// Сумма чисел Фибоначчи, не превосходящих 1000000.
let SumOfOddFibonacciNumberLessThen1000000 () =    
    let supremum = 1000000
    let rec loop beforeLastNumber previousNumber acc =            
        match (beforeLastNumber, previousNumber) with
        | _ when beforeLastNumber + previousNumber > supremum -> acc             
        | _ when (beforeLastNumber + previousNumber) % 2 = 0 -> 
            let curNumber = beforeLastNumber + previousNumber                                   
            loop previousNumber curNumber (acc + curNumber)    
        | _ -> 
            let curNumber = beforeLastNumber + previousNumber                                   
            loop previousNumber curNumber acc        
        
    loop 0 1 0 

/// Тест. Сумма чётных чисел Фибоначчи, не превосходящих `sup`.
let SumOfOddFibonacciNumberLessThenSupremumTest supremum =      
    let rec loop beforeLastNumber previousNumber acc =            
        match (beforeLastNumber, previousNumber) with
        | _ when beforeLastNumber + previousNumber > supremum -> acc             
        | _ when (beforeLastNumber + previousNumber) % 2 = 0 -> 
            let curNumber = beforeLastNumber + previousNumber                                   
            loop previousNumber curNumber (acc + curNumber)    
        | _ -> 
            let curNumber = beforeLastNumber + previousNumber                                   
            loop previousNumber curNumber acc    
        
    loop 0 1 0 

/// Тест. Сумма всех чисел Фибоначчи, не превосходящих `sup`.
let SumOfAllFibonacciNumberLessThenSupremumTest supremum=      
    let rec loop beforeLastNumber previousNumber acc =            
        match (beforeLastNumber, previousNumber) with
        | _ when beforeLastNumber + previousNumber > supremum -> acc             
        | _  -> 
            let curNumber = beforeLastNumber + previousNumber                                   
            loop previousNumber curNumber (acc + curNumber)            
        
    loop 0 1 1 

/// Тест. Последнее число фибоначи, меньшее 1000000.
let lastFibonacciNumberLessThan1000000 () = 
    let supremum = 1000000
    let rec loop beforeLastNumber previousNumber =            
        match (beforeLastNumber, previousNumber) with
        | _ when beforeLastNumber + previousNumber > supremum -> previousNumber             
        | _ -> 
            let curNumber = beforeLastNumber + previousNumber                                   
            loop previousNumber curNumber       
        
    loop 0 1
    
[<EntryPoint>]
let main argv =    
    printfn "%s \n" (Stars.squreString 4).Value
    printfn "%s \n" (Stars.squreString 3).Value
    printfn "%s \n" (Stars.squreString 2).Value
    printfn "%s \n" (Stars.squreString 1).Value
    0 