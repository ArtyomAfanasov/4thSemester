namespace MyLazy.Tests

open NUnit.Framework
open FsUnit
open LazyFactory
open ILazy
open System.Threading
open System.Diagnostics
open System

[<TestFixture>]
type SyncedLazyTestClass () =
    /// Вызвать метод Get у объекта типа, который реализуюет интерфейс ILazy.
    let invokeGetFrom (l : ILazy<int>) = l.Get ()

    [<Test>]
    member this.``SyncedMultithreadingLazyShouldCalculateValue`` () =
        // assert
        let syncedLazy = Factory.CreateSyncedLazy(fun () -> 5)
        
        // act
        let result = invokeGetFrom syncedLazy

        // assert
        result |> should equal 5

    [<Test>]
    member this.``MultithreadingLazyShouldReturnCalculatedResultWithoutCalculation`` () =
        // assert
        let invokeGetFrom (l : ILazy<obj>) = l.Get ()
        let syncedLazy = Factory.CreateSyncedLazy (fun () -> new Object())                    

        // act
        let firstCalculatedResult = invokeGetFrom syncedLazy
        let isSecondResult = invokeGetFrom syncedLazy
        
        // assert
        Object.ReferenceEquals(firstCalculatedResult, isSecondResult) |> should equal true
       
    [<Test>]
    member this.``Must not be race.`` () =        
        // arrange
        let invokeGetFrom (l : ILazy<obj>) = l.Get ()
        let syncedLazy = Factory.CreateLockFreeLazy (fun () -> new Object())       
    
        // assert
        let firstCalculatedResult = invokeGetFrom syncedLazy

        let calculationSeq = seq { for i in 1..100 -> syncedLazy }
        calculationSeq 
        |>
        Seq.map (fun worker ->
            async {
                let isOtherResult = invokeGetFrom worker
                Object.ReferenceEquals(firstCalculatedResult, isOtherResult) |> should equal true
            })
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

    [<Test>]
    member this.``IsValueCalculatedPropertyShouldShowCorrectSituation`` () =
        // arrange
        let syncedLazy = Factory.CreateSyncedLazy (fun () -> 5)
            
        // assert
        !syncedLazy.IsValueCreated |> should equal false
        invokeGetFrom syncedLazy |> ignore
        !syncedLazy.IsValueCreated |> should equal true