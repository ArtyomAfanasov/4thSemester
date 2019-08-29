namespace MyLazy.Tests

open NUnit.Framework
open FsUnit
open LazyFactory
open ILazy
open System.Threading
open System.Diagnostics

[<TestFixture>]
type SyncedMultithreadingLazyTestClass () =
    /// Вызвать метод Get у объекта типа, который реализуюет интерфейс ILazy.
    let invokeGetFrom (l : ILazy<int>) = l.Get ()

    [<Test>]
    member this.``SyncedMultithreadingLazyShouldCalculate5Plus5`` () =
        // assert
        let SyncedLazy = Factory.CreateSyncedLazy(fun () -> 5 + 5)
        
        // act
        let result = invokeGetFrom SyncedLazy

        // assert
        result |> should equal 10

    [<Test>]
    member this.``MultithreadingLazyShouldReturnCalculatedResultWithoutCalculation`` () =
        // assert
        let SyncedLazy = Factory.CreateSyncedLazy (fun () -> 
            Thread.Sleep(10)
            5)
        
        // act
        invokeGetFrom SyncedLazy |> ignore

        let stopwatch = Stopwatch.StartNew();
        let result = invokeGetFrom SyncedLazy
        stopwatch.Stop()

        // assert
        result |> should equal 5
        stopwatch.ElapsedMilliseconds |> should lessThan 10
    
    [<Test>]
    member this.``MultithreadingLazyShouldCalculateOneTimeWhen50Threads`` () =
        // arrange
        let SyncedLazy = Factory.CreateSyncedLazy (fun () -> 
            
            // 50 мс должно ведь хватить, да? Где Вы читали, что на квант времени выделено 20-30 секунд, а потом происходит переключение контекста?
            // У .NET алгоритм переключение потоков располагает функцией передачи управления другому потоку, если данный поток выполнил свою работу за время,
            // меньшее кванта времени? 
            // 
            // Если судить по данному тесту, то должен располагать :) 
            // Но по одной из аксиом: в каждой программе есть ошибка (= 
            // Поэтому сомневаюсь.
            Thread.Sleep(50)
            5)
        let mutable result = 0
        let calculate () = 
            result <- invokeGetFrom SyncedLazy            
        let threads = Array.init 10 (fun index -> new Thread(calculate))        

        // act
        let stopwatch = Stopwatch.StartNew();
        
        // По логике написанных Start'а и Join'а, мы ждём те потоки, которые ещё работают?
        for thread in threads do
            thread.Start()                
        for thread in threads do
            thread.Join()    
        stopwatch.Stop()

        // assert
        result |> should equal 5
        stopwatch.ElapsedMilliseconds |> should lessThan 60
        
    [<Test>]
    member this.``IsValueCalculatedPropertyShouldShowCorrectSituation`` () =
        // arrange
        let invokeGetFrom (l : ILazy<int>) = l.Get ()                
        let SyncedLazy = Factory.CreateSyncedLazy (fun () -> 5)
            
        // assert
        !SyncedLazy.IsValueCreated |> should equal false
        invokeGetFrom SyncedLazy |> ignore
        !SyncedLazy.IsValueCreated |> should equal true