namespace Hw2.Tests

open NUnit.Framework
open FsUnit
open Mergesort

[<TestFixture>]
type MergesortTestsClass () =

    [<Test>]
    member this.``splitOnTwo on data [1; 2; 3; 4] should return ([3; 1], [4; 2])`` () =
        splitOnTwo [1; 2; 3; 4] |> should equal ([3; 1], [4; 2])

    [<Test>]
    member this.``splitOnTwo on data  [1; 2; 3; 4; 5] should return ([5; 3; 1], [4; 2]) `` () =
        splitOnTwo [1; 2; 3; 4; 5] |> should equal ([5; 3; 1], [4; 2]) 

    [<Test>]
    member this.``splitOnTwo on data [] should return ([], [])`` () =
        splitOnTwo [] |> should equal ([], []) 
        
    [<Test>]
    member this.``sortAndMergeTwoLists on data [5; 3; 4] [10; 8] should return [3; 4; 5; 8; 10]`` () =
        sortAndMergeTwoLists [5; 3; 4] [10; 8] |> should equal [3; 4; 5; 8; 10]

    // ===============================================TestTest=======================
    
    [<Test>]
    member this.``Test Test`` () =
        [1, 2] @ [] |> should equal [1, 2]                     