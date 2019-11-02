/// Модуль с Workflow, выполняющий вычисления с числами, заданными в виде строк. 
module StringCalculations

open System

/// Workflow, выполняющий вычисления с числами, заданными в виде строк. 
type StringCalculationFlow() =
    /// Число, полученное из строки.
    let mutable result = 0        
    
    // Подсказывать компилятору, что нужно string? Или в F# это дурной тон?
    // Без указания string компилятор думает, что это int.
    // Кажется, не подсказывать тут нельзя.
    /// Конвертирует строку в int
    member this.Bind(x : string, f) =
        match (Int32.TryParse(x, &result)) with
        | true -> f result
        | false -> None

    /// Конвертирует число в строку и возвращает
    member this.Return(x) = Some(x)