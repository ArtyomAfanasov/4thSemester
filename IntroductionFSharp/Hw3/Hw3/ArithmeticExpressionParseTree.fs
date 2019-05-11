/// Модуль для подсчёта значения дерева разбора арифметического выражения, 
/// заданного через вложенные discriminated union-ы.
module ArithmeticExpressionParseTree

/// Дерево разбора арифметического выражения.
type ParseTree =
    | None
    | Value of int
    | Plus of ParseTree * ParseTree
    | Minus of ParseTree * ParseTree    
    | Division of ParseTree * ParseTree
    | Multiply of ParseTree * ParseTree   

type Operation =
    | NoneOp
    | PlusOp
    | MinusOp
    | DivisionOp
    | MultiplyOp

/// Посчитать значение дерева разбора арифметического выражения.
let calculateValue arithmeticTree =
    let matchParseOperation = 
        System.NotImplementedException
    
    // ToDo : выполняет два раза каждый элемент
    let rec loop operationOrValue operation neighbor= 
        match operationOrValue with         
        | Value(a) ->             
            match operation with
            | PlusOp ->
                match neighbor with
                | Value(b) -> a + b
                | Plus(b, c) -> a + (loop b PlusOp c) + (loop b PlusOp c)
                | Minus(b, c) -> a + (loop b MinusOp c) + (loop c MinusOp b)
                | Division(b, c) -> a + (loop b DivisionOp c) + (loop c DivisionOp b)
                | Multiply(b, c) -> a + (loop b MultiplyOp c) + (loop c MultiplyOp b)
            | MinusOp ->
                match neighbor with 
                | Value(b) -> a - b
                | Plus(b, c) -> a - (loop b PlusOp c) + (loop b PlusOp c)
                | Minus(b, c) -> a - (loop b MinusOp c) + (loop c MinusOp b)
                | Division(b, c) -> a - (loop b DivisionOp c) + (loop c DivisionOp b)
                | Multiply(b, c) -> a - (loop b MultiplyOp c) + (loop c MultiplyOp b)
            | DivisionOp ->
                match neighbor with 
                | Value(b) -> 
                    if (b = 0) then 
                        failwith "Произошло деление на ноль"
                    else
                        a / b
                | Plus(b, c) -> a - (loop b PlusOp c) + (loop b PlusOp c)
                | Minus(b, c) -> a - (loop b MinusOp c) + (loop c MinusOp b)
                | Division(b, c) -> a - (loop b DivisionOp c) + (loop c DivisionOp b)
                | Multiply(b, c) -> a - (loop b MultiplyOp c) + (loop c MultiplyOp b)
            | MultiplyOp ->
                match neighbor with 
                | Value(b) -> a * b
                | Plus(b, c) -> a * (loop b PlusOp c) + (loop b PlusOp c)
                | Minus(b, c) -> a * (loop b MinusOp c) + (loop c MinusOp b)
                | Division(b, c) -> a * (loop b DivisionOp c) + (loop c DivisionOp b)
                | Multiply(b, c) -> a * (loop b MultiplyOp c) + (loop c MultiplyOp b)
         //   \/ Нужен ли пробел?
         | Plus(a, b) -> (loop a PlusOp b) + (loop b PlusOp a)         
         | Minus(a, b) -> (loop a MinusOp b) - (loop b MinusOp a)
         | Division(a, b) -> (loop a DivisionOp b) / (loop b DivisionOp a)
         | Multiply(a, b) -> (loop a MultiplyOp b) * (loop b MultiplyOp a)         
                
    loop arithmeticTree NoneOp None