/// Модуль создание списка с заданным условием.
module ListCreation
    
    /// Создать список следующего вида [2^n; 2^(n + 1); ... ; 2^(n + m)].
    let createList n m =                     
        let rec listCreationLoop prevElement accList step = 
            match step with 
            | temp when temp <= m ->
                let powedElement = prevElement * 2
                listCreationLoop powedElement (powedElement::accList) (step + 1)

            | _ -> ListFlip.listFlip accList

        if n >= 0 && m >= 0 then
            let firstElement = pown 2 n
            
            listCreationLoop firstElement (firstElement::[]) 1
        else
            raise (System.ArgumentException("Не определено для отрицательных чисел."))