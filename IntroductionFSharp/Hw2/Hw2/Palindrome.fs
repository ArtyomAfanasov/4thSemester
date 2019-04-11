module Palindrome

/// Обратить список.
let listFlip list =        
    let rec loop listTail accFlipedList =    
        match listTail with                 
        | head :: tail -> loop tail (head :: accFlipedList)            
        | [] -> accFlipedList

    loop list []           
        
/// Проверить: является ли строка палиндромом.
let isPalindrome (str : string) =    
    let preparedStr = (str.ToLower()).Replace(" ", "")

    let normalListFromStr = List.ofArray <| preparedStr.ToCharArray()
    let flipedListFromStr = listFlip normalListFromStr      

    normalListFromStr = flipedListFromStr        