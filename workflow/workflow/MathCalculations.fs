/// Модуль с Workflow, выполняющий математические вычисления с заданной (как аргумент Builder-а) точностью.
module MathCalculations

/// Workflow для вычисления арифметического выражения с заданной точностью (переданный аргумент Builder'a).
type CalculationFlow(computationalAccuracy : int) = 
    // Нужно ли обрабатывать исключение библиотеки?
    do 
        if computationalAccuracy < 0 || computationalAccuracy > 15 
        then failwith "Возможное количество знаков после запятой: 0 - 15."
        else ()

    /// Округляет число заданной точности и передаёт вычисление.
    member this.Bind(x : float, f) =         
        let roundedNumber = System.Math.Round(x, computationalAccuracy)
        f roundedNumber
    
    /// Округляет число и возвращает результат.
    member this.Return(x : float) = System.Math.Round(x, computationalAccuracy)

[<EntryPoint>]
let main argv =
    0