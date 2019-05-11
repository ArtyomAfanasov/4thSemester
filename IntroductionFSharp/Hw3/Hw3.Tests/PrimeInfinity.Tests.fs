namespace Hw3.Tests

open FsUnit
open NUnit.Framework
open PrimeInfinity

[<TestFixture>]
type SimpleInfinityTestClass () =  

    [<Test>]
    member this.``testtest`` () =
        let sumSeq sequence1 = Seq.fold (fun acc elem -> acc + elem) 0 sequence1
        Seq.init 10 (fun index -> index * index)
        |> sumSeq
        |> printfn "The sum of the elements is %d."        

    [<Test>]
    member this.``First 14 number should be prime.`` () =
        let primeInf = generatePrimeInfinity ()

        printf "%A" primeInf

        let rec loop step acc =
            match step with            
            | _ when (Seq.item step primeInf) = 2 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 3 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 5 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 7 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 11 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 13 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 17 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 19 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 23 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 29 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 31 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 37 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 41 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 43 -> loop (step + 1) (acc + 1)
            | _ -> acc

        loop 0 0 |> should equal 14

    (*[<Test>]
    member this.``First 14 number should be prime`` () =
        let primeInf = generatePrimeInfinity ()
        let mutable forCheckPrimeNumbers = 0
        (fun () -> 
            (Seq.fold (fun number acc -> 
                match acc with
                | 1 when number = 2 -> acc + 1
                | 2 when number = 3 -> acc + 1
                | 3 when number = 5 -> acc + 1
                | 4 when number = 7 -> acc + 1
                | 5 when number = 11 -> acc + 1
                | 6 when number = 13 -> acc + 1
                | 7 when number = 17 -> acc + 1
                | 8 when number = 19 -> acc + 1
                | 9 when number = 23 -> acc + 1
                | 10 when number = 29 -> acc + 1
                | 11 when number = 31 -> acc + 1
                | 12 when number = 37 -> acc + 1
                | 13 when number = 41 -> acc + 1
                | 14 when number = 43 -> acc + 1
                | 15 -> 
                    forCheckPrimeNumbers <- acc
                    failwith "stop!") 0 primeInf)
            |> ignore) |> should throw typeof<System.Exception>

        forCheckPrimeNumbers |> should equal 15      *)
        (*let mutable forCheckPrimeNumbers = true

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

        forCheckPrimeNumbers |> should equal true*)

    