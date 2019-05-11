namespace Hw3.Tests

open FsUnit
open NUnit.Framework
open PrimeInfinity

[<TestFixture>]
type SimpleInfinityTestClass () =  

    [<Test>]
    member this.``s`` () =
        let primeInf = generatePrimeInfinity ()
        (fun () -> 
            (Seq.fold (fun number acc -> 
                match acc with
                | 1 when number = 2 -> 0
                | 2 when number = 3 -> 0
                | 3 when number = 5 -> 0
                | 4 when number = 7 -> 0
                | 5 when number = 11 -> 0
                | 6 when number = 13 -> 0
                | 7 when number = 17 -> 0
                | 8 when number = 19 -> 0
                | 9 when number = 23 -> 0
                | 10 when number = 29 -> 0
                | 11 when number = 31 -> 0
                | 12 when number = 37 -> 0
                | 13 when number = 41 -> 0
                | 14 when number = 43 -> 0
                | 15 -> failwith "stop!") primeInf)
            |> ignore) |> should throw typeof<System.Exception>

        let mutable forCheckPrimeNumbers = true

        (fun () -> 
            (Seq.fold (fun number acc ->
                match acc with 
                | 1 when number <> 0 -> 
                    forCheckPrimeNumbers <- false 
                    1
                | 2 when number <> 0 ->
                    forCheckPrimeNumbers <- false 
                    1
                | 3 when number <> 0 -> 
                    forCheckPrimeNumbers <- false 
                    1
                | 4 when number <> 0 ->
                    forCheckPrimeNumbers <- false 
                    1
                | 5 when number <> 0 ->            
                    forCheckPrimeNumbers <- false 
                    1
                | 6 when number <> 0 -> 
                    forCheckPrimeNumbers <- false 
                    1
                | 7 when number <> 0 ->             
                    forCheckPrimeNumbers <- false 
                    1
                | 8 when number <> 0 ->
                    forCheckPrimeNumbers <- false 
                    1
                | 9 when number <> 0 -> 
                    forCheckPrimeNumbers <- false 
                    1
                | 10 when number <> 0 ->
                    forCheckPrimeNumbers <- false 
                    1
                | 11 when number <> 0 ->             
                    forCheckPrimeNumbers <- false 
                    1
                | 12 when number <> 0 -> 
                    forCheckPrimeNumbers <- false 
                    1
                | 13 when number <> 0 ->             
                    forCheckPrimeNumbers <- false 
                    1
                | 14 when number <> 0 -> 
                    forCheckPrimeNumbers <- false 
                    1
                | 15 -> failwith "stop!") primeInf)  
            |> ignore) |> should throw typeof<System.Exception>

        forCheckPrimeNumbers |> should equal true