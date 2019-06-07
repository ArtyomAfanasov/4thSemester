namespace Hw5.Generics.Tests

open BracketSequence
open NUnit.Framework
open FsUnit

[<TestFixture>]
type BracketSequenceTestClass () =
    
    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "((()))" should return true`` () =
        checkStringOnOpenAndCloseValues "((()))" ('(', ')') ('\u0000', '\u0000') ('\u0000', '\u0000') 
        |> should equal true
    
    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "[[[]]]" should return true`` () =
        checkStringOnOpenAndCloseValues "[[[]]]" ('[', ']') ('\u0000', '\u0000') ('\u0000', '\u0000') 
        |> should equal true

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "{{{}}}" should return true`` () =
        checkStringOnOpenAndCloseValues "{{{}}}" ('{', '}') ('\u0000', '\u0000') ('\u0000', '\u0000') 
        |> should equal true

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "))((" should return false`` () =
        checkStringOnOpenAndCloseValues "))((" ('(', ')') ('\u0000', '\u0000') ('\u0000', '\u0000')
        |> should equal false

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "]][[" should return false`` () =
        checkStringOnOpenAndCloseValues "]][[" ('[', ']') ('\u0000', '\u0000') ('\u0000', '\u0000')
        |> should equal false

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "}}{{" should return false`` () =
        checkStringOnOpenAndCloseValues "}}{{" ('{', '}') ('\u0000', '\u0000') ('\u0000', '\u0000')
        |> should equal false

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "(]" should return false`` () =
        checkStringOnOpenAndCloseValues "(]" ('(', ')') ('[', ']') ('\u0000', '\u0000')
        |> should equal false

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "[)" should return false`` () =
        checkStringOnOpenAndCloseValues "[)" ('(', ')') ('[', ']') ('\u0000', '\u0000')
        |> should equal false

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "{)" should return false`` () =
        checkStringOnOpenAndCloseValues "{)" ('(', ')') ('{', '}') ('\u0000', '\u0000')
        |> should equal false

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "" should return true`` () =
        checkStringOnOpenAndCloseValues "" ('(', ')') ('[', ']') ('{', '}')
        |> should equal true

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "(a[b{c}b]b)" should return true`` () =
        checkStringOnOpenAndCloseValues "(a[b{c}b]b)" ('(', ')') ('{', '}') ('[', ']')
        |> should equal true

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "(a[b{c}b)b]" should return false`` () =
        checkStringOnOpenAndCloseValues "(a[b{c}b)b]" ('(', ')') ('{', '}') ('[', ']')
        |> should equal false

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "(a[b{c{b]b)" should return false`` () =
        checkStringOnOpenAndCloseValues "(a[b{c{b]b)" ('(', ')') ('{', '}') ('[', ']')
        |> should equal false   

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data ")a)b)c)b)b)" should return false`` () =
        checkStringOnOpenAndCloseValues ")a)b)c)b)b)" ('(', ')') ('\u0000', '\u0000') ('\u0000', '\u0000')
        |> should equal false   

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data ") )" should return false`` () =
        checkStringOnOpenAndCloseValues ") )" ('(', ')') ('\u0000', '\u0000') ('\u0000', '\u0000')
        |> should equal false 
        
    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "( )" should return true`` () =
        checkStringOnOpenAndCloseValues "( )" ('(', ')') ('\u0000', '\u0000') ('\u0000', '\u0000')
        |> should equal true   

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "( asd" should return false`` () =
        checkStringOnOpenAndCloseValues "( asd" ('(', ')') ('\u0000', '\u0000') ('\u0000', '\u0000')
        |> should equal false       

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
    member this.``checkStringOnOpenAndCloseValues on data "1asd2asd43as" should return true`` () =
        checkStringOnOpenAndCloseValues "1asd2asd43as" ('1', '3') ('2', '4') ('\u0000', '\u0000')
        |> should equal true 

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data 1asd2asd21as"  should return true`` () =
        checkStringOnOpenAndCloseValues "1asd2asd21as" ('1', '1') ('2', '2') ('\u0000', '\u0000')
        |> should equal true

    [<Test>]
    member this.``checkStringOnOpenAndCloseValues on data "(a<b{c}b>b)" should return true`` () =
        checkStringOnOpenAndCloseValues "(a<b{c}b>b)" ('(', ')') ('{', '}') ('<', '>')
        |> should equal true