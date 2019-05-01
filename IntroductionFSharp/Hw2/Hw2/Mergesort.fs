/// Модуль с функцией "Сортировка слиянием".
module Mergesort

/// Разделить список на два списка.
let splitOnTwo list =
    let rec loop list left right =
        match list with 
        | first :: (second :: tail) -> loop tail (first :: left) (second :: right)
        | head :: tail -> (head :: left, right)
        | [] -> (left, right)

    loop list [] []    

/// Сортировать и слить два массива.
let sortAndMergeTwoLists first second =
    System.NotImplementedException