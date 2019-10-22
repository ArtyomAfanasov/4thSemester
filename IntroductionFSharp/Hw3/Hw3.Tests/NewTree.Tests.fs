namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open NewTree

[<TestFixture>]
type NewTreeTestClass () =
    
    /// Лист со значением.
    let leaf value = Subtree(value, Empty, Empty)

    /// Поддерево.
    let tree value left right = Subtree(value, left, right)
    
    /// Конец.
    let (!) = Empty

    [<Test>]
    member this.``updateTree on three nodes tree should correct apply function.`` () =            
        updateTree (fun x -> x + 2) (tree 1 (leaf 2) (leaf 3))
        |> should equal (tree 3 (leaf 4) (leaf 5))
            
    [<Test>]
    member this.``updateTree on one node tree should correct apply function.`` () =        
        updateTree (fun x -> x + 2) (leaf 3) 
        |> should equal (leaf 5)
        
    // Название страшное...
    [<Test>]
    member this.``updateTree on complex tree should correct apply function.`` () =        
        updateTree (fun x -> x + "!") (tree "a" (tree "a" (leaf "a") (tree "a" (leaf "a") (!))) (leaf "a"))
        |> should equal (tree "a!" (tree "a!" (leaf "a!") (tree "a!" (leaf "a!") (!))) (leaf "a!"))        