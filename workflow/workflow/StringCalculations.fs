/// Модуль с Workflow, выполняющий вычисления с числами, заданными в виде строк. 
module StringCalculations

/// Workflow, выполняющий вычисления с числами, заданными в виде строк. 
type StringCalculationFlow() =
    // Подсказывать компилятору, что нужно string? Или в F# это дурной тон?
    // Без указания string компилятор думает, что это int.
    // Кажется, не подсказывать тут нельзя.
    /// Конвертирует строку в int
    member this.Bind(x : string, f) =
        try                      
            f (x |> int)
        with
            | :? System.FormatException -> None

    /// Конвертирует число в строку и возвращает
    member this.Return(x) = Some(x)