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
    List.fold (fun stack elem -> 
        if closedBrackets.ContainsKey elem || List.contains elem openedBrackets then        
            if List.length stack = 0 then (elem :: stack)
            elif List.contains elem openedBrackets then (elem :: stack)           
            elif (match stack with
                    | h :: t -> h = closedBrackets.Item elem
                    | _ -> failwith "Should never get here") then
                        match stack with
                        | h :: t -> t
                        | _ -> failwith "Should never get here"
            else (elem :: stack)
        else stack) [] (Seq.toList input)
    |> List.length 
    |> (=) 0

    // F# красив :)