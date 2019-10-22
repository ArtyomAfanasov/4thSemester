/// Модуль с функцией, которая обновляет дерево, используя какую-либо функцию.
module NewTree

/// Бинарное дерево.
type 'a Tree =
    | Subtree of 'a * 'a Tree * 'a Tree
    | Empty

/// Обновить бинарное дерево, применив заданную функцию к его узлам.
let updateTree givenFun tree =
    let rec loop tree =
        match tree with
        | Empty -> Empty
        | Subtree(value, Empty, Empty) -> Subtree(givenFun value, Empty, Empty)  
        | Subtree(value, left, right) -> 
            Subtree(givenFun value, loop left, loop right)                

    loop tree