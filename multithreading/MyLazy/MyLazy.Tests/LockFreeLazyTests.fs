namespace MyLazy.Tests

open NUnit.Framework
open FsUnit
open LazyFactory
open ILazy
open System.Threading
open System.Diagnostics
open System

[<TestFixture>]
type LockFreeLazyTestClass () =
    /// Вызвать метод Get у объекта типа, который реализует интерфейс ILazy.
    let invokeGetFrom (l : ILazy<int>) = l.Get ()

    [<Test>]
    member this.``LockFreeLazyShouldCalculate5Plus5`` () =
        // assert
        let lockFreeLazy = Factory.CreateLockFreeLazy(fun () -> 5 + 5)
        
        // act
        let result = invokeGetFrom lockFreeLazy

        // assert
        result |> should equal 10
    
    [<Test>]
    member this.``ResultMustNotDisappear`` () =        
        // arrange
        let lockFreeLazy = Factory.CreateLockFreeLazy (fun () -> 5)       
    
        // assert
        invokeGetFrom lockFreeLazy |> should equal 5

        let calculationSeq = seq { for i in 1..100 -> lockFreeLazy }
        calculationSeq 
        |>
        Seq.map (fun worker ->
            async {
                invokeGetFrom worker |> ignore
            })
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

        invokeGetFrom lockFreeLazy |> should equal 5
    
    [<Test>]
    member this.``Must not be race.`` () =        
        // arrange
        let invokeGetFrom (l : ILazy<obj>) = l.Get ()
        let lockFreeLazy = Factory.CreateLockFreeLazy (fun () -> new Object())       

        let rec loop index (array : Object[])  =
            if index = (Seq.length array) - 1 then () 
            else array.[index] |> should equal array.[index + 1]

        let calculationSeq = seq { for i in 1..1000 -> lockFreeLazy }
        let arr = 
            calculationSeq 
            |>
            Seq.map (fun worker ->
                async {
                    return invokeGetFrom worker
                })
            |> Async.Parallel
            |> Async.RunSynchronously

        loop 0 arr

    [<Test>]
    member this.``IsValueCalculatedPropertyShouldShowCorrectSituation`` () =
        // arrange
        let invokeGetFrom (l : ILazy<int>) = l.Get ()                
        let lockFreeLazy = Factory.CreateLockFreeLazy (fun () -> 5)
            
        // assert
        !lockFreeLazy.IsValueCreated |> should equal false
        invokeGetFrom lockFreeLazy |> ignore
        !lockFreeLazy.IsValueCreated |> should equal true