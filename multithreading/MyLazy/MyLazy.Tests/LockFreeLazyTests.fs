namespace MyLazy.Tests

open NUnit.Framework
open FsUnit
open LazyFactory
open ILazy
open System.Threading
open System.Diagnostics

[<TestFixture>]
type LockFreeLazyTestClass () =
    /// Вызвать метод Get у объекта типа, который реализуюет интерфейс ILazy.
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
    member this.``LockFreeLazyShouldIgnoreSecondAndNextCalculations`` () =        
        // arrange
        let lockFreeLazy = Factory.CreateLockFreeLazy (fun () -> 5)       

        // act
    
        // assert
        ()

    [<Test>]
    member this.``IsValueCalculatedPropertyShouldShowCorrectSituation`` () =
        // arrange
        let invokeGetFrom (l : ILazy<int>) = l.Get ()                
        let lockFreeLazy = Factory.CreateLockFreeLazy (fun () -> 5)
            
        // assert
        !lockFreeLazy.IsValueCreated |> should equal false
        invokeGetFrom lockFreeLazy |> ignore
        !lockFreeLazy.IsValueCreated |> should equal true