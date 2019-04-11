module Palindrome

/// Обратить список.
let listFlip list =        
    let rec loop listTail accFlipedList =    
        match listTail with                 
        | head :: tail -> loop tail (head :: accFlipedList)            
        | [] -> accFlipedList

    loop list []

/// Создать список из строки.
let createFlipedListFromStr (str : string) =
    let length = String.length str
    let rec listCreationLoop accList step = 
        match str.[step] with 
        | _ when step = length - 1 ->                 
            str.[step] :: accList
        | ' ' ->                
            listCreationLoop accList (step + 1)                               
        | _  ->                                
            listCreationLoop (str.[step] :: accList) (step + 1)

    listCreationLoop [] 0                
        
/// Проверить: является ли строка палиндромом.
let isPolindrome (str : string) =    
    let preparedStr = str.ToLower()
    let convertedAndFlipedListFromStr = createFlipedListFromStr preparedStr         
    let normalListFromStr = listFlip convertedAndFlipedListFromStr

    if normalListFromStr = convertedAndFlipedListFromStr then
        true
    else 
        false