namespace LastTest.Tests

open NUnit.Framework
open FsUnit
open InfinitySequence

[<TestFixture>]
type InfinitySequenceTestClass () =

    [<Test>]
    member this.``First 14 numbers should be correct.`` () =
        let seq = generateInfinitySequence ()       
                
        let rec loop step acc =
            match step with             
            | _ when (Seq.item step seq) = 1 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 2 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 2 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 3 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 3 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 3 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 4 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 4 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 4 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 4 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 5 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 5 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 5 -> loop (step + 1) (acc + 1)
            | _ when (Seq.item step seq) = 5 -> loop (step + 1) (acc + 1)
            | _ -> acc

        loop 0 0 |> should equal 14   
        
    [<Test>]
    member this.``3's numbers in seq should be 2.`` () =
        let seq = generateInfinitySequence ()           

        Seq.item 2 seq |> should equal 2

    [<Test>]
    member this.``325's numbers in seq should be 2.`` () =
        let seq = generateInfinitySequence ()           

        Seq.item 324 seq |> should equal 25

    [<Test>]
       member this.``1's numbers in seq should be 1.`` () =
           let seq = generateInfinitySequence ()           

           Seq.item 0 seq |> should equal 1