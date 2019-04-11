namespace Hw2.Tests

open NUnit.Framework
open FsUnit
open Palindrome

[<TestFixture>]
type PalindromeTestClass () =

    [<Test>]
    member this.``mamam is polinrdom`` () = 
        isPolindrome "mamam" |> should equal true
    
    [<Test>]
     member this.``Son isn't polinrdom`` () = 
        isPolindrome "Son" |> should equal false
    
    [<Test>]
     member this.``With upper case`` () = 
        isPolindrome "Sos" |> should equal true
    
    [<Test>]
     member this.``Meme isn't polidrom`` () =
        isPolindrome "Meme" |> should equal false
    
    [<Test>]
     member this.``Polindrome with spaces`` () =
        isPolindrome "Я иду с мечем судия" |> should equal true 
        
    [<Test>]
     member this.``Polindrome with spaces and upper case`` () =
        isPolindrome "Я иДу С мЕчеМ сУдИя" |> should equal true