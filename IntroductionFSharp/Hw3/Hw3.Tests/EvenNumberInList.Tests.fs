namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open FsCheck
open EvenNumberInList

[<TestFixture>]
type EvenNumberInListTestClass () =
    
    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [0; 1; 2; 3; 4; 5] should return 3`` () =
        countEvenNumberBySeqFilter [0; 1; 2; 3; 4; 5] |> should equal 3

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [1; 3; 5] should return 0`` () =
        countEvenNumberBySeqFilter [1; 3; 5] |> should equal 0

    // Name ``countEvenNumberBySeqFilter on repeating one even digit [6; 6; 6; 6; 6] 
    // should return correct amount 5`` is better than 
    // ``countEvenNumberBySeqFilter on data [6; 6; 6; 6; 6] should return 5``
    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [6; 6; 6; 6; 6] should return 5`` () =
        countEvenNumberBySeqFilter [6; 6; 6; 6; 6] |> should equal 5

    [<Test>]
    member this.``countEvenNumberBySeqFilter on data [] should return 0`` () =
        countEvenNumberBySeqFilter [] |> should equal 0         

    [<Test>]
    member this.``Functions via filter and fold should be equivalent.`` () =
        let checkFilterAndFold (list : list<int>) = countEvenNumberBySeqFilter list = countEvenNumberBySeqFold list

        Check.QuickThrowOnFailure checkFilterAndFold
        Check.Quick checkFilterAndFold

    [<Test>]
    member this.``Functions via filter and map should be equivalent.`` () =
        let checkFilterAndMap (list : list<int>) = countEvenNumberBySeqFilter list = countEvenNumberBySeqMap list

        Check.QuickThrowOnFailure checkFilterAndMap
        Check.Quick checkFilterAndMap

    [<Test>]
    member this.``Functions via fold and map should be equivalent.`` () =
        let checkFoldAndMap (list : list<int>) = countEvenNumberBySeqFold list = countEvenNumberBySeqMap list

        Check.QuickThrowOnFailure checkFoldAndMap
        Check.Quick checkFoldAndMap