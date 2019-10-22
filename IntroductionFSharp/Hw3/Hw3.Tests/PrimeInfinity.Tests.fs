namespace Hw3.Tests

open FsUnit
open NUnit.Framework
open PrimeInfinity

[<TestFixture>]
type SimpleInfinityTestClass () =         
    
    [<Test>]
    member this.``First 14 number should be prime.`` () =
        let primeInf = generatePrimeInfinity ()       
               
        let finalyPrimeSeq = [|2; 3; 5; 7; 11; 13; 17; 19; 23; 29|]

        let rec loopAssert index =
            match index with 
            | 10 -> ()
            | _ ->
                Seq.item index primeInf |> should equal finalyPrimeSeq.[index]
                loopAssert (index + 1)
                       
        loopAssert 0

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


    // Для Seq.filter.
    [<Test>]
    member this.``First 14 number should be prime. Filter`` () =
        let primeInf = generatePrimeInfinityByFilter ()       
       
        let finalyPrimeSeq = [|2; 3; 5; 7; 11; 13; 17; 19; 23; 29|]

        let rec loopAssert index =
            match index with 
            | 10 -> ()
            | _ ->
                Seq.item index primeInf |> should equal finalyPrimeSeq.[index]
                loopAssert (index + 1)
               
        loopAssert 0 

    [<Test>]
    member this.``27's, 167's, 657's and 1000's numbers should be prime. ByFilter`` () =
        let primeInf = generatePrimeInfinityByFilter ()       
               
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
    member this.``5000's numbers should be prime. ByFilter`` () =
        let primeInf = generatePrimeInfinityByFilter ()    

        let rec loop step acc =
            match step with            
            | 4999 when (Seq.item step primeInf) = 48611 -> loop (step + 1) (acc + 1)            
            | _ when step < 5000 -> loop (step + 1) acc
            | _ -> acc

        loop 0 0 |> should equal 1             
        
    [<Test>]
    member this.``10000's numbers should be prime. ByFilter`` () =
        let primeInf = generatePrimeInfinityByFilter ()    

        let rec loop step acc =
            match step with            
            | 9999 when (Seq.item step primeInf) = 104729 -> loop (step + 1) (acc + 1)            
            | _ when step < 10001 -> loop (step + 1) acc
            | _ -> acc

        loop 0 0 |> should equal 1 

    // Каждый раз считает элементы с начала до нужного => дооолго.
    [<Test>]
    member this.``14's number sould be prime ByFilter`` () =
        let primeInf = generatePrimeInfinityByFilter ()    
        
        let rec loop step =                                
            if ((Seq.item step primeInf) = 43) then 1                                              
            else loop (step + 1)                
        
        loop 0 |> should equal 1   