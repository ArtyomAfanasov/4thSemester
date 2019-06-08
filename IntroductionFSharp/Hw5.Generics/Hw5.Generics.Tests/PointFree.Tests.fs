namespace Hw5.Generics.Tests

open FsCheck
open PointFree
open NUnit.Framework
open FsUnit

[<TestFixture>]
type PointFreeTestClass () =
    
    [<Test>]
    member this.``Example`` () =
        let sumFirst3 ls = ls |> Seq.fold (+) 0 |> (*) 5
        let sumFirstFree = Seq.fold (+) 0 >> (*) 5
        let pointFreeIsCorrect (l : list<int>) =
            sumFirst3 l = sumFirstFree l

        Check.QuickThrowOnFailure pointFreeIsCorrect

    [<Test>]
    member this.``Example2`` () =
        let func1 x = x * 5
        let func2 = (*) 5
        
        let pointFreeIsCorrect (x : int) =
            func1 x  = func2 x 

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
        let func1 x l = List.map ((*) x) l
        let func2 x = List.map ((*) x)
        
        let pointFreeIsCorrect (x : int, l : list<int>) =
            func1 x l = func2 x l

        Check.QuickThrowOnFailure pointFreeIsCorrect

    (*[<Test>]
    member this.``FsCheck for point-free function.`` () =
        let func x l = List.map (fun y -> y * x) l
        let pointFreeIsCorrect (x: int) (l : list<int>) = 
            (pointFree x l) = func x l
        Check.QuickThrowOnFailure pointFreeIsCorrect*)