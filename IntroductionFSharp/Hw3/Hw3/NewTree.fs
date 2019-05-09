/// Модуль с функцией, которая обновляет дерево, используя какую-либо функцию.
module NewTree

type 'a Tree =
    | Tree of 'a * 'a Tree * 'a Tree
    | Dangling of 'a    
    | Empty

let updateTree givenFun tree =
    let rec loop tree =
        match tree with
        | Empty -> Empty
        | Tree(value, left, right) -> 
            Tree(givenFun value, loop left, loop right)  
        | Dangling(value) -> Dangling(givenFun value)        

    loop tree