namespace Hw5.Generics.Tests

open FsCheck
open NUnit.Framework
open FsUnit

/// Записать в point-free стиле func x l = List.map (fun y -> y * x) l. 
[<TestFixture>]
type PointFreeTestClass () =    
    [<Test>]
    member this.``Main.`` () =
        let func1 x l = List.map (fun y -> y * x) l
        let func2 x l = List.map ((*) x) l
        let func2 x = List.map ((*) x)
        let func2 x = List.map (x |> (*))
        let func2 x = (x |> (*)) |> List.map
        let func2 x = x |> (*) |> List.map
        let func2 = (*) >> List.map
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect
    
    [<Test>]
    member this.``Step1`` () =
        let func1 x l = List.map (fun y -> y * x) l
        let func2 x l = List.map ((*) x) l
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect

    [<Test>]
    member this.``Step2`` () =
        let func1 x l = List.map ((*) x) l
        let func2 x = List.map ((*) x)
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect
    
    [<Test>]
    member this.``Step3`` () =
        let func1 x = List.map ((*) x)
        let func2 x = List.map (x |> (*))
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect
    
    [<Test>]
    member this.``Step4`` () =
        let func1 x = List.map (x |> (*))
        let func2 x = (x |> (*)) |> List.map
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect 
    
    [<Test>]
    member this.``Step5`` () =
        let func1 x = (x |> (*)) |> List.map
        let func2 x = x |> (*) |> List.map
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect
    
    [<Test>]
    member this.``Final step.`` () =
        let func1 x = x |> (*) |> List.map
        let func2 = (*) >> List.map
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect