/// Модуль с функцией, находящей наименьший элемент в списке, не используя рекурсию и List.Min
module ListMin



let compare elem acc =
    match box elem with
    | :? string -> failwith "Не работает с типом string"
    | :? char -> failwith "Не работает с типом char"
    | :? bool -> failwith "Не работает с типом bool"
    | :? unit -> failwith "Не работает с типом unit"   
    | :? float -> 
        Seq.choose (fun elem acc -> 
            if elem < acc then elem
            else acc
            ) 5.0 list
    | :? int -> 
        match sign (Operators.compare elem acc) with
        | 1 -> acc
        | -1 -> elem
        | _ -> acc

let firstElem list =
    match list with
    | head :: tail -> head
    | [] -> failwith "Список пуст."

/// Найти наименьший элемент в списке.
let listMin list =
    //TODO[]
    let newlist = []
    match box list with     
    | :? string -> failwith "Не работает с типом string"
    | :? char -> failwith "Не работает с типом char"
    | :? bool -> failwith "Не работает с типом bool"
    | :? unit -> failwith "Не работает с типом unit"    
    | :? _ -> 
        (Seq.iter (fun (a,b) -> compare a b ) list) :: newlist       


        (*List.choose (fun (h1 :: h2 :: tail) ->
        match h1, h2, tail with
        | h1, h2, [] -> 
            if h1 < h2 then 
                Some(h1)  
            else Some(h2)         
        | _ -> 
            if h1 < h2 then 
                Some(h1)  
            else Some(h2)
        ) list  
        |> List.rev
        |> firstElem*)



        (*Seq.choose (fun (h1 :: h2 :: tail) ->
            match list with
            | h1 :: h2 :: _ -> 
                if h1 < h2 then 
                    Some(h1)  
                else Some(h2)
            | last :: [] ->
                Some(last)
            ) list  
        |> List.rev*)         
    
[<EntryPoint>]
let main argv =   
    0 