namespace MyLazy.Tests

open NUnit.Framework
open FsUnit
open LazyFactory
open ILazy

[<TestFixture>]
type SingleThreadedLazyTestClass () =

    [<Test>]
    member this.``SingleLazyShouldCalculate5Plus5`` () =
        // arrange
        let calculateFrom (l : ILazy<int>) = l.Get ()        
        let singleLazy = Factory.CreateSingleThreadedLazy (fun () -> 5 + 5)
        
        // act
        let result = calculateFrom singleLazy

        // assert
        result |> should equal 10
    
    [<Test>]
    member this.``SingleLazyShouldReturnResultAfterSecondCalculation`` () =
        // arrange
        let calculateFrom (l : ILazy<int>) = l.Get ()        
        let singleLazy = Factory.CreateSingleThreadedLazy (fun () -> 5 + 5)
        
        // act
        calculateFrom singleLazy |> ignore
        let result = calculateFrom singleLazy

        // assert
        result |> should equal 10
    
    [<Test>]
    member this.``SingleLazyShouldWorkWithString`` () =
        // arrange
        let calculateFrom (l : ILazy<string>) = l.Get ()        
        let singleLazy = Factory.CreateSingleThreadedLazy (fun () -> "I'm work!")
        
        // act
        let result = calculateFrom singleLazy

        // assert
        result |> should equal "I'm work!"
