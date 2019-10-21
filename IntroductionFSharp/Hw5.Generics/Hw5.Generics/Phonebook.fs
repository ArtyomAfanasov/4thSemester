/// Содержит реализацию телефонного справочника.
module Phonebook

open System.IO
open PhonebookErrors
open PhonebookLogic

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
            System.Console.Clear() 
            printfn "Введите абсолютный путь к файлу (должен содержать имя файла), в который сохранить данные."

            let path = System.Console.ReadLine()

            try 
                saveToFile path phonebookHashTable
            with 
            | e -> printfn "%s" (e.ToString())
                        
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
                               
                    let loadedPhonebookHashTable = getPhonebook coupleOfNameAndPhoneSeq
            
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