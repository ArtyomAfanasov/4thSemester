namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open EvenNumberInList

type Person =
    { Name: string;
    DateOfBirth: System.DateTime; }

type IntOrBool = I of int | B of bool

type C = Circle of int | Rectangle of int * int

[<TestFixture>]
type EvenNumberInListTestClass () =
    
    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [0; 1; 2; 3; 4; 5] should return 3`` () =
        countEvenNumberBySeqFilter [0; 1; 2; 3; 4; 5] |> should equal 3

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [1; 3; 5] should return 0`` () =
        countEvenNumberBySeqFilter [1; 3; 5] |> should equal 0

    // Такое название теста хорошее? Или лучше писать нечто такое: 
    // "Функция на списке из нескольких одинаковых чётных элементов должна бросать исключение"?
    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [6; 6; 6; 6; 6] should return 5`` () =
        countEvenNumberBySeqFilter [6; 6; 6; 6; 6] |> should equal 5

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [] should return 0`` () =
        countEvenNumberBySeqFilter [] |> should equal 0            


    [<Test>]
    member this.``countEvenNumberBySeqFold on data [0; 1; 2; 3; 4; 5] should return 3`` () =
        countEvenNumberBySeqFold [0; 1; 2; 3; 4; 5] |> should equal 3

    [<Test>]
    member this.``countEvenNumberBySeqFold on data [1; 3; 5] should return 0`` () =
        countEvenNumberBySeqFold [1; 3; 5] |> should equal 0
    
    [<Test>]
    member this.``countEvenNumberBySeqFold on data [6; 6; 6; 6; 6] should return 5`` () =
        countEvenNumberBySeqFold [6; 6; 6; 6; 6] |> should equal 5

    [<Test>]
    member this.``countEvenNumberBySeqFold on data [] should return 0`` () =
        countEvenNumberBySeqFold [] |> should equal 0      


    [<Test>]
    member this.``countEvenNumberBySeqMap on data [0; 1; 2; 3; 4; 5] should return 3`` () =
        countEvenNumberBySeqMap [0; 1; 2; 3; 4; 5] |> should equal 3

    [<Test>]
    member this.``countEvenNumberBySeqMap on data [1; 3; 5] should return 0`` () =
        countEvenNumberBySeqMap [1; 3; 5] |> should equal 0
    
    [<Test>]
    member this.``countEvenNumberBySeqMap on data [6; 6; 6; 6; 6] should return 5`` () =
        countEvenNumberBySeqMap [6; 6; 6; 6; 6] |> should equal 5

    [<Test>]
    member this.``countEvenNumberBySeqMap on data [] should return 0`` () =
        countEvenNumberBySeqMap [] |> should equal 0      

    // =================================TestTest ============================================

    [<Test>]
    member this.``TestTest.append without hole`` () =
        let fSeq = {1..5}
        let sSeq = {6..12}
        Seq.append fSeq sSeq |> should equal {1..12}

    [<Test>]
    member this.``TestTest.append with hole`` () =
        let fSeq = {1..5}
        let sSeq = {8..12}
        let res = Seq.append fSeq sSeq
        Seq.iter (fun a -> printf "(%i) " a) res
        ()

    [<Test>]
    member this.``TestTest.concat`` () =
        let fSeq = {1..5}        
        let strangeSeq = [ [| 1; 2; 3 |]; [| 4; 5; 6 |]; [|7; 8; 9|] ]
        let seqResult = Seq.concat strangeSeq
        Seq.iter (fun a -> printf "(%i) " a) seqResult
        ()

    [<Test>]
    member this.``Records`` () =
        let person = { Name = "Anna";
            DateOfBirth = new System.DateTime(1968, 07, 23) }
        let { Name = name; DateOfBirth = date} = person

        let i = I 99
        let b = B true

        //Seq.iter (fun a -> printf "(%i) " a) (List.map Circle <| [1..10])
        printfn "%A"  (List.map Circle <| [1..10])

        let list = 
            [1..10]
            |> List.zip [21..30]
            |> List.map Rectangle

        printfn "%A" list
        ()