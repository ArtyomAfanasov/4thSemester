namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open NewTree

[<TestFixture>]
type NewTreeTestClass () =
    
    // Не нужно заводить новых переменных по типу expectedTree, 
    // т.к нужно сразу вставлять нужные значения в аргументы тестового метода?
    [<Test>]
    member this.``updateTree on three nodes tree Subtree(1, Dangling(2), Dangling(3)) with givenFun (fun x -> x + 2) should return Subtree(3, Dangling(4), Dangling(5))`` () =            
        updateTree (fun x -> x + 2) (Subtree(1, Dangling(2), Dangling(3)))
        |> should equal (Subtree(3, Dangling(4), Dangling(5)))  
        
    [<Test>]
    member this.``updateTree on one node tree Dangling(3) with givenFun (fun x -> x + 2) should return Dangling(5)`` () =        
        updateTree (fun x -> x + 2) (Dangling(3)) 
        |> should equal (Dangling(5))
        
    // Название страшное...
    [<Test>]
    member this.``updateTree on complex tree should correct apply function.`` () =        
        updateTree (fun x -> x + "!") (Subtree("a", Subtree("a", Dangling("a"), Subtree ("a", Dangling("a"), Empty)), Dangling("a")))
        |> should equal (Subtree("a!", Subtree("a!", Dangling("a!"), Subtree ("a!", Dangling("a!"), Empty)), Dangling("a!")))

    [<Test>]
    member this.``updateTree on tree Subtree(5, Empty, Empty) with givenFun (x + 2) should return Subtree(7, Empty, Empty)`` () =              
        updateTree (fun x -> x + 2) (Subtree(5, Empty, Empty))
        |> should equal (Subtree(7, Empty, Empty))    

    [<Test>]
    member this.``updateTree tree Subtree(1, Dangling(2), Dangling(3)) with givenFun (fun x -> ()) should return Subtree((), Dangling(()), Dangling(()))`` () =               
        updateTree (fun x -> ()) (Subtree(1, Dangling(2), Dangling(3)))
        |> should equal (Subtree((), Dangling(()), Dangling(()))) 

    [<Test>]
    member this.``updateTree tree Subtree(1.0, Dangling(2.0), Dangling(3.0)) with givenFun (fun x -> x * x) should return Subtree(1.0, Dangling(4.0), Dangling(9.0))`` () =               
        updateTree (fun x -> x * x) (Subtree(1.0, Dangling(2.0), Dangling(3.0)))
        |> should equal (Subtree(1.0, Dangling(4.0), Dangling(9.0))) 