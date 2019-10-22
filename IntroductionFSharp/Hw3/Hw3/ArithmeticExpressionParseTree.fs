/// Модуль для подсчёта значения дерева разбора арифметического выражения, 
/// заданного через вложенные discriminated union-ы.
module ArithmeticExpressionParseTree

/// Дерево разбора арифметического выражения.
type ArithmExprTree =   
    | Value of int
    | Plus of ArithmExprTree * ArithmExprTree
    | Minus of ArithmExprTree * ArithmExprTree    
    | Division of ArithmExprTree * ArithmExprTree
    | Multiply of ArithmExprTree * ArithmExprTree   

/// Посчитать значение дерева разбора арифметического выражения.
let calculateValue arithmeticTree =       
    let rec loop operationOrValue = 
        match operationOrValue with   
        | Plus (a, b) -> (loop a) + (loop b)         
        | Minus (a, b) -> (loop a) - (loop b)
        | Division (a, b) -> 
            if (loop b = 0) then
                failwith "Произошло деление на ноль."
            else
                (loop a) / (loop b)
        | Multiply (a, b) -> (loop a) * (loop b)  
        | Value (a) -> a                 
                               
    loop arithmeticTree