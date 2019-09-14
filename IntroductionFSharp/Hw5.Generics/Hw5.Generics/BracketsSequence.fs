/// Модуль, содержащий функцию, которая по произвольной строке 
/// проверяет корректность скобочной последовательности в этой строке. 
/// Скобки бывают трёх видов.
module BracketSequence 

open System.Collections.Generic

/// Закрывающие скобки.
let closedBrackets = Map.ofList [ (')', '('); ('}', '{'); (']', '['); ('>', '<') ]  

/// Закрывающие скобки.
let openedBrackets = ['('; '['; '{'; '<']    

/// Проверить корректность скобочной последовательности.
let checkBrackets (input : string) = 
    let stack = Stack<char>()
    
    String.iter (fun elem -> 
        if closedBrackets.ContainsKey elem || List.contains elem openedBrackets then        
            if stack.Count = 0 then stack.Push elem            
            elif List.contains elem openedBrackets then stack.Push(elem)            
            elif closedBrackets.Item elem = stack.Peek() then
                stack.Pop() |> ignore
            else stack.Push elem
        else ()) input

    stack.Count = 0