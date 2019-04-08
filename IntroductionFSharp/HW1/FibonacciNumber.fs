/// Модуль подсчёта чисел Фибоначи за линейное время
module FibonacciNumber

    /// Подсчёт числа Фибоначчи с номером 'input'.
    let fibonacciNumber inputNumber =                          
        let rec loop curNumberOfNumber beforeLastNumber previousNumber =            
            match curNumberOfNumber with
            | _ when curNumberOfNumber > 0 ->                                   
                let curNumber = beforeLastNumber + previousNumber                                   
                loop (curNumberOfNumber - 1) previousNumber curNumber     
                                
            | 0 -> beforeLastNumber
            | _ -> raise (System.AggregateException("Для отрицательных номеров не определено."))

        loop inputNumber 0 1                      