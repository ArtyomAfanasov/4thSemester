/// Модуль для подсчёта значения дерева разбора арифметического выражения, 
/// заданного через вложенные discriminated union-ы.
module ArithmeticExpressionParseTree

/// Дерево разбора арифметического выражения.
/// Замечание: Pow(times, value).
type ParseTree =
    | Value of int
    | Plus of ParseTree * ParseTree
    | Minus of ParseTree * ParseTree    
    | Division of ParseTree * ParseTree
    | Multiply of ParseTree * ParseTree   

/// Посчитать значение дерева разбора арифметического выражения.
let calculateValue arithmeticTree =
    System.NotImplementedException