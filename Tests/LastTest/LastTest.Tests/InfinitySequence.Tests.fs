namespace LastTest.Tests

open NUnit.Framework
open FsUnit
open InfinitySequence

[<TestFixture>]
type InfinitySequenceTestClass () =               

    [<Test>]
    member this.``3's numbers in seq should be 2.`` () =
        let seq = generateInfinitySequence ()           

        Seq.item 2 seq |> should equal 2

    [<Test>]
    member this.``4's numbers in seq should be 3.`` () =
        let seq = generateInfinitySequence ()           

        Seq.item 3 seq |> should equal 3

    [<Test>]
    member this.``325's numbers in seq should be 25.`` () =
        let seq = generateInfinitySequence ()           

        Seq.item 324 seq |> should equal 25

    [<Test>]
       member this.``1's numbers in seq should be 1.`` () =
           let seq = generateInfinitySequence ()           

           Seq.item 0 seq |> should equal 1

    [<Test>]
        member this.``16's numbers in seq should be 6.`` () =
            let seq = generateInfinitySequence ()           

            Seq.item 15 seq |> should equal 6

    [<Test>]
        member this.``18's numbers in seq should be 6.`` () =
            let seq = generateInfinitySequence ()           

            Seq.item 17 seq |> should equal 6

    [<Test>]
        member this.``5's numbers in seq should be 3.`` () =
            let seq = generateInfinitySequence ()           

            Seq.item 4 seq |> should equal 3