module Phonebook

let addRecord (name : string) (phone : string) (hashName : Map<string,string>) (hashPhone : Map<string,string>) = 
    hashName.Add(name, phone), hashPhone.Add(phone, name)    

let findNumberByName (name : string) (hashName : Map<string,string>) = 
    if not <| hashName.ContainsKey(name) then "Данной записи нет в телефонной книге."
    else hashName.Item(name)     

let findNameByPhone phone =
    ()

let printAll () =
    ()

let save () =
    ()

let load () =
    ()

    // TODO
let invitation () =
    printfn "==========Для выхода нажмите клавишу 'q'.===========" 
    printfn "Для выхода введите клавишу 'q'."   
    printfn "Для следующих операций вводите указанную клавишу."         
    printfn "1 - добавить запись (имя и телефон)"
    printfn "2 - найти телефон по имени"
    printfn "3 - найти имя по телефону"
    printfn "4 - вывести всё текущее содержимое базы"
    printfn "5 - сохранить текущие данные в файл"
    printfn "6 - считать данные из файла"            

[<EntryPoint>]
let main argv =
    let rec interactiveMod hashName hashPhone = 
        invitation ()
        let request = System.Console.ReadLine()
        match request with 
        | "q" -> 
            printfn "Произошёл выход из приложения." 
            System.Environment.Exit 0 
        | "1" -> 
            printfn "Введите имя, а потом телефон через Enter."
            let name = System.Console.ReadLine()
            let phone = System.Console.ReadLine()

            let updatedHashName, updatedHashPhone = addRecord name phone hashName hashPhone
            interactiveMod updatedHashName updatedHashPhone
        | "2" -> 
            printfn "Введите имя, по которому будет производиться поиск."
            let name = System.Console.ReadLine()
            printfn "%s" (findNumberByName name hashName)

            interactiveMod hashName hashPhone
        | _ -> interactiveMod hashName hashPhone

    let hashName = Map.empty
    let hashPhone = Map.empty

    interactiveMod hashName hashPhone
    0