/// Модуль, содержащий функцию, которая по произвольной строке 
/// проверяет корректность скобочной последовательности в этой строке. 
/// Скобки бывают трёх видов.
module BracketSequence 

open System.Collections.Generic

/// Закрывающие скобки.
let closedBrackets = 
    let innerDict = new Dictionary<char, char>()
    innerDict.Add(')', '(')
    innerDict.Add('}', '{')
    innerDict.Add(']', '[')
    innerDict.Add('>', '<')
    innerDict

/// Закрывающие скобки.
let openedBrackets = 
    let innerList = new List<char>()
    innerList.Add('(')
    innerList.Add('[')
    innerList.Add('{')
    innerList.Add('<')
    innerList

/// Проверить корректность скобочной последовательности.
let checkBrackets (input : string) = 
    let stack = Stack<char>()
    
    String.iter (fun elem -> 
        if closedBrackets.ContainsKey elem || openedBrackets.Contains elem then        
            if stack.Count = 0 then stack.Push elem            
            elif openedBrackets.Contains elem then stack.Push(elem)            
            elif closedBrackets.Item elem = stack.Peek() then
                stack.Pop() |> ignore
            else stack.Push elem
        else ()) input

    stack.Count = 0