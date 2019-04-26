namespace PriorityQueueTest

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open PirorityQueue
open NUnit.Framework
open FsUnit

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.``Enqueue to empty queue``() =
        let emptyQueue = MyQueue.Empty        
        let expected = MyQueue(5, MyQueue.Empty)
        emptyQueue.Enqueue 5 |> should equal expected

    [<Test>]
    member this.``Enqueue 1 element to empty queue`` () =
        let emptyQueue = MyQueue.Empty  
        let expected = MyQueue(5, MyQueue(5, MyQueue.Empty))
        (emptyQueue.Enqueue 5).Enqueue 5 |> should equal expected

    [<TestMethod>]
    member this.``Enqueue to not empty queue``() =
        let emptyQueue = MyQueue.Empty 
        let notEmptyQueue = emptyQueue.Enqueue 5
        let expected = MyQueue(5, MyQueue(5, MyQueue.Empty))
        notEmptyQueue.Enqueue 5 |> should equal expected                