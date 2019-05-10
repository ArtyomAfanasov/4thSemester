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
        //listStore.Exists (fun couple -> subList.Item(0) = hashKey)
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
            //listStore.Item(listStore.Count).Add(hashKey)
                                                                    (*| listStoreLength -> 
                                                                        let newSubList = new List<'a>()
                                                                        newSubList.Add(elem)
                                                                        listStore.Add(newSubList)
                                                                    | _ -> 
                                                                        if (listStore.Item(step).Item = hashKey) then
                                                                            listStore.Item(step).Add(elem)
                                                                        else 
                                                                            loop (step + 1)*)
            
       
        
        //listStore.Item(hashKey)  

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
                    right.Exists (fun x -> x = elem)
                    
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