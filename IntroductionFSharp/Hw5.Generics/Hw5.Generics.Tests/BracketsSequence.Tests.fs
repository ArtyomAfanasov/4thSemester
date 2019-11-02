namespace Hw5.Generics.Tests

open BracketSequence
open NUnit.Framework
open FsUnit

[<TestFixture>]
type BracketSequenceTestClass () =
    
    [<Test>]
    member this.``For "[()]{}{[()()]()}" should return true`` () =
        checkBrackets "[()]{}{[()()]()}" |> should equal true        
    
    [<Test>]
    member this.``For "[(asd)]{asd}{[()(asd)]()}" should return true`` () =
        checkBrackets "[(asd)]{asd}{[()(asd)]()}" |> should equal true    
          
    [<Test>]
    member this.``For "[(])" should return false`` () =
        checkBrackets "[(])" |> should equal false          
    
    [<Test>]
    member this.``For "" should return true`` () =
        checkBrackets "" |> should equal true
    
    [<Test>]
    member this.``For "text" should return true`` () =
        checkBrackets "text" |> should equal true 
   
    [<Test>]
    member this.``For ")(" should return false`` () =
        checkBrackets ")(" |> should equal false