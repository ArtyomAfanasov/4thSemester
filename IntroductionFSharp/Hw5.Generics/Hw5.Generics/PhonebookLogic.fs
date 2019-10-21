module PhonebookLogic

open PhonebookErrors
open System.IO

/// Добавить запись (имя, телефон).
let addRecord name phone (phonebookHashTable : Map<string, string>) = 
    phonebookHashTable.Add(name, phone)    

/// Найти номер по имени.
let findPhoneByName name (phonebookHashTable : Map<string, string>) = 
    if not <| phonebookHashTable.ContainsKey(name) then keyNotFoundError
    else phonebookHashTable.Item(name)     

/// Найти имя по телефону.
let findNameByPhone phone (phonebookHashTable : Map<string, string>) =
    match Map.tryFindKey (fun _ phoneElement ->  phoneElement = phone) phonebookHashTable with
    | Some(key) -> key
    | None -> keyNotFoundError

/// Вывести все пары (имя, телефон) из базы.
let printAll phonebookHashTable =
    Map.iter (fun name phone -> printfn "Владалец телефона %s --- это %s" phone name) phonebookHashTable

/// Нормализовать введённый номер.
let normalizePhone (phone : string) = 
    try 
        let normalizedCharSeq =
            Seq.filter (fun letter -> 
                match letter with
                | '+' | '(' | ')' | '-' -> false
                | '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' -> true
                | _ -> failwith phoneAlphabetError
                ) phone

        let normalizedStringSeq = 
            Seq.map(fun letter -> letter.ToString()) normalizedCharSeq

        String.concat "" normalizedStringSeq        
    with 
        | _ -> phoneAlphabetError

/// Получить базу из последовательности данных.
let getPhonebook (coupleOfNameAndPhoneSeq : seq<string>) =
    Seq.fold (fun (acc : Map<string, string>) (coupleInString : string) ->
        let nameAndPhone = coupleInString.Split [|' '|]

        if nameAndPhone.Length = 2 then
            let name, phone = nameAndPhone.[0], nameAndPhone.[1]
            let normalizedPhone = normalizePhone phone

            if normalizedPhone = phoneAlphabetError then 
                acc
            else 
                acc.Add(name, phone)                        
        else
            acc
        ) Map.empty coupleOfNameAndPhoneSeq

/// Сохранить базу в файл.
let saveToFile path phonebookHashTable =  
    try 
        use file = File.CreateText(path)
        Map.iter (fun name phone ->        
            file.Write(name + " ", new obj())
            file.WriteLine(phone, new obj())
            ) phonebookHashTable
    with 
        | e -> printfn "%s" (e.ToString())    

/// Вывести управление.
let printInvitation () =
    printfn "\nДля выхода введите клавишу 'q'."   
    printfn "Для следующих операций вводите указанную клавишу."         
    printfn "1 - добавить запись (имя и телефон)"
    printfn "2 - найти телефон по имени"
    printfn "3 - найти имя по телефону"
    printfn "4 - вывести всё текущее содержимое базы"
    printfn "5 - сохранить текущие данные в файл"
    printfn "6 - считать данные из файла\n"        