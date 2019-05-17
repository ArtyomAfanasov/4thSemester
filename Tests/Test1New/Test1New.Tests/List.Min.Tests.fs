namespace Test1New.Tests

open NUnit.Framework
open ListMin
open FsUnit

[<TestFixture>]
type ListMinTestClass () =

    [<Test>]
    member this.``listMin on [1; 2; 3] should return 1`` () =
        listMin [1; 2; 3] |> should equal 1     
    
    [<Test>]
    member this.``listMin on [1; 2; -5] should return -5`` () =
        listMin [1; 2; -5] |> should equal -5

    [<Test>]
    member this.``listMin on [1; 1; 1] should return 1`` () =
        listMin [1; 1; 1] |> should equal 1

    [<Test>]
    member this.``listMin on [0; 0; 0] should return 0`` () =
        listMin [0; 0; 0] |> should equal 0

    [<Test>]
    member this.``listMin on ["string"] should throw Exception`` () =
        (fun () -> listMin ["string"] |> ignore) |> should throw typeof<System.Exception>

    [<Test>]
    member this.``listMin on ['c'] should throw Exception`` () =
        (fun () -> listMin ['c'] |> ignore) |> should throw typeof<System.Exception>

    [<Test>]
    member this.``listMin on [true] should throw Exception`` () =
        (fun () -> listMin [true] |> ignore) |> should throw typeof<System.Exception>

    [<Test>]
    member this.``listMin on () should throw Exception`` () =
        (fun () -> listMin [()] |> ignore) |> should throw typeof<System.Exception>