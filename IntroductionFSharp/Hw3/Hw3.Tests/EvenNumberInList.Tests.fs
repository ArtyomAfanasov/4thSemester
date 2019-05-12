namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open EvenNumberInList

[<TestFixture>]
type EvenNumberInListTestClass () =
    
    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [0; 1; 2; 3; 4; 5] should return 3`` () =
        countEvenNumberBySeqFilter [0; 1; 2; 3; 4; 5] |> should equal 3

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [1; 3; 5] should return 0`` () =
        countEvenNumberBySeqFilter [1; 3; 5] |> should equal 0

    // Такое название теста хорошее? Или лучше писать нечто такое: 
    // "Функция на списке из 6 одинаковых чётных элементов должна возвращать 5"?
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