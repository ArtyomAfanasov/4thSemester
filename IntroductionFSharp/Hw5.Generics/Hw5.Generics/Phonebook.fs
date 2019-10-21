/// Содержит реализацию телефонного справочника.
module Phonebook

/// Ошибка: "Данной записи нет в телефонной книге."
let keyNotFoundError = "Данной записи нет в телефонной книге."

/// Ошибка: "Для записи телефона доступны только символы из алфавита: { 0..9, +, -, (, ) }."
let phoneStringError = "Для записи телефона доступны только символы из алфавита: { 0..9, +, -, (, ) }."

/// Добавить запись (имя, телефон).
let addRecord (name : string) (phone : string) (phoneHashTable : Map<string,string>) = 
    phoneHashTable.Add(name, phone)    

/// Найти номер по имени.
let findPhoneByName (name : string) (phoneHashTable : Map<string,string>) = 
    if not <| phoneHashTable.ContainsKey(name) then keyNotFoundError
    else phoneHashTable.Item(name)     

/// Найти имя по телефону.
let findNameByPhone (phone : string) (phoneHashTable : Map<string,string>) =
    match Map.tryFindKey (fun name phoneElement ->  phoneElement = phone) phoneHashTable with
    | Some(key) -> key
    | None -> keyNotFoundError

/// Вывести все пары (имя, телефон) из базы.
let printAll phoneHashTable =
    Map.iter (fun name phone ->
        printfn "Владалец телефона %s --- это %s" phone name) phoneHashTable

/// Сохранить данные в файл.
let save () =
    ()

/// Загрузить данные из файла.
let load () =
    ()

/// Нормализовать введённый номер.
let normalizePhone (phone : string) = 
    let normalizedCharSeq =
        Seq.filter (fun letter -> 
        match letter with
        | '+' | '(' | ')' | '-' -> false
        | '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' -> true
        | _ -> failwith phoneStringError) phone

    let normalizedStringSeq = 
        Seq.map(fun letter ->
            letter.ToString()) normalizedCharSeq

    String.concat "" normalizedStringSeq

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
    let rec interactiveMod phoneHashTable = 
        printInvitation ()

        let request = System.Console.ReadLine()
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

            let updatedPhoneHashTable = addRecord name normalizedPhone phoneHashTable
            interactiveMod updatedPhoneHashTable
        | "2" -> 
            System.Console.Clear()

            printfn "Введите имя, по которому будет производиться поиск."
            let name = System.Console.ReadLine()
            printfn "%s имеет следующий телефон: %s\n\n" name (findPhoneByName name phoneHashTable)

            interactiveMod phoneHashTable
        | "3" ->
            System.Console.Clear()
            
            printfn "Введите телефон, по которому будет производиться поиск."
            let phone = System.Console.ReadLine()
            printfn "Телефон %s имеет: %s\n\n" phone (findNameByPhone phone phoneHashTable)

            interactiveMod phoneHashTable
        | "4" ->
            System.Console.Clear()
            
            printfn "Всё содержимое базы телефонного справочника:"
            
            if phoneHashTable.Count = 0 then printfn "Телефонная база пуста.\n\n"
            else printAll phoneHashTable               

            interactiveMod phoneHashTable

        | _ -> interactiveMod phoneHashTable

    interactiveMod Map.empty
    0