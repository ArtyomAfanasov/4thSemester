module ``Testing``    
    open NUnit.Framework    
    open FsUnit

    [<Test>]
    let ``FirstEntry should complete with 4 for data: 5 in [1;2;3;4;5;6]`` () =
        FirstEntry.firstEntry [1;2;3;4;5;6] 5 |> should equal 4

    [<Test>]
    let ``FirstEntry should complete with 0 for data: 5 in [5;4;3]`` () =
        FirstEntry.firstEntry [5;4;3] 5 |> should equal 0

    [<Test>]
    let ``FirstEntry should complete with 5 for data: 5 in [0;1;2;3;4;5]`` () = 
        FirstEntry.firstEntry [0;1;2;3;4;5] 5 |> should equal 5

    [<Test>]
    let ``FirstEntry should complete with exception for data: 5 in []`` () =
        (fun () -> FirstEntry.firstEntry [] 5 |> ignore) |> should throw typeof<System.Exception> 

    [<Test>]
    let ``FirstEntry should complete with exception for data: 5 in [1;2;3]`` () =
        (fun () -> FirstEntry.firstEntry [1;2;3] 5 |> ignore) |> should throw typeof<System.Exception>   