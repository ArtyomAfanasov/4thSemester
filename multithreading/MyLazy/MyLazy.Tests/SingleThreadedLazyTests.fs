namespace MyLazy.Tests

open NUnit.Framework
open FsUnit
open LazyFactory
open ILazy
open System.Threading
open System.Diagnostics

[<TestFixture>]
type SingleThreadedLazyTestClass () =
    // ��� �����������? :) ���� �� ��������, �� ���������� �������� �� � ���� ������� :)
    /// ������� ����� Get � ������� ����, ������� ���������� ��������� ILazy.
    let invokeIntGetFrom (l : ILazy<int>) = l.Get ()     
        
    [<Test>]
    member this.``IntSingleLazyShouldCalculateValue`` () =
        // assert
        let singleLazy = Factory.CreateSingleThreadedLazy (fun () -> 5)    
        
        // act
        let result = invokeIntGetFrom singleLazy

        // assert
        result |> should equal 5
    
    [<Test>]
    member this.``SingleLazyShouldReturnCalculatedResultWithoutCalculation`` () =
        // assert
        let singleLazy = Factory.CreateSingleThreadedLazy (fun () -> 
            
            // ���� �� ����� ���������� �������� (�� ��������) ����������� �� ������� ��?
            Thread.Sleep(10)
            5)
        
        // act
        invokeIntGetFrom singleLazy |> ignore
        
        let stopwatch = Stopwatch.StartNew();
        let result = invokeIntGetFrom singleLazy
        stopwatch.Stop()

        // assert
        result |> should equal 5        
        stopwatch.ElapsedMilliseconds |> should lessThan 10
    
    [<Test>]
    member this.``SingleLazyShouldWorkWithString`` () =
        // arrange
        let invokeStringGetFrom (l : ILazy<string>) = l.Get ()                
        let singleLazy = Factory.CreateSingleThreadedLazy (fun () -> "I'm work!")
        
        // act
        let result = invokeStringGetFrom singleLazy

        // assert
        result |> should equal "I'm work!"
    
    [<Test>]
    member this.``IsValueCalculatedPropertyShouldShowCorrectSituation`` () =
        // arrange
        let invokeGetFrom (l : ILazy<int>) = l.Get ()                        
        let singleLazy = Factory.CreateSingleThreadedLazy (fun () -> 5)    

        // assert
        singleLazy.IsValueCreated |> should equal false
        invokeGetFrom singleLazy |> ignore
        singleLazy.IsValueCreated |> should equal true