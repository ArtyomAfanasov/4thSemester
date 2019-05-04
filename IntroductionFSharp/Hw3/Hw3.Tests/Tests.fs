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
    member this.``ttt`` () =
        ()

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [0; 1; 2; 3; 4; 5] should return 3`` () =
        countEvenNumberBySeqFilter [0; 1; 2; 3; 4; 5] |> should equal 3

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [1; 3; 5] should return 0`` () =
        countEvenNumberBySeqFilter [1; 3; 5] |> should equal 0

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [6; 6; 6; 6; 6] should return 5`` () =
        countEvenNumberBySeqFilter [6; 6; 6; 6; 6] |> should equal 5

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [] should return 0`` () =
        countEvenNumberBySeqFilter [] |> should equal 0    

    // Такое название теста хорошее? Или лучше писать нечто такое: 
    // "Функция на списке из строк должна бросать исключение"?
    [<Test>]
    member this.``countEvenNumberBySeqFilter on data ["string"; "list"] should throw Exception`` () =
        (fun () -> countEvenNumberBySeqFilter ["string"; "list"]  |> ignore) 
        |> should throw typeof<System.Exception>

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [false; true] should throw Exception`` () =
        (fun () -> countEvenNumberBySeqFilter [false; true]  |> ignore) 
        |> should throw typeof<System.Exception>    

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data ['c'; 'h'; 'a'; 'r'] should throw Exception`` () =
        (fun () -> countEvenNumberBySeqFilter ['c'; 'h'; 'a'; 'r'] |> ignore) 
        |> should throw typeof<System.Exception>

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [3.0; 2.0; 5.123] should throw Exception`` () =
        (fun () -> countEvenNumberBySeqFilter [3.0; 2.0; 5.123] |> ignore) 
        |> should throw typeof<System.Exception>

    // =================================TestTest ============================================

    [<Test>]
    member this.``Seq.append without hole`` () =
        let fSeq = {1..5}
        let sSeq = {6..12}
        Seq.append fSeq sSeq |> should equal {1..12}

    [<Test>]
    member this.``Seq.append with hole`` () =
        let fSeq = {1..5}
        let sSeq = {8..12}
        let res = Seq.append fSeq sSeq
        Seq.iter (fun a -> printf "(%i) " a) res
        ()

    [<Test>]
    member this.``Seq.concat`` () =
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