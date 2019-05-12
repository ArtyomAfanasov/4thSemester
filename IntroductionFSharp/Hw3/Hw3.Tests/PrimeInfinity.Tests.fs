namespace Hw3.Tests

open FsUnit
open NUnit.Framework
open PrimeInfinity

[<TestFixture>]
type SimpleInfinityTestClass () =         

    [<Test>]
    member this.``First 14 number should be prime.`` () =
        let primeInf = generatePrimeInfinity ()

        let rec loopPrint step =
            match step with 
            | 14 -> 14
            | _ -> 
                printf "%d " <| Seq.item step primeInf
                loopPrint (step + 1)

        loopPrint 0 |> ignore
               

        let rec loop step acc =
            match step with            
            | _ when (Seq.item step primeInf) = 2 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 3 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 5 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 7 -> loop (step + 1) (acc + 1)
            (*| _ when (Seq.item step primeInf) = 11 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 13 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 17 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 19 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 23 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 29 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 31 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 37 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 41 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step primeInf) = 43 -> loop (step + 1) (acc + 1)*)
            | _ -> acc

        loop 0 0 |> should equal 14    