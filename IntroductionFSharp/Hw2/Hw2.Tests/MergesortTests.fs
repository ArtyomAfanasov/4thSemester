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
    member this.``splitOnTwo on data  [1] should return ([1], 0) `` () =
        splitOnTwo [1] |> should equal ([1], []) 

    [<Test>]
    member this.``splitOnTwo on data  [1; 2] should return ([1], [2]) `` () =
        splitOnTwo [1; 2] |> should equal ([1], [2]) 

    [<Test>]
    member this.``splitOnTwo on data [] should return ([], [])`` () =
        splitOnTwo [] |> should equal ([], []) 
        

    [<Test>]
    member this.``sortAndMergeTwoLists on data [3; 5; 8] [1; 10] should return [1; 3; 5; 8; 10]`` () =
        sortAndMergeTwoLists [3; 5; 8] [1; 10] |> should equal [1; 3; 5; 8; 10]

    [<Test>]
    member this.``sortAndMergeTwoLists on data [1; 10] [3; 5; 8] should return [1; 3; 5; 8; 10]`` () =
        sortAndMergeTwoLists [1; 10] [3; 5; 8] |> should equal [1; 3; 5; 8; 10]

    [<Test>]
    member this.``sortAndMergeTwoLists on data [2] [1] should return [1; 2]`` () =
        sortAndMergeTwoLists [2] [1] |> should equal [1; 2]

    [<Test>]
    member this.``sortAndMergeTwoLists on data [3] [] should return [3]`` () =
        sortAndMergeTwoLists [3] [] |> should equal [3]

    [<Test>]
    member this.``sortAndMergeTwoLists on data [] [3] should return [3]`` () =
        sortAndMergeTwoLists [] [3] |> should equal [3]
    
    [<Test>]
    member this.``mergesort on data [] should return []`` () =
        mergesort [] |> should equal []

    [<Test>]
    member this.``mergesort on data [1] should return [1]`` () =
        mergesort [1] |> should equal [1]

    [<Test>]
    member this.``mergesort on data [3; 2] should return [2; 3]`` () =
        mergesort [3; 2] |> should equal [2; 3]

    [<Test>]
    member this.``mergesort on data [3; 2; 1] should return [1; 2; 3]`` () =
        mergesort [3; 2; 1] |> should equal [1; 2; 3]   

    [<Test>]
    member this.``mergesort on data [1; 8; 2; 9; 3; 7; 4] should return [1; 2; 3; 4; 7; 8; 9]`` () =
        mergesort [1; 8; 2; 9; 3; 7; 4] |> should equal [1; 2; 3; 4; 7; 8; 9]

    [<Test>]
    member this.``mergesort on data [4; 3; 3; 2; 2; 1] should return [1; 2; 2; 3; 3; 4]`` () =
        mergesort [4; 3; 3; 2; 2; 1] |> should equal [1; 2; 2; 3; 3; 4]

    [<Test>]
    member this.``mergesort on data [5; -1; 4; -3] should return [-3; -1; 4; 5]`` () =
        mergesort [5; -1; 4; -3] |> should equal [-3; -1; 4; 5]   

    [<Test>]
    member this.``mergesort on data [1; 2; 3] should return [1; 2; 3]`` () =
        mergesort [1; 2; 3] |> should equal [1; 2; 3]

    [<Test>]
    member this.``mergesort on data [3.2; 2.8; 3.0] should return [2.8; 3.0; 3.2]`` () =
        mergesort [3.2; 2.8; 3.0] |> should equal [2.8; 3.0; 3.2]

    [<Test>]
    member this.``mergesort on data ["string"; "string again"] should return Exception.`` () =
        (fun () -> mergesort ["string"; "string again"] |> ignore) |> should throw typeof<System.Exception> 

    [<Test>]
    member this.``mergesort on data [false; true] should return Exception.`` () =
        (fun () -> mergesort [false; true] |> ignore) |> should throw typeof<System.Exception>    

    [<Test>]
    member this.``mergesort on data ['a'; 'b'; 'c'; 'd'] should return Exception.`` () =
        (fun () -> mergesort ['a'; 'b'; 'c'; 'd'] |> ignore) |> should throw typeof<System.Exception>                  