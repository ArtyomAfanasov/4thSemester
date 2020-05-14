module PirorityQueue

/// Очередь с приоритетами.
type 'a MyQueue =
    | Empty 
    | MyQueue of 'a * 'a MyQueue    
        
    /// Добавить в очередь.
    member this.Enqueue newEl =
        let rec enqueue queue newEl = 
            match queue with
            | Empty -> MyQueue(newEl, Empty)
            | MyQueue(firstEl, tail) -> MyQueue(firstEl, enqueue tail newEl) 
        enqueue this newEl

    /// Получить значение первого элемента.
    member this.GetFirst () = 
        match this with
        | Empty -> failwith "Queue is empty"
        | MyQueue(firstEl, _) -> firstEl

    /// Проверка очереди на пустот.   
    member this.MyIsEmpty () = 
        match this with
        | Empty -> printfn "Очередь пуста."
        | _ -> printfn "Очередь не пуста."

    /// Забрать из очереди пару (первый элемент, очередь без первого элемента).
    member this.Dequeue () = 
        match this with
        | Empty -> failwith "Queue is empty"
        | MyQueue(firstEl, tail) -> (firstEl, tail)            