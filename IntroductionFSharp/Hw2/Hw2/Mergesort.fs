/// Модуль с функцией "Сортировка слиянием".
module Mergesort

/// Разделить список на два списка.
let splitOnTwo list =
    let rec loop list left right =
        match list with 
        | first :: (second :: tail) -> loop tail (first :: left) (second :: right)
        | head :: tail -> head :: left, right
        | [] -> left, right

    loop list [] []    

/// Сортировать и слить два массива.
let sortAndMergeTwoLists first second =
    let rec loop first second sortedList =
        match first, second with 
        | firstHead :: firstTail, secondHead :: secondTail ->
            if firstHead <= secondHead then 
                loop firstTail second (firstHead :: sortedList)
            else 
                loop first secondTail (secondHead :: sortedList)
        | [], head :: tail -> loop [] tail (head :: sortedList)
        | head :: tail, [] -> loop tail [] (head :: sortedList)
        | [], [] -> List.rev sortedList
            
    loop first second []   