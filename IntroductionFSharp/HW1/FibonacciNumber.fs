/// Модуль подсчёта чисел Фибоначи за линейное время
module FibonacciNumber

    /// Подсчёт числа Фибоначчи с номером 'input'.
    let fibonacciNumber inputNumber =                  
        let mutable operationOne = (+)
        let mutable operationTwo = (-)
        if inputNumber < 0 then 
            operationOne <- (-)
            operationTwo <- (+)                            

        let rec loop curNumberOfNumber throughOneBeforeCurNumber previouseNumber =            
            match curNumberOfNumber with
            | positive when positive <> 0 ->                                   
                let curNumber = operationOne throughOneBeforeCurNumber previouseNumber                                   
                loop (operationTwo curNumberOfNumber 1) previouseNumber curNumber     
                    
            | 0 when throughOneBeforeCurNumber <> 0 ->  throughOneBeforeCurNumber
            | 0 -> throughOneBeforeCurNumber

        loop inputNumber 0 1                      