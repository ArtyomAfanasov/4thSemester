/// Модуль с Workflow, выполняющий вычисления с числами, заданными в виде строк. 
module StringCalculations

/// Workflow, выполняющий вычисления с числами, заданными в виде строк. 
type StringCalculationFlow() =
    let error = "В строках должны быть только целые числа."
    
    // Подсказывать компилятору, что нужно string? Или в F# это дурной тон?
    // Без указания string компилятор думает, что это int.
    /// Конвертирует строку в int
    member this.Bind(x : string, f) =
        try                      
            f (x |> int)
        with
            | :? System.FormatException -> error

    /// Конвертирует число в строку и возвращает
    member this.Return(x) = x |> string

    /// Сообщение в случае неудачной конвертации.
    member this.ErrorMessage = error