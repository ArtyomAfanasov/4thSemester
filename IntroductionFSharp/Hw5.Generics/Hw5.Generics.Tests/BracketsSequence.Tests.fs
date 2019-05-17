namespace Hw5.Generics.Tests

open BracketSequence
open NUnit.Framework
open FsUnit

[<TestFixture>]
type BracketSequenceTestClass () =
    
    [<Test>]
    member this.``checkBrackets on data "((()))" should return true`` () =
        checkBrackets "((()))" |> should equal true

    [<Test>]
    member this.``checkBrackets on data "[[[]]]" should return true`` () =
        checkBrackets "[[[]]]" |> should equal true

    [<Test>]
    member this.``checkBrackets on data "{{{}}}" should return true`` () =
        checkBrackets "{{{}}}" |> should equal true

    [<Test>]
    member this.``checkBrackets on data "))((" should return false`` () =
        checkBrackets "))((" |> should equal false

    [<Test>]
    member this.``checkBrackets on data "]][[" should return false`` () =
        checkBrackets "]][[" |> should equal false

    [<Test>]
    member this.``checkBrackets on data "}}{{" should return false`` () =
        checkBrackets "}}{{" |> should equal false

    [<Test>]
    member this.``checkBrackets on data "(]" should return false`` () =
        checkBrackets "(]" |> should equal false

    [<Test>]
    member this.``checkBrackets on data "[[[]]]" should return false`` () =
        checkBrackets "[)" |> should equal false

    [<Test>]
    member this.``checkBrackets on data "{{{}}}" should return false`` () =
        checkBrackets "{)" |> should equal false

    [<Test>]
    member this.``checkBrackets on data "" should return true`` () =
        checkBrackets "" |> should equal true

    [<Test>]
    member this.``checkBrackets on data "(a[b{c}b]b)" should return true`` () =
        checkBrackets "(a[b{c}b]b)" |> should equal true

    [<Test>]
    member this.``checkBrackets on data "(a[b{c}b)b]" should return false`` () =
        checkBrackets "(a[b{c}b)b]" |> should equal false

    [<Test>]
    member this.``checkBrackets on data "(a[b{c{b]b)" should return false`` () =
        checkBrackets "(a[b{c{b]b)" |> should equal false   

    [<Test>]
    member this.``checkBrackets on data ")a)b)c)b)b)" should return false`` () =
        checkBrackets ")a)b)c)b)b)" |> should equal false   

    [<Test>]
    member this.``checkBrackets on data ") )" should return false`` () =
        checkBrackets ") )" |> should equal false 
        
    [<Test>]
    member this.``checkBrackets on data "( )" should return true`` () =
        checkBrackets "( )" |> should equal true   

    [<Test>]
    member this.``checkBrackets on data "( asd" should return false`` () =
        checkBrackets "( asd" |> should equal false   