namespace Hw3.Tests

open FsUnit
open NUnit.Framework
open PrimeInfinity

[<TestFixture>]
type SimpleInfinityTestClass () =         
    
    [<Test>]
    member this.``First 14 number should be prime.`` () =
        let primeInf = generatePrimeInfinity ()       
               
        // Нумерация элементов в посл-ти начинается с нуля.
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

    [<Test>]
    member this.``27's, 167's, 657's and 1000's numbers should be prime.`` () =
        let primeInf = generatePrimeInfinity ()       
               
        let rec loop step acc =
            match step with            
            | 26 when (Seq.item step primeInf) = 103 -> loop (step + 1) (acc + 1)
            | 166 when (Seq.item step primeInf) = 991 -> loop (step + 1) (acc + 1)
            | 656 when (Seq.item step primeInf) = 4919 -> loop (step + 1) (acc + 1)
            | 999 when (Seq.item step primeInf) = 7919 -> loop (step + 1) (acc + 1)
            | _ when step < 1001 -> loop (step + 1) acc
            | _ -> acc

        loop 0 0 |> should equal 4

    [<Test>]
    member this.``5000's numbers should be prime.`` () =
        let primeInf = generatePrimeInfinity ()    

        let rec loop step acc =
            match step with            
            | 4999 when (Seq.item step primeInf) = 48611 -> loop (step + 1) (acc + 1)            
            | _ when step < 5000 -> loop (step + 1) acc
            | _ -> acc

        loop 0 0 |> should equal 1             
        
    [<Test>]
    member this.``10000's numbers should be prime.`` () =
        let primeInf = generatePrimeInfinity ()    

        let rec loop step acc =
            match step with            
            | 9999 when (Seq.item step primeInf) = 104729 -> loop (step + 1) (acc + 1)            
            | _ when step < 10001 -> loop (step + 1) acc
            | _ -> acc

        loop 0 0 |> should equal 1 

    // Каждый раз считает элементы с начала до нужного => дооолго.
    [<Test>]
    member this.``14's number sould be prime `` () =
        let primeInf = generatePrimeInfinity ()    
        
        let rec loop step =                                
            if ((Seq.item step primeInf) = 43) then 1                                              
            else loop (step + 1)                
        
        loop 0 |> should equal 1   