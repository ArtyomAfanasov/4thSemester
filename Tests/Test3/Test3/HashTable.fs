module Test3

open System;
open System.Collections.Generic;

type HashTable (hashFunc : int -> int) =
    
    /// Объект для хранения хэш таблицы.
    let listStore = new List<int * List<'a>>()    
        
    let convertFuncion hashFunc =
        ()    
   
    let isKeyExist elem =
        let hashKey = hashFunc elem
        
        listStore.Exists (fun couple -> 
            let left, right = couple
            left = hashKey)

    let addElem elem =
        let hashKey = hashFunc elem
        
        if (isKeyExist elem) then   
            let rec loop step =
                match listStore.Item(step) with
                | left, right when left = hashKey ->                    
                    right.Add(elem)  
                | _ -> loop (step + 1)
                
            loop 0
        else             
            let newSubList = new List<'a>()
            newSubList.Add(elem)
            listStore.Add(hashKey, newSubList)
            
    let isExist elem =       
        let hashKey = hashFunc elem
        
        if (isKeyExist elem) then   
            let rec loop step =
                match listStore.Item(step) with
                | left, right when left = hashKey ->                    
                    right.Exists (fun x -> x = elem)
                    
                | _ -> loop (step + 1)
                
            loop 0
        else             
            false      

    let delete elem =
        let hashKey = hashFunc elem
        
        if (isKeyExist elem) then   
            let rec loop step =
                match listStore.Item(step) with
                | left, right when left = hashKey ->                    
                    let rec innerLoop innerStep =
                        match right.Item(innerStep) with
                        | a when a = elem -> right.Remove(innerStep)
                        | _ -> innerLoop (innerStep + 1)

                    innerLoop 0                    
                | _ -> loop (step + 1)
                
            loop 0
        else             
            failwith "Удаляемого элемента не существует."  
    
    member this.AddElem elem = addElem elem
    member this.IsExist elem = isExist elem
    member this.Delete elem = delete elem
    
[<EntryPoint>]
let main argv =   
    0