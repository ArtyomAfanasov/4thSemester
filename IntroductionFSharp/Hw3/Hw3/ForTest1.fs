module Palindrome

/// Обратить список.
let listFlip list =        
    let rec loop listTail accFlipedList =    
        match listTail with                 
        | head :: tail -> loop tail (head :: accFlipedList)            
        | [] -> accFlipedList

    loop list []

/// Создать список из строки.
let createFlipedListFromStr number =
    let str = number.ToString()
    let length = String.length (str)
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
let isPolindrome number =    
    //let preparedStr = str.ToLower()
    let convertedAndFlipedListFromStr = createFlipedListFromStr number         
    let normalListFromStr = listFlip convertedAndFlipedListFromStr

    normalListFromStr = convertedAndFlipedListFromStr

// Найти максимальный палиндром, полученный произведением двух трёхзначных чисел.
let maxPalindrome () =   
    let firstOld = 999
    let secondOld = 999

    let mutable maxPalindrome = 0
    
    let rec loopForFirst first = 
        match first with
        | _ when first = -1 -> Some(maxPalindrome)
        | _ when first = 100 -> None
        | _ -> loopForSecond first secondOld     

    and loopForSecond first second = 
        match (first, second) with
        | a, b when isPolindrome (a * b) ->
            if a * b < maxPalindrome then 
                loopForFirst -1
            else 
                maxPalindrome <- a * b
                loopForSecond first (second - 1)
        | a, b when b = 100 -> loopForFirst (a - 1)
        | _ -> loopForSecond first (second - 1)
    
    loopForFirst firstOld