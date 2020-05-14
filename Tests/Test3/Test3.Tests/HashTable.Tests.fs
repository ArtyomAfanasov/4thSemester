namespace Test3.Tests

open System
open Test3
open FsUnit
open NUnit.Framework

[<TestFixture>]
type Test3TestClass () = 
     
    [<Test>]
    member this.``addElement`` () =
        
        let hashTable = HashTable (fun x -> x % 20)
        hashTable.AddElem 5 |> ignore
        hashTable.IsExist 5 |> should equal true 
        
    [<Test>]
    member this.``delete not existed elem`` () =
        
        let hashTable = HashTable (fun x -> x % 20)
        hashTable.AddElem 5 |> ignore
        (fun () -> hashTable.Delete 10 |> ignore) |> should throw typeof<Exception>

    [<Test>]
    member this.``delete elem`` () =
        let hashTable = HashTable (fun x -> x % 20)
        hashTable.AddElem 5 |> ignore
        hashTable.AddElem 7 |> ignore
        hashTable.Delete 7 |> ignore
        hashTable.IsExist 7 |> should equal false

    [<Test>]
    member this.``addElement x4`` () =
        let hashTable = HashTable (fun x -> x % 20)
        hashTable.AddElem 5 |> ignore
        hashTable.AddElem 7 |> ignore
        hashTable.AddElem 9 |> ignore
        hashTable.IsExist 5 |> should equal true 
        hashTable.IsExist 7 |> should equal true 
        hashTable.IsExist 9 |> should equal true 

        hashTable.AddElem 25 |> ignore
        hashTable.IsExist 25 |> should equal true

    [<Test>]
    member this.``same key`` () =
        let hashTable = HashTable (fun x -> x % 20)
        hashTable.AddElem 5 |> ignore
        hashTable.AddElem 25 |> ignore
        
        hashTable.IsExist 5 |> should equal true 
        hashTable.IsExist 25 |> should equal true            