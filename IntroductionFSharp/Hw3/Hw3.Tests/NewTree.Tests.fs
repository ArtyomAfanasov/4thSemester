namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open NewTree

[<TestFixture>]
type NewTreeTestClass () =
    
    // Не нужно заводить новых переменных по типу expectedTree, 
    // т.к нужно сразу вставлять нужные значения в аргументы тестового метода?
    [<Test>]
    member this.``updateTree on three nodes tree Tree(1, Dangling(2), Dangling(3)) with givenFun (fun x -> x + 2) should return Tree(3, Dangling(4), Dangling(5))`` () =            
        updateTree (fun x -> x + 2) (Tree(1, Dangling(2), Dangling(3)))
        |> should equal (Tree(3, Dangling(4), Dangling(5)))  
        
    [<Test>]
    member this.``updateTree on one node tree Dangling(3) with givenFun (fun x -> x + 2) should return Dangling(5)`` () =        
        updateTree (fun x -> x + 2) (Dangling(3)) 
        |> should equal (Dangling(5))
        
    // Название страшное...
    [<Test>]
    member this.``updateTree on complex tree Tree(1, Tree(2, Dangling(4), Tree (3, Dangling(4), TEmpty)), Dangling(2)) with givenFun (fun x -> x + "!") should return (Tree("a!", Tree("a!", Dangling("a!"), Tree ("a!", Dangling("a!"), Empty)), Dangling("a!")))`` () =        
        updateTree (fun x -> x + "!") (Tree("a", Tree("a", Dangling("a"), Tree ("a", Dangling("a"), Empty)), Dangling("a")))
        |> should equal (Tree("a!", Tree("a!", Dangling("a!"), Tree ("a!", Dangling("a!"), Empty)), Dangling("a!")))

    [<Test>]
    member this.``updateTree on tree Tree(5, Empty, Empty) with givenFun (x + 2) should return Tree(7, Empty, Empty)`` () =              
        updateTree (fun x -> x + 2) (Tree(5, Empty, Empty))
        |> should equal (Tree(7, Empty, Empty))    

    [<Test>]
    member this.``updateTree tree Tree(1, Dangling(2), Dangling(3)) with givenFun (fun x -> ()) should return Tree((), Dangling(()), Dangling(()))`` () =               
        updateTree (fun x -> ()) (Tree(1, Dangling(2), Dangling(3)))
        |> should equal (Tree((), Dangling(()), Dangling(()))) 

    [<Test>]
    member this.``updateTree tree Tree(1.0, Dangling(2.0), Dangling(3.0)) with givenFun (fun x -> x * x) should return Tree(1.0, Dangling(4.0), Dangling(9.0))`` () =               
        updateTree (fun x -> x * x) (Tree(1.0, Dangling(2.0), Dangling(3.0)))
        |> should equal (Tree(1.0, Dangling(4.0), Dangling(9.0))) 