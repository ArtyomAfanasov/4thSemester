namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open Palindrome

[<TestFixture>]
type ForTest1TestClass () =

    [<Test>]
    member this.``t`` () =
        (maxPalindrome ()).Value |> should equal 906609

    [<Test>]
    member this.``ss`` () = 
        isPolindrome 906609 |> should equal true

    [<Test>]
    member this.``asdasd`` () = 
        createFlipedListFromStr "12345" |> should equal [ '5'; '4'; '3'; '2'; '1' ]


