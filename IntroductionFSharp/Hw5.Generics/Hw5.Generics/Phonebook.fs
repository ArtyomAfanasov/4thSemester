module Phonebook

open System.Collections

let exit () =
    ()

let addRecord (name : string) (phone : string) (hashName : Hashtable) (hashPhone : Hashtable) = 
    hashName.Add(name, phone)
    hashPhone.Add(phone, name)
    hashName, hashPhone

let findNumberByName (name : string) (hashName : Hashtable) = 
    match hashName.Item(name) with 
    | :? System.NotSupportedException -> "Данного человека нет в телефонной книге."
    | _ -> hashName.Item(name) :?> string
    //if hashName.Item(name) = typeof<System.NotSupportedException> then 
    //    "Данного человека нет в телефонной книге."
    //else 
    //   hashName.Item(name) :?> string
        //let phone = hashName.Item(name) :?> string
        //phone
    

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
    printfn "Для выхода нажмите клавишу 'q'."         
    printfn "добавить запись (имя и телефон)"
    printfn "найти телефон по имени"
    printfn "найти имя по телефону"
    printfn "вывести всё текущее содержимое базы"
    printfn "сохранить текущие данные в файл"
    printfn "считать данные из файла"            

[<EntryPoint>]
let main argv =
    let rec interactiveMod hashName hashPhone = 
        invitation ()
        let request = System.Console.ReadLine()
        match request with 
        | "1" -> printfn "Произошёл выход из приложения." 
        | "2" -> 
            printfn "Введите имя, а потом телефон через Enter."
            let name = System.Console.ReadLine()
            let phone = System.Console.ReadLine()

            let updatedHashName, updatedHashPhone = addRecord name phone hashName hashPhone
            interactiveMod updatedHashName updatedHashPhone
        | "3" -> 
            printfn "Введите имя, по которому будет производиться поиск."
            let name = System.Console.ReadLine()
            printfn "%s" (findNumberByName name hashName)

            interactiveMod hashName hashPhone
        | _ -> interactiveMod hashName hashPhone

    let hashName = new Hashtable()
    let hashPhone = new Hashtable()

    interactiveMod hashName hashPhone
    0