/// Модуль, содержащий функцию, которая по произвольной строке 
/// проверяет корректность скобочной последовательности в этой строке. 
/// Скобки бывают трёх видов.
module BracketSequence 

/// Проверить список с закрывающими и открывающими значениями на корректность
/// их расположения.
let checkBracketList list openF closeF openS closeS openT closeT  =
    let rec loop list =        
        match list with   
        | [] -> true
        | head :: tail -> 
            if  head = closeF && List.last tail = openF ||
                head = closeS && List.last tail = openS ||
                head = closeT && List.last tail = openT then
                    loop (List.rev (List.rev tail).Tail)                
            else false

    loop list

/// Проверить корректность последовательности выбранных символов.
/// Первый элемент пары - это открывающий символ. Второй - закрывающий.
///
/// Если какой-то из вариантов не нужен, 
/// то задайте для него пару значений char по умолчанию ('\u0000', '\u0000').
let checkStringOnOpenAndCloseValues (input : string) (firstValue : char * char) 
    (secondValue : char * char) (thirdValue : char * char) =
    let openF, closeF = firstValue
    let openS, closeS = secondValue
    let openT, closeT = thirdValue  
    
    // Из (()) получу ))((.
    let listOfOpenAndCloseValues =
        Seq.fold (fun bracketList elem ->
            match elem with 
            | a when 
                a = openF || a = closeF || 
                a = openS || a = closeS || 
                a = openT || a = closeT -> elem :: bracketList
            | _ -> bracketList            
           ) [] input
    
    checkBracketList listOfOpenAndCloseValues openF closeF openS closeS openT closeT

/// Проверить корректность скобочной последовательности в данной строке. 
let checkBrackets input =
    checkStringOnOpenAndCloseValues input ('(', ')') ('[', ']') ('{', '}')    