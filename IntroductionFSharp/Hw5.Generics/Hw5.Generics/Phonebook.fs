/// Содержит реализацию телефонного справочника.
module Phonebook

open System.IO
open System
open PhonebookErrors
open PhonebookLogic

[<EntryPoint>]
let main argv =
    let rec interactiveMod phonebookHashTable = 
        printInvitation ()
        let request = Console.ReadLine()
        Console.Clear()

        match request with 
        | "q" -> 
            Console.Clear()            
            printfn "Произошёл выход из приложения." 
            Environment.Exit 0 
        | "1" -> 
            Console.Clear()            
            printfn "Введите имя, а потом телефон через Enter."
            let name = Console.ReadLine()
            let phone = Console.ReadLine()

            let normalizedPhone = normalizePhone phone   
            if normalizedPhone = phoneStringError then 
                printfn "%s" phoneStringError
                interactiveMod phonebookHashTable
            else 
                let updatedPhonebookHashTable = addRecord name normalizedPhone phonebookHashTable
                interactiveMod updatedPhonebookHashTable
        | "2" -> 
            Console.Clear()
            printfn "Введите имя, по которому будет производиться поиск."
            let name = Console.ReadLine().Trim()

            let result = findPhoneByName name phonebookHashTable
            if result = keyNotFoundError then printfn "%s" keyNotFoundError
            else printfn "%s имеет следующий телефон: %s\n\n" name result
            
            interactiveMod phonebookHashTable
        | "3" ->
            Console.Clear()
            
            printfn "Введите телефон, по которому будет производиться поиск."
            let phone = Console.ReadLine().Trim()

            let result = findNameByPhone phone phonebookHashTable
            if result = keyNotFoundError then printfn "%s" keyNotFoundError
            else printfn "Телефон %s имеет: %s\n\n" phone result
            
            interactiveMod phonebookHashTable
        | "4" ->
            Console.Clear()            
            printfn "Всё содержимое базы телефонного справочника:"
            
            if phonebookHashTable.Count = 0 then printfn "Телефонная база пуста."
            else printAll phonebookHashTable  

            interactiveMod phonebookHashTable
        | "5" ->
            Console.Clear() 
            printfn "Введите абсолютный путь к файлу (должен содержать имя файла), в который сохранить данные."

            let path = Console.ReadLine()

            saveToFile path phonebookHashTable            
                        
            interactiveMod phonebookHashTable
        | "6" ->
            Console.Clear()
            printfn "Учтите, что текущая база данных заменится загружаемой в случае, если файл существует."
            printfn "Для отмены операции введите 'q'."
            printfn "Введите абсолютный путь до файла, из которого считывать данные."            
            let request = Console.ReadLine()

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
                               
                    let loadedPhonebookHashTable = getPhonebook coupleOfNameAndPhoneSeq
            
                    interactiveMod loadedPhonebookHashTable
                with
                    | :? IO.FileNotFoundException -> 
                        printfn "%s" fileNotFoundError
                        interactiveMod phonebookHashTable
                    | :? IO.DirectoryNotFoundException -> 
                        printfn "%s" directoryNotFoundError
                        interactiveMod phonebookHashTable
                    | e ->
                        printfn "%s" (e.ToString())
                        interactiveMod phonebookHashTable

        | _ -> interactiveMod phonebookHashTable

    interactiveMod Map.empty
    0