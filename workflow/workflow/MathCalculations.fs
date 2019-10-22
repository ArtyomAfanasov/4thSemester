/// Модуль с Workflow, выполняющий математические вычисления с заданной (как аргумент Builder-а) точностью.
module MathCalculations

/// Workflow для вычисления арифметического выражения с заданной точностью (переданный аргумент Builder'a).
type CalculationFlow(calculationAccuracy : int) = 
    /// Округляет число заданной точности и передаёт вычисление.
    member this.Bind(x : float, f) =         
        let roundedNumber = System.Math.Round(x, calculationAccuracy)
        f roundedNumber
    
    /// Округляет число и возвращает результат.
    member this.Return(x : float) = System.Math.Round(x, calculationAccuracy)

[<EntryPoint>]
let main argv =
    0