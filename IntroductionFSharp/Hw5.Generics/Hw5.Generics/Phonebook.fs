/// Содержит реализацию телефонного справочника.
module Phonebook

open System.IO
open System

/// Ошибка: "Данной записи нет в телефонной книге."
let keyNotFoundError = "Данной записи нет в телефонной книге."

/// Ошибка: "Для записи телефона доступны только символы из алфавита: { 0..9, +, -, (, ) }."
let phoneStringError = "Для записи телефона доступны только символы из алфавита: { 0..9, +, -, (, ) }."

/// Ошибка: "Файл не найден."
let fileNotFoundError = "Файл не найден."

/// Ошибка: "Директория не найдена."
let directoryNotFoundError = "Директория не найдена."

/// Добавить запись (имя, телефон).
let addRecord (name : string) (phone : string) (phonebookHashTable : Map<string,string>) = 
    phonebookHashTable.Add(name, phone)    

/// Найти номер по имени.
let findPhoneByName (name : string) (phonebookHashTable : Map<string,string>) = 
    if not <| phonebookHashTable.ContainsKey(name) then keyNotFoundError
    else phonebookHashTable.Item(name)     

/// Найти имя по телефону.
let findNameByPhone (phone : string) (phonebookHashTable : Map<string,string>) =
    match Map.tryFindKey (fun name phoneElement ->  phoneElement = phone) phonebookHashTable with
    | Some(key) -> key
    | None -> keyNotFoundError

/// Вывести все пары (имя, телефон) из базы.
let printAll phonebookHashTable =
    Map.iter (fun name phone ->
        printfn "Владалец телефона %s --- это %s" phone name) phonebookHashTable

/// Сохранить данные в файл.
let save () =
    ()

/// Загрузить данные из файла.
let load () =
    ()

/// Нормализовать введённый номер.
let normalizePhone (phone : string) = 
    try 
        let normalizedCharSeq =
            Seq.filter (fun letter -> 
            match letter with
            | '+' | '(' | ')' | '-' -> false
            | '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' -> true
            | _ -> failwith phoneStringError
            ) phone

        let normalizedStringSeq = 
            Seq.map(fun letter ->
                letter.ToString()
                ) normalizedCharSeq

        String.concat "" normalizedStringSeq        
    with 
        | _ -> phoneStringError

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

[<EntryPoint>]
let main argv =
    let rec interactiveMod phonebookHashTable = 
        printInvitation ()
        let request = System.Console.ReadLine()
        System.Console.Clear()

        match request with 
        | "q" -> 
            System.Console.Clear()            
            printfn "Произошёл выход из приложения." 
            System.Environment.Exit 0 
        | "1" -> 
            System.Console.Clear()            
            printfn "Введите имя, а потом телефон через Enter."
            let name = System.Console.ReadLine()
            let phone = System.Console.ReadLine()

            let normalizedPhone = normalizePhone phone   
            if normalizedPhone = phoneStringError then 
                printfn "%s" phoneStringError
                interactiveMod phonebookHashTable
            else 
                let updatedPhonebookHashTable = addRecord name normalizedPhone phonebookHashTable
                interactiveMod updatedPhonebookHashTable
        | "2" -> 
            System.Console.Clear()
            printfn "Введите имя, по которому будет производиться поиск."
            let name = System.Console.ReadLine()

            printfn "%s имеет следующий телефон: %s\n\n" name (findPhoneByName name phonebookHashTable)

            interactiveMod phonebookHashTable
        | "3" ->
            System.Console.Clear()
            
            printfn "Введите телефон, по которому будет производиться поиск."
            let phone = System.Console.ReadLine()
            printfn "Телефон %s имеет: %s\n\n" phone (findNameByPhone phone phonebookHashTable)

            interactiveMod phonebookHashTable
        | "4" ->
            System.Console.Clear()            
            printfn "Всё содержимое базы телефонного справочника:"
            
            if phonebookHashTable.Count = 0 then printfn "Телефонная база пуста."
            else printAll phonebookHashTable  

            interactiveMod phonebookHashTable
        | "5" ->
            // ToDO

            
            
            interactiveMod phonebookHashTable
        | "6" ->
            System.Console.Clear()
            printfn "Учтите, что текущая база данных заменится загружаемой в случае, если файл существует."
            printfn "Для отмены операции введите 'q'."
            printfn "Введите абсолютный путь до файла, из которого считывать данные."            
            let request = System.Console.ReadLine()

            match request with 
            | "q" -> interactiveMod phonebookHashTable
            | _ ->    
                let path = request

                try
                    let coupleOfNameAndPhoneSeq =
                           seq {
                               use reader = 
                                   new StreamReader(
                                       File.OpenRead(path))

                           while not reader.EndOfStream do
                               yield reader.ReadLine() }
                               
                    let loadedPhonebookHashTable = 
                        Seq.fold (fun (acc : Map<string,string>) (coupleInString : string)->
                            let nameAndPhone = coupleInString.Split [|' '|]

                            if nameAndPhone.Length = 2 then
                                acc.Add(nameAndPhone.[0], nameAndPhone.[1])
                            else
                                acc
                            ) Map.empty coupleOfNameAndPhoneSeq
            
                    interactiveMod loadedPhonebookHashTable
                with
                    | :? System.IO.FileNotFoundException -> 
                        printfn "%s" fileNotFoundError
                        interactiveMod phonebookHashTable
                    | :? System.IO.DirectoryNotFoundException -> 
                        printfn "%s" directoryNotFoundError
                        interactiveMod phonebookHashTable
                    | e ->
                        printfn "%s" (e.ToString())
                        interactiveMod phonebookHashTable

        | _ -> interactiveMod phonebookHashTable

    interactiveMod Map.empty
    0